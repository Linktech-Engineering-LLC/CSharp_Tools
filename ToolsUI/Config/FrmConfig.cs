/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Config/FrmConfig.cs
 * File: FrmConfig.cs
 * Version: 1.0.0
 * Created: 2026-03-31
 * Modified: 2026-05-11
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
#region System Libraries
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion
#region Project Libraries
using DbTools;
using Tools.Config;
using Tools.Diagnostics;
using Tools.Enums;
using Tools.Files;
using ToolsUI.Helpers;
using ToolsUI.Layout;
using ToolsUI.UserControls;
#endregion

namespace ToolsUI.Config
{
    public partial class FrmConfig : Form
    {
        #region Private Classes
        #endregion
        #region Private Variables
        private UIHelperService Helpers = new();
        private TabLayoutManager _layout;
        private bool _loadingPath = false;
        private SettingsModel CurrentSettings;
        private SettingsModel _settings;
        private readonly string _configPath;
        private SettingsModel currentSettings {  get; set; }
        #endregion
        #region public Structs/Enums
        #endregion
        #region Constructors/Destructors
        public FrmConfig(string appname)
        {
            InitializeComponent();
            AppName = appname;
            InitializeControls();
            //InitializeTreeControls();
            cboEngines.DataSource = Enum.GetValues(typeof(DatabaseEngine));
            _configPath = ConfigManager.GetConfigPath(AppName);
        }
        #endregion
        #region Private Helper Methods
        private void ApplyEngineRules()
        {
            var engine = (DatabaseEngine)cboEngines.SelectedItem;

            bool usesNetwork = engine switch
            {
                DatabaseEngine.SQLite => false,
                _ => true
            };

            // Enable/disable host + port
            txtDbHost.Enabled = usesNetwork;
            numDbPort.Enabled = usesNetwork;

            // Enable/disable user + password
            txtDbUser.Enabled = usesNetwork;

            // Enable/disable schema + instance
            txtDbSchema.Enabled = usesNetwork;
            txtDbInstance.Enabled = usesNetwork && (engine == DatabaseEngine.SQLServer || engine == DatabaseEngine.Oracle);

            // Apply default port only when enabled
            if (numDbPort.Enabled)
            {
                numDbPort.IntValue = engine switch
                {
                    DatabaseEngine.MariaDB or DatabaseEngine.MySQL => 3306,
                    DatabaseEngine.PostgreSql => 5432,
                    DatabaseEngine.SQLServer => 1433,
                    DatabaseEngine.Oracle => 1521,
                    _ => 0
                };
            }
            else
            {
                // SQLite → no port
                numDbPort.IntValue = 0;
            }
        }
        private void ApplySettingsToUI(SettingsModel s)
        {
            // -------------------------
            // PATHS
            // -------------------------
            //txtPath.Text = s.LogPath.PathValue;

            // -------------------------
            // DATABASE
            // -------------------------
            //txtDbHost.Text = s.DbHost;
            //numDbPort.IntValue = s.DbPort;
            //txtDbSchema.Text = s.DbName;
            //txtDbUser.Text = s.DbUser;
            //txtDbInstance.Text = s.DbInstance;

            // Apply enable/disable rules for engine
            ApplyEngineRules();
        }
        private void BuildConfigTree()
        {
            tvConfig.Nodes.Clear();

            // Root
            TreeNode root = tvConfig.Nodes.Add("Configuration");

            // -------------------------
            // SECURITY (enum-driven)
            // -------------------------
            TreeNode secNode = root.Nodes.Add("Security");

            foreach (PasswordTarget target in Enum.GetValues(typeof(PasswordTarget)))
            {
                TreeNode node = secNode.Nodes.Add(target.ToString());

                if (target == PasswordTarget.Database)
                {
                    // Tag the parent node as Database
                    node.Tag = target;
                    DatabaseConfig dbf = _settings.Database;
                    // Add child nodes for each connection
                    foreach (DbConnection? conn in dbf.Connections)
                    {
                        string label = $"{conn.ConnectionId} ({conn.Engine})";
                        TreeNode child = node.Nodes.Add(label);

                        // Tag the child with BOTH target + connection ID
                        child.Tag = (PasswordTarget.Database, (string)conn.ConnectionId);
                    }
                }
                else
                {
                    // Normal case: Application, Configuration
                    node.Tag = target;
                }
            }

            // -------------------------
            // PATHS (enum-driven)
            // -------------------------
            TreeNode pathsNode = root.Nodes.Add("Paths");

            foreach (PathLocation loc in Enum.GetValues(typeof(PathLocation)))
            {
                TreeNode node = pathsNode.Nodes.Add(loc.ToString());
                node.Tag = loc;      // store enum for AfterSelect
            }

            // -------------------------
            // DATABASE (data-driven)
            // -------------------------
            TreeNode dbNode = root.Nodes.Add("Database");

            // Connections branch
            TreeNode connRoot = dbNode.Nodes.Add("Connections");

            foreach (DbConnection? conn in _settings.Database.Connections)
            {
                string label = $"{conn.ConnectionId} ({conn.Engine})";
                TreeNode node = connRoot.Nodes.Add(label);
                node.Tag = conn;     // store the actual DbConnection object
            }

            // Add Connection node
            TreeNode addNode = connRoot.Nodes.Add("Add Connection…");
            addNode.Tag = "AddConnection";

            root.Expand();
        }
        private SettingsModel CollectSettingsFromUI()
        {
            // We assume `currentSettings` is the in-memory SettingsModel
            // that already contains updated PasswordMetadata objects
            // from the password dialog.

            //currentSettings.LogPath.PathValue = txtPath.Text;

            //currentSettings.DbHost = txtDbHost.Text;
            //currentSettings.DbPort = numDbPort.Enabled ? numDbPort.IntValue : 0;
            //currentSettings.DbName = txtDbSchema.Text;
            //currentSettings.DbUser = txtDbUser.Text;
            //currentSettings.DbInstance = txtDbInstance.Text;
            return currentSettings;
        }
        private void DisplayResult(DiagnosticResult result)
        {
            lstDiagnosticsResults.Items.Add(
                new DiagnosticListItem(result.ToDisplayString(), result.Passed)
            );

            if (result.MissingItems.Count > 0)
                lstDiagnosticsResults.Items.Add($"   Missing: {string.Join(", ", result.MissingItems)}");

            if (result.ExtraItems.Count > 0)
                lstDiagnosticsResults.Items.Add($"   Extra: {string.Join(", ", result.ExtraItems)}");
        }
        private void InitializeControls()
        {
            btnSave.Click += ButtonClicked;
            btnCancel.Click += ButtonClicked;
            btnRead.Click += ButtonClicked;
            btnTest.Click += ButtonClicked;
            Load += FormLoad;
            lstDiagnosticsResults.DrawItem += LstSchemaResults_DrawItem;
            cboEngines.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            tabConfig.SelectedIndexChanged += TabConfig_SelectedIndexChanged;
            numDbPort.Leave += NumDbPort_Leave;
        }
        private void LoadControl(UserControl ctrl)
        {
            pnlContent.Controls.Clear();
            ctrl.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(ctrl);
        }
        private void LoadPasswordEditor(PasswordTarget target, string? connectionId)
        {
            UcPasswordEditor editor = new UcPasswordEditor
            {
                CurrentSettings = _settings
            };
            editor.CloseRequested += (s, e) =>
            {
                pnlContent.Controls.Remove(editor);
                editor.Dispose();
                RefreshTree();
            };
            editor.Initialize(target, connectionId);

            LoadControl(editor);
        }
        private void LoadPathEditor(PathLocation loc)
        {
            UcPathEditor editor = new UcPathEditor
            {
                CurrentSettings = _settings
            };
            editor.CloseRequested += (s, e) =>
            {
                pnlContent.Controls.Remove(editor);
                editor.Dispose();
                RefreshTree();
            };
            editor.Initialize(loc, _settings);
            LoadControl(editor);
        }
        private void LstSchemaResults_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            var item = (DiagnosticListItem)lstDiagnosticsResults.Items[e.Index];

