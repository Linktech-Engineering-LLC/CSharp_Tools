/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Helpers/UIHelperService.cs
 * File: UIHelperService.cs
 * Created: 2026-03-31
 * Modified: 2026-04-01
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
#region System Libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Tools.Logging;
using ToolsUI.Logging;
#endregion
#region Project Libraries
#endregion
namespace ToolsUI.Helpers
{
    public class UIHelperService
    {
        #region Private Properties
        #endregion
        #region Constructors/Destructors
        public UIHelperService()
        {
        }
        #endregion
        #region Public Properties
        #endregion
        #region Public Methods
        public void ClearBoxIfDisabled(TextBox box)
        {
            if(!box.Enabled)
                box.Text = string.Empty;
        }
        public IEnumerable<CheckedListBox> FindCheckedListBoxes(Control root, string tag)
        {
            return GetAllControls<CheckedListBox>(root)
                .Where(clb => string.Equals(clb.Tag?.ToString(), tag, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Label> FindLabels(Control parent, string tag)
        {
            foreach (var lbl in GetAllControls<Label>(parent))
            {
                if (string.Equals(lbl.Tag?.ToString(), tag, StringComparison.OrdinalIgnoreCase))
                    yield return lbl;
            }
        }
        public IEnumerable<T> GetAllControls<T>(Control root) where T : Control
        {
            foreach (Control c in root.Controls)
            {
                if (c is T match)
                    yield return match;

                foreach (var child in GetAllControls<T>(c))
                    yield return child;
            }
        }
        public (CheckedListBox clb, int index) GetCheckListBoxIndex(Control root, string tag, string item)
        {
            foreach (var clb in GetAllControls<CheckedListBox>(root))
            {
                if (clb.Tag?.ToString() == tag)
                {
                    int idx = -1;
                    for (int i = 0; i < clb.Items.Count; i++)
                    {
                        if (string.Equals(clb.Items[i]?.ToString(), item, StringComparison.OrdinalIgnoreCase))
                        {
                            idx = i;
                            break;
                        }
                    }
                    return (clb, idx);
                }
            }

            return (null, -1);
        }
        public void UpdateMetadataLabels(Control parent, LoggerConfig cfg)
        {
            foreach (Label lbl in GetAllControls<Label>(parent))
            {
                string? tag = lbl.Tag?.ToString();
                if (string.IsNullOrEmpty(tag))
                    continue;

                if (tag == "ArchiveType")
                {
                    lbl.Text = $"Archive Type: {cfg.ArchiveType}";
                    continue;
                }

                if (Enum.TryParse(tag, out LoggerOption opt))
                {
                    bool enabled = cfg.Options.Contains(opt);

                    lbl.Text = opt switch
                    {
                        LoggerOption.EnableCompression => enabled ? "Compression: On" : "Compression: Off",
                        LoggerOption.IncludeRawLogs => enabled ? "Raw Logs: Included" : "Raw Logs: Excluded",
                        LoggerOption.VerboseArchiveLogging => enabled ? "Verbose Logging: On" : "Verbose Logging: Off",
                        LoggerOption.AutoRotateLogs => enabled ? "Auto-Rotate: On" : "Auto-Rotate: Off",
                        _ => lbl.Text
                    };
                }
            }
        }

        public void PopulateEnum<T>(CheckedListBox list) where T : Enum
        {
            list.Items.Clear();
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                list.Items.Add(value);
            }
            list.CheckOnClick = true;
        }

        public void PopulateList(ListBox list, IEnumerable<string> items)
        {
            list.Items.Clear();
            foreach (var item in items)
            {
                list.Items.Add(item);
            }
        }

        public void PopulateCombo<T>(ComboBox combo) where T : Enum
        {
            combo.Items.Clear();
            foreach (var value in Enum.GetValues(typeof(T)))
                combo.Items.Add(value);
        }
        public void SetOptionChecked(Control parent, string tag, string optionName, bool value)
        {
            var (clb, idx) = GetCheckListBoxIndex(parent, tag, optionName);
            if (clb != null && idx >= 0)
                clb.SetItemChecked(idx, value);
        }
        #endregion
    }
}
