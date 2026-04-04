/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Config/FrmConfig.cs
 * File: FrmConfig.cs
 * Created: 2026-03-31
 * Modified: 2026-04-04
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
        private SettingsModel CurrentSettings;
        private readonly string _configPath;
        #endregion
        #region public Structs/Enums
        #endregion
        #region Constructors/Destructors
        public FrmConfig(string appname)
        {
            InitializeComponent();
            AppName = appname;
            InitializeControls();
            InitializeTreeControls();
            cboPwdStyle.DataSource = Enum.GetValues(typeof(PasswordStyles));
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
            txtDbfPwd.Enabled = usesNetwork;

            // Enable/disable schema + instance
            txtDbName.Enabled = usesNetwork;
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
            cboPwdStyle.SelectedItem = s.PasswordStyle;

            // -------------------------
            // PASSWORD HANDLING
            // -------------------------
            if (s.PasswordStyle == PasswordStyles.Vault)
            {
                txtAppPwd.Text = string.IsNullOrWhiteSpace(s.AppPassword)
                                 ? ""
                                 : VaultManager.ReadSecret(s.AppPassword) ?? "";

                txtCfgPwd.Text = string.IsNullOrWhiteSpace(s.ConfigPassword)
                                 ? ""
                                 : VaultManager.ReadSecret(s.ConfigPassword) ?? "";

                txtDbfPwd.Text = string.IsNullOrWhiteSpace(s.DbPassword)
                                 ? ""
                                 : VaultManager.ReadSecret(s.DbPassword) ?? "";
            }
            else if (s.PasswordStyle == PasswordStyles.Encrypted)
            {
                txtAppPwd.Text = string.IsNullOrWhiteSpace(s.AppPassword)
                                 ? ""
                                 : EncryptionHelper.Decrypt(s.AppPassword);

                txtCfgPwd.Text = string.IsNullOrWhiteSpace(s.ConfigPassword)
                                 ? ""
                                 : EncryptionHelper.Decrypt(s.ConfigPassword);

                txtDbfPwd.Text = string.IsNullOrWhiteSpace(s.DbPassword)
                                 ? ""
                                 : EncryptionHelper.Decrypt(s.DbPassword);
            }
            else
            {
                // PlainText mode
                txtAppPwd.Text = s.AppPassword;
                txtCfgPwd.Text = s.ConfigPassword;
                txtDbfPwd.Text = s.DbPassword;
            }

            // -------------------------
            // PATHS
            // -------------------------
            txtLogPath.Text = s.LogPath;
            txtDataPath.Text = s.DataPath;
            txtTempPath.Text = s.TempPath;

            // -------------------------
            // DATABASE
            // -------------------------
            txtDbHost.Text = s.DbHost;
            numDbPort.IntValue = s.DbPort;
            txtDbName.Text = s.DbName;
            txtDbUser.Text = s.DbUser;
            txtDbInstance.Text = s.DbInstance;

            // Apply enable/disable rules for engine
            ApplyEngineRules();
        }
        private void BrowseForFolder(TextBox target)
        {
            using var dlg = new FolderBrowserDialog();
            dlg.ShowNewFolderButton = true;

            if (!string.IsNullOrWhiteSpace(target.Text) && Directory.Exists(target.Text))
                dlg.SelectedPath = target.Text;

            if (dlg.ShowDialog(this) == DialogResult.OK)
                target.Text = dlg.SelectedPath;
        }
        private SettingsModel CollectSettingsFromUI()
        {
            var engine = (DatabaseEngine)cboEngines.SelectedItem;
            string appName = AppName;

            string appPasswordKey = $"{appName}.Application";
            string configPasswordKey = $"{appName}.Configuration";
            string dbPasswordKey = $"{appName}.{engine}";

            var style = (PasswordStyles)cboPwdStyle.SelectedItem;

            string appPasswordToSave;
            string configPasswordToSave;
            string dbPasswordToSave;

            // -------------------------
            // VAULT MODE
            // -------------------------
            if (style == PasswordStyles.Vault)
            {
                appPasswordToSave = SavePasswordToVault(appPasswordKey, txtAppPwd.Text);
                configPasswordToSave = SavePasswordToVault(configPasswordKey, txtCfgPwd.Text);
                dbPasswordToSave = SavePasswordToVault(dbPasswordKey, txtDbfPwd.Text);
            }
            // -------------------------
            // ENCRYPTED MODE
            // -------------------------
            else if (style == PasswordStyles.Encrypted)
            {
                appPasswordToSave = string.IsNullOrWhiteSpace(txtAppPwd.Text)
                                       ? ""
                                       : EncryptionHelper.Encrypt(txtAppPwd.Text);

                configPasswordToSave = string.IsNullOrWhiteSpace(txtCfgPwd.Text)
                                       ? ""
                                       : EncryptionHelper.Encrypt(txtCfgPwd.Text);

                dbPasswordToSave = string.IsNullOrWhiteSpace(txtDbfPwd.Text)
                                       ? ""
                                       : EncryptionHelper.Encrypt(txtDbfPwd.Text);
            }
            // -------------------------
            // PLAINTEXT MODE
            // -------------------------
            else
            {
                appPasswordToSave = txtAppPwd.Text;
                configPasswordToSave = txtCfgPwd.Text;
                dbPasswordToSave = txtDbfPwd.Text;
            }

            return new SettingsModel
            {
                PasswordStyle = style,

                AppPassword = appPasswordToSave,
                ConfigPassword = configPasswordToSave,
                DbPassword = dbPasswordToSave,

                LogPath = txtLogPath.Text,
                DataPath = txtDataPath.Text,
                TempPath = txtTempPath.Text,

                DbHost = txtDbHost.Text,
                DbPort = numDbPort.Enabled ? numDbPort.IntValue : 0,
                DbName = txtDbName.Text,
                DbUser = txtDbUser.Text,
                DbInstance = txtDbInstance.Text
            };
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
            btnAppNew.Click += ButtonClicked;
            btnAppShow.Click += ButtonClicked;
            btnCfgShow.Click += ButtonClicked;
            btnDbfShow.Click += ButtonClicked;
            btnEncrypt.Click += ButtonClicked;
            btnSave.Click += ButtonClicked;
            btnCancel.Click += ButtonClicked;
            btnRead.Click += ButtonClicked;
            btnTest.Click += ButtonClicked;
            btnBrowseLogPath.Click += (s, e) => BrowseForFolder(txtLogPath);
            btnBrowseDataPath.Click += (s, e) => BrowseForFolder(txtDataPath);
            btnBrowseTempPath.Click += (s, e) => BrowseForFolder(txtTempPath);
            Load += FormLoad;
            lstDiagnosticsResults.DrawItem += LstSchemaResults_DrawItem;
            cboEngines.SelectedIndexChanged += CboEngines_SelectedIndexChanged;
            cboPwdStyle.SelectedIndexChanged += cboPwdStyle_SelectedIndexChanged;
            tabConfig.SelectedIndexChanged += TabConfig_SelectedIndexChanged;
            numDbPort.Leave += NumDbPort_Leave;
        }
        private void InitializeTreeControls()
        {
            tvDiagnostics.AfterSelect += TvDiagnostics_AfterSelect;
            btnRunTest.Click += TvDiagnosticButton_Click;
            btnRunGroup.Click += TvDiagnosticButton_Click;
        }
        private void InitializePasswordStyleUI()
        {
            // Force the SelectedIndexChanged logic to run
            cboPwdStyle_SelectedIndexChanged(cboPwdStyle, EventArgs.Empty);
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
        private void PopulateDetailsPanel(DiagnosticTest test, DiagnosticResult result)
        {
            lblTestName.Text = result.TestName;

            txtDescription.Text = DiagnosticsMetadata.GetDescription(test);
            txtExpected.Text = DiagnosticsMetadata.GetExpected(test);

            txtActual.Text = result.Passed
                ? "Test passed successfully."
                : result.Error ?? "Test failed.";

            txtRepair.Text = DiagnosticsMetadata.GetRepair(test);
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
        private void ShowDiagnosticResultOnButton(DiagnosticResult result, Button button)
        {
            string baseText = button.Tag?.ToString() ?? button.Text;

            if (result.Passed)
            {
                button.ForeColor = Color.Green;
                button.Text = $"{baseText}  ✔ Passed";
                button.BackColor = Color.FromArgb(230, 255, 230); // light green

                //_logger?.Info($"Diagnostics: {result.TestName} passed");
            }
            else
            {
                button.ForeColor = Color.Red;
                button.Text = $"{baseText}  ✖ Failed";
                button.BackColor = Color.FromArgb(255, 230, 230); // light red

                // Log the error
                //_logger?.Error($"Diagnostics: {result.TestName} failed: {result.Error}");

                // Show the error to the user
                MessageBox.Show(
                    result.Error ?? "Unknown error",
                    $"{result.TestName} Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
        private string SavePasswordToVault(string key, string uiValue)
        {
            // If empty → do NOT write to vault, return empty
            if (string.IsNullOrWhiteSpace(uiValue))
                return "";

            // If UI contains a real password (not the key), write it
            if (!uiValue.Equals(key, StringComparison.OrdinalIgnoreCase))
                VaultManager.WriteSecret(key, uiValue);

            // Save only the key
            return key;
        }
        private async void TestConnectionAsync(object sender, EventArgs e)
        {
            DatabaseSettings dbSettings = new()
            {
                Engine = (DatabaseEngine)cboEngines.SelectedItem,
                Host = txtDbHost.Text,
                Port = numDbPort.IntValue,
                User = txtDbUser.Text,
                Password = txtDbfPwd.Text,
                Schema = txtDbName.Text,
                Instance = txtDbInstance.Text
            };
            bool ok = await DbConnectionTester.TestConnectionAsync(dbSettings);
            MessageBox.Show(ok ? "Connection successful" : "Connection failed");
        }
        private void TogglePassword(TextBox txt, Button btn)
        {
            bool isShowing = txt.PasswordChar == '\0';

            // Toggle the mask
            txt.PasswordChar = isShowing ? '•' : '\0';

            // Update the button label
            btn.Text = isShowing ? "Show" : "Hide";
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
                    case "AppNew":
                        FrmPassword frm = new(null);
                        DialogResult rc = frm.ShowDialog(this);
                        PasswordDialogResult pmd = frm.PasswordData;
                        txtAppPwd.Text = string.Empty;
                        break;
                    case "AppShow":
                        TogglePassword(txtAppPwd, btn);
                        break;
                    case "Cancel":
                        DialogResult = DialogResult.Cancel;
                        Close();
                        break;
                    case "CfgShow":
                        TogglePassword(txtCfgPwd, btn);
                        break;
                    case "DbfShow":
                        TogglePassword(txtDbfPwd, btn);
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
        private void cboPwdStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPwdStyle.SelectedItem is PasswordStyles style)
            {
                // Default: hide everything
                grpPasswords.Visible = false;
                btnEncrypt.Visible = false;

                switch (style)
                {
                    case PasswordStyles.Vault:
                        grpPasswords.Visible = true;
                        btnEncrypt.Visible = false;
                        break;

                    case PasswordStyles.Encrypted:
                        // Show all password fields + show/hide buttons + Encrypt
                        grpPasswords.Visible = true;
                        btnEncrypt.Visible = true;
                        break;

                    case PasswordStyles.PlainText:
                        // Show all password fields + show/hide buttons, but no Encrypt
                        grpPasswords.Visible = true;
                        btnEncrypt.Visible = false;
                        break;

                    case PasswordStyles.Prompt:
                        // No passwords shown; runtime prompt only
                        break;
                }
            }
        }
        private void CboEngines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEngines.SelectedItem is DatabaseEngine engine)
            {
                ApplyEngineRules();
            }
        }
        private void FormLoad(object sender, EventArgs e)
        {
            // Load settings
            SettingsModel settings = ConfigManager.Load(AppName);
            ApplySettingsToUI(settings);
            ApplyEngineRules();
            InitializePasswordStyleUI();

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
