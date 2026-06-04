/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Config/FrmConfig.cs
 * File: FrmConfig.cs
 * Version: 1.0.5
 * Created: 2026-03-31
 * Modified: 2026-06-04
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
        private bool _isDirty = false;
        private SettingsModel CurrentSettings;
        private SettingsModel _settings;
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
            _configPath = ConfigManager.GetConfigPath(AppName);
        }
        #endregion
        #region Private Helper Methods
        private void AttachCloseHandler(EditorBase editor)
        {
            editor.CloseRequested += (s, e) =>
            {
                pnlContent.Controls.Remove(editor);
                editor.Dispose();
            };
        }
        private void AttachRefreshHandler(EditorBase editor)
        {
            editor.RefreshRequested += (s, e) =>
            {
                _isDirty = true;
                btnSave.Enabled = true;

                ReloadTree();

                if (tvConfig.SelectedNode != null)
                {
                    TvConfig_AfterSelect(tvConfig, new TreeViewEventArgs(tvConfig.SelectedNode));
                }
            };
        }
        private void AdjustFormSize(UserControl editor)
        {
            const int horizontalPadding = 20;
            const int verticalPadding = 30;
            const int buttonSpacing = 12;   // space between pnlConfigure and pnlButtons
            const int bottomPadding = 20;   // space below buttons

            // Preferred size of the editor
            Size pref = editor.GetPreferredSize(Size.Empty);

            // Width: tree + editor + padding
            int requiredWidth =
                tvConfig.Width +
                pref.Width +
                horizontalPadding;

            // Height of the configure panel (tree + editor area)
            int configureHeight =
                Math.Max(pref.Height + verticalPadding, pnlConfigure.MinimumSize.Height);

            // Apply to pnlConfigure
            pnlConfigure.Size = new Size(requiredWidth, configureHeight);

            // Now compute total form client height: configure + spacing + buttons + bottom padding
            int totalClientHeight =
                pnlConfigure.Height +
                buttonSpacing +
                pnlButtons.Height +
                bottomPadding;

            // Set form client size
            this.ClientSize = new Size(requiredWidth, Math.Max(totalClientHeight, this.MinimumSize.Height));

            // Center pnlButtons on the form (or pnlConfigure width, they match now)
            pnlButtons.Location = new Point(
                (this.ClientSize.Width - pnlButtons.Width) / 2,
                pnlConfigure.Bottom + buttonSpacing
            );
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
                    node.Tag = target;
                    DatabaseConfig dbf = _settings.Database;

                    foreach (DbConnection? conn in dbf.Connections)
                    {
                        string label = $"{conn.ConnectionId} ({conn.Engine})";
                        TreeNode child = node.Nodes.Add(label);

                        child.Tag = (PasswordTarget.Database, (string)conn.ConnectionId);
                    }
                }
                else
                {
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
                node.Tag = loc;
            }

            // -------------------------
            // DATABASE (data-driven)
            // -------------------------
            TreeNode dbNode = root.Nodes.Add("Database");

            TreeNode connRoot = dbNode.Nodes.Add("Connections");

            foreach (DbConnection? conn in _settings.Database.Connections)
            {
                string label = $"{conn.ConnectionId} ({conn.Engine})";
                TreeNode node = connRoot.Nodes.Add(label);
                node.Tag = conn;
            }

            TreeNode addNode = connRoot.Nodes.Add("Add Connection…");
            addNode.Tag = "AddConnection";

            // -------------------------
            // LOGGING (new root)
            // -------------------------
            TreeNode logNode = root.Nodes.Add("Logging");
            logNode.Tag = "Logging";   // simple tag for AfterSelect

            root.Expand();
        }
        private void InitializeControls()
        {
            Text = $"{AppName} Records Configuration Manager";
            btnClose.Click += Button_Click;
            btnSave.Click += Button_Click;
            Load += FormLoad;
        }
        private void LoadControl(UserControl ctrl)
        {
            pnlContent.Controls.Clear();
            ctrl.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(ctrl);
            SuspendLayout();
            AdjustFormSize(ctrl);
            ResumeLayout();
        }
        private void LoadDatabaseEditor(DbConnection conn)
        {
            var editor = new UcDatabaseEditor
            {
                CurrentSettings = _settings
            };
            AttachCloseHandler(editor);
            AttachRefreshHandler(editor);
            editor.Initialize(conn);
            LoadControl(editor);
        }
        private void LoadPasswordEditor(PasswordTarget target, string? connectionId)
        {
            UcPasswordEditor editor = new UcPasswordEditor
            {
                CurrentSettings = _settings
            };
            AttachCloseHandler(editor);
            AttachRefreshHandler(editor);
            editor.Initialize(target, connectionId);

            LoadControl(editor);
        }
        private void LoadPathEditor(PathLocation loc)
        {
            UcPathEditor editor = new UcPathEditor
            {
                CurrentSettings = _settings
            };
            AttachCloseHandler(editor);
            AttachRefreshHandler(editor);
            editor.Initialize(loc, _settings);
            LoadControl(editor);
        }
        private void ReloadTree()
        {
            tvConfig.Nodes.Clear();
            BuildConfigTree();   // your existing builder
            tvConfig.ExpandAll();
        }
        #endregion
        #region Public Properties
        public string AppName { get; set; }
        #endregion
        #region Public Methods
        #endregion
        #region Private Form Methods
        private void Button_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is string tag)
            {
                switch (tag)
                {
                    case "Save":
                        ConfigManager.Save(AppName, _settings);

                        _isDirty = false;          // reset dirty flag
                        btnSave.Enabled = false;   // disable Save button

                        MessageBox.Show(
                            "Configuration saved successfully.",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        // DO NOT close the form
                        break;
                    case "Cancel":
                        this.Close();
                        break;
                }
            }
        }
        private void FormLoad(object sender, EventArgs e)
        {
            _settings = ConfigManager.Load(AppName);

            BuildConfigTree();
            tvConfig.AfterSelect += TvConfig_AfterSelect;
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
                case DbConnection conn:
                    LoadDatabaseEditor(conn);
                    break;
            }
        }
        #endregion
    }
}
