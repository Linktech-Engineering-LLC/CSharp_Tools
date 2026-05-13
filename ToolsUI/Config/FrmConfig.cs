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
 * Version: 1.0.2
 * Created: 2026-03-31
 * Modified: 2026-05-13
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
        private void AdjustFormSize(UserControl editor)
        {
            // Base padding for borders and spacing
            const int horizontalPadding = 20;
            const int verticalPadding = 30;
            Size pref = editor.GetPreferredSize(Size.Empty);

            int requiredWidth = tvConfig.Width + pref.Width + horizontalPadding;
            int requiredHeight = Math.Max(pref.Height + verticalPadding, MinimumSize.Height);

            // Resize form to fit editor comfortably
            pnlConfigure.Size = new Size(requiredWidth, requiredHeight);
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
            editor.Initialize(loc, _settings);
            LoadControl(editor);
        }
        private void RefreshTree()
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
        private void FormLoad(object sender, EventArgs e)
        {
            // Load settings
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
