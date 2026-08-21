/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Logging/ArchiveBinder.cs
 * File: ArchiveBinder.cs
 * Version: 1.0.2
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
#endregion
#region Project Libraries
using Tools.Config;
using Tools.Logging;
using ToolsUI.Helpers;
#endregion
namespace ToolsUI.Logging
{
    public class ArchiveBinder
    {
        #region Private Variables
        private readonly UIHelperService uiHelper;
        #endregion
        #region Constructors/Destructors
        public ArchiveBinder(UIHelperService helper)
        {
            uiHelper = helper;
        }
        #endregion
        #region Public Methods

        public void SyncConfigToUI(LoggerConfig cfg, Control root)
        {
            foreach (ArchiveType at in Enum.GetValues(typeof(ArchiveType)))
            {
                var (clb, idx) = uiHelper.GetCheckListBoxIndex(root, "ArchiveType", at.ToString());
                if (clb != null && idx >= 0)
                    clb.SetItemChecked(idx, cfg.ArchiveType == at);
            }
        }
        public void SyncUIToConfig(LoggerConfig cfg, Control root)
        {
            foreach (ArchiveType at in Enum.GetValues(typeof(ArchiveType)))
            {
                var (clb, idx) = uiHelper.GetCheckListBoxIndex(root, "ArchiveTypes", at.ToString());
                if (clb != null && idx >= 0 && clb.GetItemChecked(idx))
                {
                    cfg.ArchiveType = at;
                    break;
                }
            }
        }
        #endregion
    }
}
