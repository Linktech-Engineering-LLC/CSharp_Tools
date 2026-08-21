/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Logging/FrmLogViewer.cs
 * File: FrmLogViewer.cs
 * Version: 1.0.1
 * Created: 2026-01-07
 * Modified: 2026-08-21
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
#region System Libraries
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.Logging;
#endregion
#region Product Libraries
using Tools.Helpers;
using Tools.Config;
using Tools.Logging;
using ToolsUI.Logging;
using ToolsUI.Files;
using System.Globalization;
using System.Security.Policy;
using ToolsUI.Helpers;
#endregion
namespace ToolsUI
{
    public partial class FrmLogViewer : Form
    {
        #region Private Classes/
        private class LogEntry
        {
            public string RawLine { get; set; } = "";
            public DateTime Timestamp { get; set; } = DateTime.MinValue;
            public string Domain { get; set; } = "";
            public string PID { get; set; } = "";
            public string Facility { get; set; } = "";
            public string Severity { get; set; } = "";
            public string Message { get; set; } = "";
        }
        #endregion
        #region Private Variables
        private readonly ArchiveBinder archiveBinder;
        private readonly UIHelperService uiHelper = new();
        private readonly OptionsBuilder optionsBinder;
        private readonly Logger lgr;
        private readonly LoggerConfig lgr_cfg;
        private readonly FilerUI filer;
        private List<LogEntry> _entries = [];
        private bool _inverted = false;
        private bool _isSyncing = false;
        #endregion
        #region Public Variables
        public string Found { get; set; } = string.Empty;
        public string Log { get; set; } = string.Empty;
        public long MaxLog { get; set; }
        public string Source { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        #endregion
        #region Private Methods
        private void DoArchive()
        {
            lgr.Archive(ArchiveType.Daily);
            rtbLogs.Clear();
            dgvLogs.Rows.Clear();
            DoRefresh();
        }
        private LogEntry CreateFallbackEntry(string line)
        {
            return new LogEntry
            {
                RawLine = line,
                Timestamp = DateTime.MinValue,
                Domain = PathHelpers.NameWithoutExtension(lgr.LogFileName),
                PID = "",
                Facility = "",
                Severity = "UNKNOWN",
                Message = "Unrecognized log format"
            };
        }
        private bool DetectStructuredFormat(string[] lines)
        {
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string tsCandidate = line.Length >= 19 ? line.Substring(0, 19).Trim() : "";

                if (DateTime.TryParse(tsCandidate, out _))
                    return true;
            }

            return false;
        }
        private void DoBrowse()
        {
            string? path = filer.SelectLogFile("Select Log File");
            if (string.IsNullOrEmpty(path))
                return;

            // Tell Logger to switch logs
            lgr.SetLogPath(path);

            // Refresh UI
            LoadLog();
            RefreshStructure();
            RefreshText();
            RefreshGrid();
        }
        private void DoEmpty()
        {
            //ForRawOrStructured(EmptyRaw, EmptyStructured);
        }
        private void DoInvert()
        {
            _inverted = !_inverted;

            if (HasValidTimestamps())
            {
                // Structured logs → invert by timestamp
                _entries = _inverted
                    ? _entries.OrderByDescending(e => e.Timestamp).ToList()
                    : _entries.OrderBy(e => e.Timestamp).ToList();
            }
            else
            {
                // Unknown format → invert by line order
                _entries.Reverse();
            }

            RefreshText();   // Raw Logs tab
            RefreshGrid();   // Structured Logs tab
        }
        private void DoRefresh()
        {
            // Refresh UI
            LoadLog();
            RefreshStructure();
            RefreshText();
            RefreshGrid();
        }
        private void DoSearch()
        {
            //ForRawOrStructured(SearchRaw, SearchStructured);
        }
        private void DgvLogs_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgvLogs.Rows[e.RowIndex];
            LogEntry? entry = row.DataBoundItem as LogEntry;
            if (entry == null)
                return;

