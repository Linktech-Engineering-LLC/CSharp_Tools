/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Files/FilerUIConfig.cs
 * File: FilerUIConfig.cs
 * Created: None
 * Modified: 2026-01-31
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
#region System Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion
#region Project Libraries
#endregion 
namespace ToolsUI.Files
{
    public class FilerUIConfig
    {
        #region Public properties
        public string ProgramName { get; set; } = string.Empty;
        public string InitialDirectory { get; set; } = string.Empty;
        public string Filter { get; set; } = "All Files (*.*)|*.*";
        public bool MultiSelect { get; set; } = false;
        public bool RestoreDirectory { get; set; } = true;
        public bool AllowMultiDotExtensions { get; set; } = true;
        public string DataPath { get; set; } = string.Empty;
        public string LogPath { get; set; } = string.Empty;
        public string TextPath { get; set; } = string.Empty;


        #endregion
    }
}
