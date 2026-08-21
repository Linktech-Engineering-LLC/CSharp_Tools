/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Logging/OptionsBuilder.cs
 * File: OptionsBuilder.cs
 * Version: 1.0.1
 * Created: None
 * Modified: 2026-08-21
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
using Tools.Logging;
#endregion
#region Project Libraries
using Tools.Config;
using Tools.Logging;
using ToolsUI.Helpers;
#endregion
namespace ToolsUI.Logging
{
    public class OptionsBuilder
    {
        #region Private Variables
        private readonly UIHelperService uiHelper;
        #endregion
        #region Constructors/Destructors
        public OptionsBuilder(UIHelperService helper)
        {
            uiHelper = helper;
        }
        #endregion
        #region Public Properties
        #endregion
        #region Public Methods
        public void SyncConfigToUI(LoggerConfig cfg, Control root)
        {
            foreach (LoggerOption opt in Enum.GetValues(typeof(LoggerOption)))
            {
                var (clb, idx) = uiHelper.GetCheckListBoxIndex(root, "LoggerOption", opt.ToString());
                if (clb != null && idx >= -1)
                    clb.SetItemChecked(idx, cfg.Options.Contains(opt));
            }
        }
        public void SyncUIToConfig(LoggerConfig cfg, Control root)
        {
            cfg.Options.Clear();

            foreach (LoggerOption opt in Enum.GetValues(typeof(LoggerOption)))
            {
                var (clb, idx) = uiHelper.GetCheckListBoxIndex(root, "Options", opt.ToString());
                if (clb != null && idx >= 0 && clb.GetItemChecked(idx))
                    cfg.Options.Add(opt);
            }
        }
        public void HandleItemCheck(LoggerConfig cfg, Control root, ItemCheckEventArgs e)
        {
            var (clb, idx) = uiHelper.GetCheckListBoxIndex(root, "Options", "Enable Compression");
            if (e.Index == idx)
            {
                if (e.NewValue == CheckState.Checked)
                    cfg.Options.Add(LoggerOption.EnableCompression);
                else
                    cfg.Options.Remove(LoggerOption.EnableCompression);

                //Logger.Instance.Info("LogViewer", LogLevel.Info,
                  //  $"Compression setting changed to: {e.NewValue == CheckState.Checked}");
            }

            // Add similar blocks for other options if you want live updates
        }
        #endregion
    }
}
