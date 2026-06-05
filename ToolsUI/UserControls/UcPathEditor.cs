/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/UserControls/UcPathEditor.cs
 * File: UcPathEditor.cs
 * Version: 1.0.4
 * Created: 2026-05-11
 * Modified: 2026-06-05
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
#region Public Libraries
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion
#region Project Libraries
using Tools.Config;
using Tools.Enums;
#endregion
namespace ToolsUI.UserControls
{
    public partial class UcPathEditor : EditorBase
    {
        #region Private Fields
        private SettingsModel _settings;
        private PathLocation _loc;
        private bool _loading = false;
        #endregion
        #region Constructors/Destructors
        public UcPathEditor()
        {
            InitializeComponent();
        }
        #endregion
        #region Private Helpers
        private bool IsLoggerPath(PathLocation loc)
        {
            return loc == PathLocation.LogPath
                || loc == PathLocation.RotatePath
                || loc == PathLocation.ArchivePath;
        }
        #endregion
        #region Public Properties/Methods
        public SettingsModel CurrentSettings { get; set; }
        public override Size GetPreferredSize(Size proposedSize)
        {
            return pnlRoot.Size;
        }
        public void Initialize(PathLocation loc, SettingsModel settings)
        {
            _settings = settings;
            _loc = loc;

            PopulateCombos();
            ReloadMetadata();
            btnBrowse.Click += Button_Click;
            btnUpdate.Click += Button_Click;
            btnRemove.Click += Button_Click;
            cboPathName.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
        }
        #endregion
        #region Private Helpers
        private AppPath EnsureLoggerPath(PathLocation loc)
        {
            AppPath ap = loc switch
            {
                PathLocation.LogPath => _settings.Logger.LogDirectory,
                PathLocation.RotatePath => _settings.Logger.RotatePath,
                PathLocation.ArchivePath => _settings.Logger.ArchivePath,
                _ => null
            };

            if (ap == null)
            {
                ap = new AppPath
                {
                    PathName = loc,
                    PathType = PathType.LocalDrive,
                    PathValue = string.Empty
                };

                // Assign it back into LoggerConfig
                switch (loc)
                {
                    case PathLocation.LogPath:
                        _settings.Logger.LogDirectory = ap;
                        break;
                    case PathLocation.RotatePath:
                        _settings.Logger.RotatePath = ap;
                        break;
                    case PathLocation.ArchivePath:
                        _settings.Logger.ArchivePath = ap;
                        break;
                }
            }

            return ap;
        }
        private void PopulateCombos()
        {
            cboPathName.DataSource = Enum.GetValues(typeof(PathLocation));
            cboPathType.DataSource = Enum.GetValues(typeof(PathType));
        }
        private void ReloadMetadata()
        {
            _loading = true;
            cboPathName.SelectedItem = _loc;

            bool isLoggerPath = IsLoggerPath(_loc);

            if (isLoggerPath)
            {
                // Ensure the AppPath object exists
                AppPath ap = EnsureLoggerPath(_loc);

                // Load the value
                txtPath.Text = ap.PathValue ?? string.Empty;

                // Logger paths always use LocalDrive
                cboPathType.Enabled = false;
                cboPathType.SelectedItem = PathType.LocalDrive;

                // Button states
                bool hasValue = !string.IsNullOrWhiteSpace(ap.PathValue);
                btnRemove.Enabled = hasValue;
                btnUpdate.Text = hasValue ? "&Edit" : "&Add";

                _loading = false;
                return;
            }

            //
            // Normal (non-logger) path handling
            //
            AppPath? normal = _settings.Paths.FirstOrDefault(p => p.PathName == _loc);

            txtPath.Text = normal?.PathValue ?? string.Empty;
            btnRemove.Enabled = normal != null;
            btnUpdate.Text = normal != null ? "&Edit" : "&Add";

            if (normal == null)
            {
                cboPathType.Enabled = true;
                cboPathType.SelectedIndex = -1;
                cboPathType.Text = string.Empty;
            }
            else
            {
                cboPathType.Enabled = true;
                cboPathType.SelectedItem = normal.PathType;
            }

            _loading = false;
        }

        #endregion
        #region Private From Events
        private void Button_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is string Tags)
            {
                switch(Tags)
                {
                    case "Browse":
                        using (var fbd = new FolderBrowserDialog())
                        {
                            if (fbd.ShowDialog() == DialogResult.OK)
                            {
                                txtPath.Text = fbd.SelectedPath;
                            }
                        }
                        break;
                    case "Update":

                        if (IsLoggerPath(_loc))
                        {
                            string value = txtPath.Text;
                            
                            switch (_loc)
                            {
                                case PathLocation.LogPath:
                                    _settings.Logger.LogDirectory.PathValue = value ?? string.Empty;
                                    break;

                                case PathLocation.RotatePath:
                                    _settings.Logger.RotatePath.PathValue = value ?? string.Empty;
                                    break;

                                case PathLocation.ArchivePath:
                                    _settings.Logger.ArchivePath.PathValue = value ?? string.Empty;
                                    break;
                            }

                            // Remove from general paths list
                            var ap = _settings.Paths.FirstOrDefault(p => p.PathName == _loc);
                            if (ap != null)
                                _settings.Paths.Remove(ap);

                            RequestRefresh();
                            break;
                        }

                        // Normal path update...

                        // Normal path update
                        var path = _settings.Paths.FirstOrDefault(p => p.PathName == _loc);
                        if (path != null)
                        {
                            path.PathType = (PathType)cboPathType.SelectedItem;
                            path.PathValue = txtPath.Text;
                        }
                        else
                        {
                            _settings.Paths.Add(new AppPath()
                            {
                                PathName = _loc,
                                PathType = (PathType)cboPathType.SelectedItem,
                                PathValue = txtPath.Text
                            });
                        }

                        RequestRefresh();
                        break;
                    case "Remove":

                        if (IsLoggerPath(_loc))
                        {
                            // Clear logger config
                            switch (_loc)
                            {
                                case PathLocation.LogPath:
                                    _settings.Logger.LogDirectory.PathValue = string.Empty;
                                    break;

                                case PathLocation.RotatePath:
                                    _settings.Logger.RotatePath.PathValue = string.Empty;
                                    break;

                                case PathLocation.ArchivePath:
                                    _settings.Logger.ArchivePath.PathValue = string.Empty;
                                    break;
                            }

                            // Remove from general paths list
                            var apRemove = _settings.Paths.FirstOrDefault(p => p.PathName == _loc);
                            if (apRemove != null)
                                _settings.Paths.Remove(apRemove);

                            RequestRefresh();
                            break;
                        }

                        // Normal path removal
                        var ap2 = _settings.Paths.FirstOrDefault(p => p.PathName == _loc);
                        if (ap2 != null)
                            _settings.Paths.Remove(ap2);

                        RequestRefresh();
                        break;
                }
            }
        }
        private void ComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_loading) return;

            if (sender is ComboBox cbo && cbo.Tag is string Tags)
            {
                switch (Tags)
                {
                    case "PathName":
                        if (cbo.SelectedItem is PathLocation pl)
                        {
                            _loc = pl;
                            ReloadMetadata();
                        }
                        break;
                }
            }
        }
        #endregion
    }
}