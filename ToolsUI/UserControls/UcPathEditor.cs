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
 * Version: 1.0.1
 * Created: 2026-05-11
 * Modified: 2026-05-13
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
using Tools.Config;
using Tools.Enums;
#endregion
#region Project Libraries
#endregion
namespace ToolsUI.UserControls
{
    public partial class UcPathEditor : EditorBase
    {
        #region Private Fields
        private SettingsModel _settings;
        private PathLocation _loc;
        #endregion
        #region Constructors/Destructors
        public UcPathEditor()
        {
            InitializeComponent();
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
            btnCancel.Click += Button_Click;
            btnRemove.Click += Button_Click;
        }
        #endregion
        #region Private Helpers
        private void PopulateCombos()
        {
            cboPathName.DataSource = Enum.GetValues(typeof(PathLocation));
            cboPathType.DataSource = Enum.GetValues(typeof(PathType));
        }
        private void ReloadMetadata()
        {
            var ap = _settings.Paths.FirstOrDefault(p => p.PathName == _loc);
            if (ap == null)
                return;

            cboPathName.SelectedItem = _loc;
            cboPathType.SelectedItem = ap.PathType;
            txtPath.Text = ap.PathValue;
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
                        var ap = _settings.Paths.FirstOrDefault(p => p.PathName == _loc);
                        if (ap != null)
                        {
                            ap.PathType = (PathType)cboPathType.SelectedItem;
                            ap.PathValue = txtPath.Text;
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
                        RequestClose();
                        break;
                    case "Cancel":
                        RequestClose();
                        break;
                    case "Remove":
                        AppPath? apRemove = _settings.Paths.FirstOrDefault(p => p.PathName == _loc);
                        if (apRemove != null)
                        {
                            _settings.Paths.Remove(apRemove);
                            RequestClose();
                        }
                        break;
                }
            }
        }
        #endregion
    }
}