            e.DrawBackground();

            Color color = item.Success ? Color.LimeGreen : Color.Red;

            TextRenderer.DrawText(
                e.Graphics,
                item.Text,
                e.Font,
                e.Bounds,
                color,
                TextFormatFlags.Left
            );

            e.DrawFocusRectangle();
        }
        private void PopulateDiagnosticsTree()
        {
            tvDiagnostics.Nodes.Clear();

            // --- File Integrity Group ---
            var fileGroup = new TreeNode("File Integrity")
            {
                Tag = new DiagnosticNodeTag
                {
                    Group = DiagnosticGroup.FileIntegrity,
                    Test = null
                }
            };

            fileGroup.Nodes.Add(new TreeNode("Exists")
            {
                Tag = new DiagnosticNodeTag
                {
                    Group = DiagnosticGroup.FileIntegrity,
                    Test = DiagnosticTest.ConfigFileExists
                }
            });

            fileGroup.Nodes.Add(new TreeNode("Readable")
            {
                Tag = new DiagnosticNodeTag
                {
                    Group = DiagnosticGroup.FileIntegrity,
                    Test = DiagnosticTest.ConfigFileReadable
                }
            });

            fileGroup.Nodes.Add(new TreeNode("Healthy")
            {
                Tag = new DiagnosticNodeTag
                {
                    Group = DiagnosticGroup.FileIntegrity,
                    Test = DiagnosticTest.ConfigFileValidLiteDB
                }
            });

            fileGroup.Nodes.Add(new TreeNode("Required Collections")
            {
                Tag = new DiagnosticNodeTag
                {
                    Group = DiagnosticGroup.FileIntegrity,
                    Test = DiagnosticTest.RequiredCollections
                }
            });

            tvDiagnostics.Nodes.Add(fileGroup);
            tvDiagnostics.ExpandAll();
        }
        private void RefreshTree()
        {
            tvConfig.Nodes.Clear();
            BuildConfigTree();   // your existing builder
            tvConfig.ExpandAll();
        }
        private void RunTest(DiagnosticTest test)
        {
            switch (test)
            {
                case DiagnosticTest.ConfigFileExists:
                    RunTestConfigFileExists();
                    break;

                case DiagnosticTest.ConfigFileReadable:
                    RunTestConfigFileReadable();
                    break;

                case DiagnosticTest.ConfigFileValidLiteDB:
                    RunTestConfigFileValidLiteDB();
                    break;

                case DiagnosticTest.RequiredCollections:
                    RunTestRequiredCollections();
                    break;

                default:
                    MessageBox.Show("Test not implemented.");
                    break;
            }
        }
        private void RunTestConfigFileExists()
        {
            var result = DiagnosticsTests.Test_ConfigFileExists(_configPath);
            DisplayResult(result);
        }
        private void RunTestConfigFileReadable()
        {
            var result = DiagnosticsTests.Test_ConfigFileReadable(_configPath);
            DisplayResult(result);
        }
        private void RunTestConfigFileValidLiteDB()
        {
            var result = DiagnosticsTests.Test_ConfigFileValidLiteDB(_configPath);
            DisplayResult(result);
        }
        private void RunTestRequiredCollections()
        {
            var result = DiagnosticsService.TestRequiredFields(_configPath);
            DisplayResult(result);
        }
        private async void TestConnectionAsync(object sender, EventArgs e)
        {
            DatabaseSettings dbSettings = new()
            {
                Engine = (DatabaseEngine)cboEngines.SelectedItem,
                Host = txtDbHost.Text,
                Port = numDbPort.IntValue,
                User = txtDbUser.Text,
                //Password = ResolvePasswordFromSettings(currentSettings.DbPassword ?? new PasswordMetadata()),
                Schema = txtDbSchema.Text,
                Instance = txtDbInstance.Text
            };
            bool ok = await DbConnectionTester.TestConnectionAsync(dbSettings);
            MessageBox.Show(ok ? "Connection successful" : "Connection failed");
        }

