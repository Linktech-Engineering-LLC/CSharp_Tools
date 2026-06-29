/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/UserControls/UcLoggerEditor.cs
 * File: UcLoggerEditor.cs
 * Version: 1.0.4
 * Created: 2026-06-05
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
#endregion
namespace ToolsUI.UserControls
{
    public partial class UcLoggerEditor : EditorBase
    {
        #region Private Fields
        private SettingsModel _settings;
        private LoggerConfig _config;
        #endregion
        #region Constructors/Destructors
        public UcLoggerEditor()
        {
            InitializeComponent();
        }
        #endregion
        #region Private Helper Methods
        private void InitializeOptions()
        {
            clbOptions.Items.Clear();

            foreach (LoggerOption opt in Enum.GetValues(typeof(LoggerOption)))
            {
                clbOptions.Items.Add(opt);
            }
        }
        #endregion
        #region Public Properties/Methods
        public SettingsModel CurrentSettings { get; set; }
        public void Initialize(LoggerConfig settings)
        {
            _config = settings;
            cboMinimumLevel.DataSource = Enum.GetValues(typeof(LogLevel));
            cboRotationType.DataSource = Enum.GetValues(typeof(RotationType));
            cboArchiveType.DataSource = Enum.GetValues(typeof(ArchiveType));
            InitializeOptions();
        }
        #endregion
        #region Private Form Events
        #endregion
    }
}
