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
using System.Security.Policy;
using System.Windows.Forms;
using Microsoft.VisualBasic.Logging;
#endregion
#region Product Libraries
using Tools.Helpers;
using Tools.Config;
using Tools.Logging;
using ToolsUI.Files;
using System.Globalization;
using ToolsUI.Helpers;
using System.Text.RegularExpressions;
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
        private readonly UIHelperService uiHelper = new();
        private readonly Logger lgr;
        private readonly LoggerConfig lgr_cfg;
        private readonly FilerUI filer;
        private List<LogEntry> _entries = [];
        private bool _inverted = false;
        private List<int> _rawMatches = new();
        private int _rawMatchIndex = -1;
        private readonly Color HighlightColor = Color.Yellow;
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
            string term = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(term))
                return;

            // RAW TAB
            if (tabLogs.SelectedTab.Equals(pgeRaw))
            {
                ClearRawHighlights();
                SearchRaw(term);
                HighlightAllRawMatches(term);
                return;
            }

            // STRUCTURED TAB
            SearchStructured(term);
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
        private bool HasValidTimestamps()
        {
            return _entries.Any(e => e.Timestamp > DateTime.MinValue);
        }
        private bool IsStructuredLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line) || line.Length < 19)
                return false;

            string tsCandidate = line.Substring(0, 19).Trim();

            return DateTime.TryParse(tsCandidate, out _);
        }
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
        }
        private void RefreshText()
        {
            rtbLogs.Clear();
            foreach (var entry in _entries)
            {
                rtbLogs.AppendText(entry.RawLine + Environment.NewLine);
            }
        }
        private bool SearchOption(string name)
        {
            foreach (var item in clbSearchOptions.CheckedItems)
            {
                if (item.ToString() == name)
                    return true;
            }
            return false;
        }
        private void SearchRaw(string term)
        {
            _rawMatches.Clear();
            _rawMatchIndex = -1;

            bool caseSensitive = SearchOption("Case Sensitive");
            bool regex = SearchOption("Regex");
            bool highlightAll = SearchOption("Highlight All");

            string text = rtbLogs.Text;

            if (regex)
            {
                var matches = Regex.Matches(
                    text,
                    term,
                    caseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase);

                foreach (Match m in matches)
                    _rawMatches.Add(m.Index);
            }
            else
            {
                StringComparison cmp = caseSensitive
                    ? StringComparison.Ordinal
                    : StringComparison.OrdinalIgnoreCase;

                int index = 0;
                while (true)
                {
                    index = text.IndexOf(term, index, cmp);
                    if (index < 0)
                        break;

                    _rawMatches.Add(index);
                    index += term.Length;
                }
            }

            if (_rawMatches.Count == 0)
                return;

            _rawMatchIndex = 0;

            if (highlightAll)
                HighlightAllRawMatches(term);

            HighlightRawMatch();
        }
        private void ClearRawHighlights()
        {
            rtbLogs.Select(0, rtbLogs.Text.Length);
            rtbLogs.SelectionBackColor = Color.White;

            _rawMatches.Clear();
            _rawMatchIndex = -1;
        }
        private void HighlightAllRawMatches(string term)
        {
            rtbLogs.Select(0, rtbLogs.Text.Length);
            rtbLogs.SelectionBackColor = Color.White;

            foreach (int index in _rawMatches)
            {
                rtbLogs.Select(index, term.Length);
                rtbLogs.SelectionBackColor = HighlightColor;
            }

            HighlightRawMatch();
        }
        private void HighlightRawMatch()
        {
            if (_rawMatchIndex < 0 || _rawMatchIndex >= _rawMatches.Count)
                return;

            int start = _rawMatches[_rawMatchIndex];
            int length = txtSearch.Text.Length;

            rtbLogs.Select(start, length);
            rtbLogs.ScrollToCaret();
            rtbLogs.Focus();
        }
        private void FindNextRaw()
        {
            if (_rawMatches.Count == 0)
                return;

            _rawMatchIndex++;
            if (_rawMatchIndex >= _rawMatches.Count)
                _rawMatchIndex = 0;   // wrap around

            HighlightRawMatch();
        }
        private void FindPrevRaw()
        {
            if (_rawMatches.Count == 0)
                return;

            _rawMatchIndex--;
            if (_rawMatchIndex < 0)
                _rawMatchIndex = _rawMatches.Count - 1;   // wrap around

            HighlightRawMatch();
        }
        private void SearchStructured(string term)
        {
            // Wildcard reset
            if (term == "*")
            {
                dgvLogs.DataSource = _entries;
                return;
            }

            var matches = _entries
                .Where(e => e.RawLine.Contains(term, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matches.Count == 0)
                return;

            if (matches.Count == 1)
            {
                var entry = matches[0];
                int index = _entries.IndexOf(entry);

                dgvLogs.DataSource = _entries;   // ensure full list is shown
                dgvLogs.ClearSelection();
                dgvLogs.Rows[index].Selected = true;
                dgvLogs.FirstDisplayedScrollingRowIndex = index;
                return;
            }

            // Multiple matches → filter grid
            dgvLogs.DataSource = matches;
        }
        private void ClearFilter()
        {
            // Restore full list
            dgvLogs.DataSource = _entries;

            // Optional: clear search box
            txtSearch.Text = string.Empty;

            // Optional: clear selection
            dgvLogs.ClearSelection();
            _rawMatches.Clear();
            _rawMatchIndex = -1;

            rtbLogs.Select(0, 0);   // remove highlight
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
                    DoSearch();
                    break;
                case "ClearFilter":
                    ClearFilter();
                    ClearRawHighlights();
                    break;
                case "FindNext":
                    FindNextRaw();
                    break;
                case "FindPrev":
                    FindPrevRaw();
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
