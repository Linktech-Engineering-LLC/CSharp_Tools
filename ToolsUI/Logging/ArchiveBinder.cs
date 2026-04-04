/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Logging/ArchiveBinder.cs
 * File: ArchiveBinder.cs
 * Created: None
 * Modified: 2026-04-01
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
using ToolsUI.Helpers;
#endregion
#region Project Libraries
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