        #endregion
        #region Public Properties
        public string AppName { get; set; }
        #endregion
        #region Public Methods
        public class DiagnosticListItem
        {
            public string Text { get; }
            public bool Success { get; }

            public DiagnosticListItem(string text, bool success)
            {
                Text = text;
                Success = success;
            }

            public override string ToString() => Text;
        }
        #endregion
        #region Private Form Methods
        private void ButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is string Tags)
            {
                switch (Tags)
                {
                    case "Cancel":
                        DialogResult = DialogResult.Cancel;
                        Close();
                        break;
                    case "Encrypt":
                        MessageBox.Show("Encryption settings would be displayed here.", "Encryption", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case "Read":
                        // Here you would read the existing settings from the DBLite file and populate the UI
                        SettingsModel reader = ConfigManager.Load(AppName);
                        ApplySettingsToUI(reader);
                        break;
                    case "Save":
                        // Here you would gather all settings and save them as needed
                        SettingsModel writer = CollectSettingsFromUI();
                        ConfigManager.Save(AppName, writer);
                        Close();
                        break;
                    case "Test":
                        TestConnectionAsync(sender, e);
                        break;
                    default:
                        MessageBox.Show("Unknown action.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is ComboBox cbo)
            {
                switch (cbo.Tag)
                {
                    case "Engines":
                        ApplyEngineRules();
                        break;
                    default:
                        break;
                }
            }
        }
        private void FormLoad(object sender, EventArgs e)
        {
            // Load settings
            _settings = ConfigManager.Load(AppName);
            BuildConfigTree();
            tvConfig.AfterSelect += TvConfig_AfterSelect;
            //ApplySettingsToUI(currentSettings);
            //ApplyEngineRules();

            // Build layout manager
            _layout = new TabLayoutManager(
                this,
                tabConfig,
                new Dictionary<string, TabLayoutInfo>
                {
                    ["pgeConfigure"] = new()
                    {
                        Panel = pnlConfigure
                    },

                    ["pgeDiagnostics"] = new()
                    {
                        Panel = pnlDiagnostics
                    }
                }
            );
            // Apply initial layout
            _layout.ApplyLayout();
            PopulateDiagnosticsTree();
        }
        private void NumDbPort_Leave(object? sender, EventArgs e)
        {
            if (!numDbPort.HasValue || numDbPort.IntValue < 1 || numDbPort.IntValue > numDbPort.Maximum)
            {
                MessageBox.Show("Please enter a valid port number between 1 and " + numDbPort.Maximum);
                numDbPort.Focus();
            }
        }
        private void TabConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            _layout?.ApplyLayout();
        }
        private void TvConfig_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = e.Node;

            // 1. If this node has children, DO NOT load the editor
            if (node.Nodes.Count > 0)
            {
                return;
            }

            // 2. Handle leaf nodes only
            switch (node.Tag)
            {
                case PasswordTarget target:
                    LoadPasswordEditor(target, null);
                    break;

                case ValueTuple<PasswordTarget, string> dbTag:
                    LoadPasswordEditor(dbTag.Item1, dbTag.Item2);
                    break;

                case PathLocation loc:
                    LoadPathEditor(loc);
                    break;
            }
        }
        private void TvDiagnostics_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var tag = e.Node?.Tag as DiagnosticNodeTag;

            if (tag?.Test != null)
            {
                lstDiagnosticsResults.Items.Clear();
                if (tag?.Test is DiagnosticTest test)
                {
                    RunTest(test);
                }
                else
                {
                    MessageBox.Show("Please select a specific diagnostic test.");
                }
                lstDiagnosticsResults.Refresh();
            }
        }
        private void TvDiagnosticButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is string Tags)
            {
                switch (Tags)
                {
                    case "RunTest":
                        var tag = tvDiagnostics.SelectedNode?.Tag as DiagnosticNodeTag;

                        if (tag == null)
                        {
                            MessageBox.Show("Please select a diagnostic test from the tree.");
                            return;
                        }

                        if (tag.Test == null)
                        {
                            MessageBox.Show("Please select a specific diagnostic test.");
                            return;
                        }

                        if (tag?.Test is DiagnosticTest test)
                        {
                            RunTest(test);
                        }
                        else
                        {
                            MessageBox.Show("Please select a specific diagnostic test.");
                        }
                        lstDiagnosticsResults.Refresh(); break;
                    case "RunGroup":
                        // For simplicity, we'll just run the required fields test as an example
                        RunTestRequiredCollections();
                        lstDiagnosticsResults.Refresh();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