            string sev = entry.Severity?.Trim().ToUpperInvariant() ?? "";

            // Normalize long names to short codes
            if (sev == "INFORMATIONAL")
                sev = "INFO";
            if (sev == "WARNING")
                sev = "WARN";

            switch (sev)
            {
                case "INFO":
                    row.DefaultCellStyle.ForeColor = Color.DimGray;
                    row.DefaultCellStyle.BackColor = Color.White;
                    break;

                case "WARN":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 245, 200);
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    break;

                case "ERROR":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    break;

                case "CRITICAL":
                    row.DefaultCellStyle.BackColor = Color.FromArgb(255, 180, 180);
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    row.DefaultCellStyle.Font = new Font(dgvLogs.Font, FontStyle.Bold);
                    break;

                case "AUDIT":
                    row.DefaultCellStyle.ForeColor = Color.RoyalBlue;
                    row.DefaultCellStyle.BackColor = Color.White;
                    break;

                default:
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    break;
            }
        }
        private void ForRawOrStructured(Action rawAction, Action structuredAction)
        {
            if (IsRawTab)
                rawAction();
            else
                structuredAction();
        }
        private bool HasValidTimestamps()
        {
            return _entries.Any(e => e.Timestamp > DateTime.MinValue);
        }
        private void HandleArchiveTypeItem(string optionText, CheckState newState)
        {
            // Only react when the item is being checked, not unchecked
            if (newState != CheckState.Checked)
                return;

            // Convert the option text to the enum
            if (Enum.TryParse(optionText, out ArchiveType type))
            {
                lgr_cfg.ArchiveType = type;
                return;
            }

            // If something unexpected happens
            lgr.Error($"{Product} LogViewer",
                $"Unknown ArchiveType option '{optionText}'");
        }
        private void HandleOptionsItem(string optionText, CheckState newState)
        {
            bool value = (newState == CheckState.Checked);

            switch (optionText)
            {
                case "Enable Compression":
                    //lgr_cfg.Options.Contains(LoggerOption.EnableCompression) ?  = value;
                    break;

                case "Include Raw Logs":
                    //lgr_cfg.IncludeRawLogs = value;
                    break;

                case "Verbose Archive Logging":
                    //lgr_cfg.VerboseArchiveLogging = value;
                    break;

                default:
                    lgr.Error($"{Product} LogViewer",
                        $"Unknown option '{optionText}' in Options list");
                    break;
            }
        }
        private bool IsRawTab => tabLogs.SelectedIndex == 0;
        private bool IsStructuredLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line) || line.Length < 19)
                return false;

            string tsCandidate = line.Substring(0, 19).Trim();

            return DateTime.TryParse(tsCandidate, out _);
        }
        private bool IsStructuredTab => tabLogs.SelectedIndex == 1;
        private void LoadLog()
        {
            _entries.Clear();

            string[] lines = lgr.ReadLog().Split('\n');

            // Detect whether this file uses the structured Medical format
            bool structured = DetectStructuredFormat(lines);

            foreach (string line in lines)
            {
                LogEntry? entry = null;

                if (IsStructuredLine(line))
                    entry = ParseLogLine(line);

                if (entry == null)
                    entry = CreateFallbackEntry(line);

                entry.RawLine = line.TrimEnd('\r', '\n');
                _entries.Add(entry);
            }
            // Inversion logic
            if (HasValidTimestamps())
            {
                _entries = _inverted
                    ? _entries.OrderByDescending(e => e.Timestamp).ToList()
                    : _entries.OrderBy(e => e.Timestamp).ToList();
            }
            else
            {
                _entries.Reverse();
            }
            lblFormatMode.Text = HasValidTimestamps() ? "Mode: Structured" : "Mode: Raw";
            lblParsedCount.Text = $"Parsed: {_entries.Count(e => e.Timestamp > DateTime.MinValue)}";
            lblFallbackCount.Text = $"Fallback: {_entries.Count(e => e.Timestamp == DateTime.MinValue)}";
        }
        private LogEntry? ParseLogLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                return null;

            line = line.Trim();

            if (line.Length < 20)
            {
                return new LogEntry
                {
                    Timestamp = DateTime.MinValue,
                    Domain = "",
                    PID = "",
                    Facility = "",
                    Severity = "",
                    Message = line
                };
            }

            // Extract timestamp (first 19 chars)
            string tsString = line.Substring(0, 19).Trim();
            DateTime ts;

            if (!DateTime.TryParseExact(
                    tsString,
                    "yyyy-MM-dd HH:mm:ss",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out ts))
            {
                ts = DateTime.MinValue;
            }

            // Find domain separator
            int dashIndex = line.IndexOf(" ", tsString.Length);
            if (dashIndex < 0)
                return null;

            // Extract domain
            int domainStart = dashIndex + 1;
            int levelStart = line.IndexOf(":[", domainStart);
            if (levelStart < 0)
                return null;

            string domain = line.Substring(domainStart, levelStart - domainStart).Trim();

            // Extract level block
            int levelEnd = line.IndexOf("]:", levelStart);
            if (levelEnd < 0)
                return null;

            string levelBlock = line.Substring(levelStart + 2, levelEnd - levelStart - 2).Trim();

            // Parse level block: "ID 29824 local7.Informational"
            string pid = "", facility = "", severity = "";
            string[] levelParts = levelBlock.Split(' ');
            if (levelParts.Length >= 3)
            {
                pid = levelParts[1].Trim();
                string[] facilityParts = levelParts[2].Split('.');
                if (facilityParts.Length == 2)
                {
                    facility = facilityParts[0].Trim();
                    severity = facilityParts[1].Trim();
                }
            }

            // Extract message
            string message = line.Substring(levelEnd + 2).Trim();

            return new LogEntry
            {
                Timestamp = ts,
                Domain = domain,
                PID = pid,
                Facility = facility,
                Severity = severity,
                Message = message
            };
        }
        private void RefreshGrid()
        {
            dgvLogs.DataSource = null;
            dgvLogs.DataSource = _entries;
        }
        private void RefreshStructure()
        {
            string log = lgr.FullLogPath;
            DateTime mod = File.Exists(log)
                ? new FileInfo(log).LastWriteTime
                : DateTime.MinValue;
            DateTime cd = File.Exists(log)
                ? new FileInfo(log).CreationTime
                : DateTime.MinValue;
            FileAttributes attrs = File.Exists(log)
                ? new FileInfo(log).Attributes
                : FileAttributes.Normal;
            List<string> flags = new();
            if (attrs.HasFlag(FileAttributes.ReadOnly))
                flags.Add("RO");
            else
                flags.Add("RW");
            if (attrs.HasFlag(FileAttributes.Hidden)) flags.Add("Hidden");
            if (attrs.HasFlag(FileAttributes.System)) flags.Add("System");
            if (attrs.HasFlag(FileAttributes.Archive)) flags.Add("Archive");
            if (attrs.HasFlag(FileAttributes.Compressed)) flags.Add("Compressed");
            string attrText = flags.Count > 0
                ? $"[{string.Join(", ", flags)}]"
                : "";
            long sze = 0;
            if (File.Exists(log))
                sze = new FileInfo(log).Length;

            txtLogPath.Text =
                $@"{log}
                (size: {sze / 1024:N0} KB
                 created: {cd.ToString("yyyy-MM-dd HH:mm:ss")}
                 modified: {mod.ToString("yyyy-MM-dd HH:mm:ss")}
                 attributes: {attrText})";
            Text = $"{Product} Application Log";
            uiHelper.UpdateMetadataLabels(pnlMetaTags, lgr_cfg);
        }
        private void RefreshText()
        {
            rtbLogs.Clear();
            foreach (var entry in _entries)
            {
                rtbLogs.AppendText(entry.RawLine + Environment.NewLine);
            }
        }
        private void ResizeArchiveTypesList()
        {
            int itemHeight = clbArchiveTypes.ItemHeight;
            int itemCount = clbArchiveTypes.Items.Count;

            // Add a little padding so it doesn't look cramped
            int padding = 4;

            clbArchiveTypes.Height = (itemHeight * itemCount) + padding;
        }
        private void SyncAll(LoggerConfig cfg)
        {
            archiveBinder.SyncConfigToUI(cfg, pnlArchives);
            optionsBinder.SyncConfigToUI(cfg, pnlOptions);
            uiHelper.UpdateMetadataLabels(pnlMetaTags, cfg);
        }
        #endregion
        #region Constructors/Destructors
        public FrmLogViewer(
            Tools.Logging.Logger logger,
            Tools.Config.LoggerConfig config,
            FilerUI filerUI,
            string appname)
        {
            InitializeComponent();

            dgvLogs.CellFormatting += DgvLogs_CellFormatting;
            dgvLogs.AutoGenerateColumns = false;

            // Injected backend
            lgr = logger;
            lgr_cfg = config;
            filer = filerUI;
            Product = appname;

            uiHelper = new UIHelperService();
            optionsBinder = new OptionsBuilder(uiHelper);
            archiveBinder = new ArchiveBinder(uiHelper);

            // Populate UI lists
            uiHelper.PopulateEnum<ArchiveType>(clbArchiveTypes);
            uiHelper.PopulateEnum<LoggerOption>(clbOptions);

            // Sync UI with config (replaces ALL old boolean-based code)
            SyncAll(lgr_cfg);

            ResizeArchiveTypesList();

            // Initial load of the log file
            LoadLog();
            RefreshStructure();
            RefreshText();
            RefreshGrid();
        }
        #endregion
        private void Button_Click(object sender, EventArgs e)
        {
            if (sender is not Button btn || btn.Tag is not string action)
                return;

            switch (action)
            {
                case "Browse":
                    DoBrowse();
                    break;

                case "Refresh":
                    DoRefresh();
                    break;

                case "Search":
                    //DoSearch();
                    break;

                case "Invert":
                    DoInvert();
                    break;

                case "Archive":
                    DoArchive();
                    break;

                case "Empty":
                    //DoEmpty();
                    break;

                case "Close":
                    Close();
                    break;
            }
        }
        private void CheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (_isSyncing)
                return;

            if (sender is not CheckedListBox clb)
                return;

            string tag = clb.Tag?.ToString() ?? "";
            string optionText = clb.Items[e.Index].ToString();

            _isSyncing = true;

            switch (tag)
            {
                case "LoggerOption":
                    {
                        var opt = Enum.Parse<LoggerOption>(optionText);

                        if (e.NewValue == CheckState.Checked)
                        {
                            if (!lgr_cfg.Options.Contains(opt))
                                lgr_cfg.Options.Add(opt);
                        }
                        else
                        {
                            lgr_cfg.Options.Remove(opt);
                        }

                        break;
                    }

                case "ArchiveType":
                    {
                        var at = Enum.Parse<ArchiveType>(optionText);
                        lgr_cfg.ArchiveType = at;

                        // enforce single-select safely
                        for (int i = 0; i < clb.Items.Count; i++)
                        {
                            if (i != e.Index)
                                clb.SetItemChecked(i, false);
                        }

                        break;
                    }

                default:
                    lgr.Error($"{Product} LogViewer",
                        $"Unknown CheckedListBox tag '{tag}' for option '{optionText}'");
                    break;
            }

            _isSyncing = false;

            RefreshStructure();
        }
        private void FrmLogViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            _entries?.Clear();
            _entries = null!;
            rtbLogs.Clear();
            dgvLogs.DataSource = null;
            dgvLogs.Rows.Clear();
            dgvLogs.Dispose();
            tabLogs.Dispose();
        }

    }
}
