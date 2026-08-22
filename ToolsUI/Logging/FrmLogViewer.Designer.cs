/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Logging/FrmLogViewer.Designer.cs
 * File: FrmLogViewer.Designer.cs
 * Created: 2026-01-07
 * Modified: 2026-03-31
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */


namespace ToolsUI
{
    partial class FrmLogViewer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlControls = new Panel();
            pnlSearch = new Panel();
            btnPrevious = new Button();
            btnNext = new Button();
            btnClearFilter = new Button();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnClose = new Button();
            btnEmpty = new Button();
            btnRefresh = new Button();
            btnArchive = new Button();
            btnInvert = new Button();
            btnBrowse = new Button();
            tabLogs = new TabControl();
            pgeGrid = new TabPage();
            dgvLogs = new DataGridView();
            colTimestamp = new DataGridViewTextBoxColumn();
            colDomain = new DataGridViewTextBoxColumn();
            colPID = new DataGridViewTextBoxColumn();
            colFacility = new DataGridViewTextBoxColumn();
            colSeverity = new DataGridViewTextBoxColumn();
            colMessage = new DataGridViewTextBoxColumn();
            pgeRaw = new TabPage();
            rtbLogs = new RichTextBox();
            ttpLogViewer = new ToolTip(components);
            pnlBottom = new Panel();
            txtLogPath = new TextBox();
            clbSearchOptions = new CheckedListBox();
            pnlControls.SuspendLayout();
            pnlSearch.SuspendLayout();
            tabLogs.SuspendLayout();
            pgeGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            pgeRaw.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // pnlControls
            // 
            pnlControls.Controls.Add(clbSearchOptions);
            pnlControls.Controls.Add(pnlSearch);
            pnlControls.Controls.Add(btnClose);
            pnlControls.Controls.Add(btnEmpty);
            pnlControls.Controls.Add(btnRefresh);
            pnlControls.Controls.Add(btnArchive);
            pnlControls.Controls.Add(btnInvert);
            pnlControls.Controls.Add(btnBrowse);
            pnlControls.Dock = DockStyle.Top;
            pnlControls.Location = new Point(0, 0);
            pnlControls.Name = "pnlControls";
            pnlControls.Size = new Size(759, 114);
            pnlControls.TabIndex = 0;
            // 
            // pnlSearch
            // 
            pnlSearch.Controls.Add(btnPrevious);
            pnlSearch.Controls.Add(btnNext);
            pnlSearch.Controls.Add(btnClearFilter);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(btnSearch);
            pnlSearch.Location = new Point(9, 40);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(615, 36);
            pnlSearch.TabIndex = 7;
            // 
            // btnPrevious
            // 
            btnPrevious.Location = new Point(528, 6);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(75, 23);
            btnPrevious.TabIndex = 8;
            btnPrevious.Tag = "FindPrev";
            btnPrevious.Text = "Previous";
            ttpLogViewer.SetToolTip(btnPrevious, "Finds Previous Occurrence");
            btnPrevious.UseVisualStyleBackColor = true;
            btnPrevious.Click += Button_Click;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(444, 6);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(75, 23);
            btnNext.TabIndex = 7;
            btnNext.Tag = "FindNext";
            btnNext.Text = "Next";
            ttpLogViewer.SetToolTip(btnNext, "Finds Next Occurrence");
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += Button_Click;
            // 
            // btnClearFilter
            // 
            btnClearFilter.Location = new Point(363, 6);
            btnClearFilter.Name = "btnClearFilter";
            btnClearFilter.Size = new Size(75, 23);
            btnClearFilter.TabIndex = 6;
            btnClearFilter.Tag = "ClearFilter";
            btnClearFilter.Text = "Clear Filter";
            ttpLogViewer.SetToolTip(btnClearFilter, "Clears the Filter");
            btnClearFilter.UseVisualStyleBackColor = true;
            btnClearFilter.Click += Button_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(85, 6);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(272, 23);
            txtSearch.TabIndex = 5;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(3, 6);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 23);
            btnSearch.TabIndex = 4;
            btnSearch.Tag = "Search";
            btnSearch.Text = "&Search";
            ttpLogViewer.SetToolTip(btnSearch, "Searches the log");
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += Button_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(459, 10);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 6;
            btnClose.Tag = "Close";
            btnClose.Text = "&Close";
            ttpLogViewer.SetToolTip(btnClose, "Closes the Log Viewer");
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += Button_Click;
            // 
            // btnEmpty
            // 
            btnEmpty.Location = new Point(367, 10);
            btnEmpty.Name = "btnEmpty";
            btnEmpty.Size = new Size(75, 23);
            btnEmpty.TabIndex = 5;
            btnEmpty.Tag = "Empty";
            btnEmpty.Text = "&Empty";
            ttpLogViewer.SetToolTip(btnEmpty, "Empties/Clears the log");
            btnEmpty.UseVisualStyleBackColor = true;
            btnEmpty.Click += Button_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(91, 10);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 23);
            btnRefresh.TabIndex = 3;
            btnRefresh.Tag = "Refresh";
            btnRefresh.Text = "&Refresh";
            ttpLogViewer.SetToolTip(btnRefresh, "Refreshes the Log");
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += Button_Click;
            // 
            // btnArchive
            // 
            btnArchive.Location = new Point(275, 10);
            btnArchive.Name = "btnArchive";
            btnArchive.Size = new Size(75, 23);
            btnArchive.TabIndex = 2;
            btnArchive.Tag = "Archive";
            btnArchive.Text = "&Archive";
            ttpLogViewer.SetToolTip(btnArchive, "Archives the log");
            btnArchive.UseVisualStyleBackColor = true;
            btnArchive.Click += Button_Click;
            // 
            // btnInvert
            // 
            btnInvert.Location = new Point(183, 10);
            btnInvert.Name = "btnInvert";
            btnInvert.Size = new Size(75, 23);
            btnInvert.TabIndex = 1;
            btnInvert.Tag = "Invert";
            btnInvert.Text = "&Invert";
            ttpLogViewer.SetToolTip(btnInvert, "Toggles between oldest to newest and newest to oldest records in the log");
            btnInvert.UseVisualStyleBackColor = true;
            btnInvert.Click += Button_Click;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(9, 10);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 0;
            btnBrowse.Tag = "Browse";
            btnBrowse.Text = "&Browse";
            ttpLogViewer.SetToolTip(btnBrowse, "Browses the file system to find a log file to open");
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += Button_Click;
            // 
            // tabLogs
            // 
            tabLogs.Controls.Add(pgeGrid);
            tabLogs.Controls.Add(pgeRaw);
            tabLogs.Dock = DockStyle.Fill;
            tabLogs.Location = new Point(0, 114);
            tabLogs.Name = "tabLogs";
            tabLogs.SelectedIndex = 0;
            tabLogs.Size = new Size(759, 441);
            tabLogs.TabIndex = 1;
            // 
            // pgeGrid
            // 
            pgeGrid.Controls.Add(dgvLogs);
            pgeGrid.Location = new Point(4, 24);
            pgeGrid.Name = "pgeGrid";
            pgeGrid.Padding = new Padding(3);
            pgeGrid.Size = new Size(751, 413);
            pgeGrid.TabIndex = 1;
            pgeGrid.Text = "Structured Logs";
            pgeGrid.UseVisualStyleBackColor = true;
            // 
            // dgvLogs
            // 
            dgvLogs.AllowUserToAddRows = false;
            dgvLogs.AllowUserToDeleteRows = false;
            dgvLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLogs.Columns.AddRange(new DataGridViewColumn[] { colTimestamp, colDomain, colPID, colFacility, colSeverity, colMessage });
            dgvLogs.Dock = DockStyle.Fill;
            dgvLogs.Location = new Point(3, 3);
            dgvLogs.Name = "dgvLogs";
            dgvLogs.ReadOnly = true;
            dgvLogs.Size = new Size(745, 407);
            dgvLogs.TabIndex = 0;
            // 
            // colTimestamp
            // 
            colTimestamp.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colTimestamp.DataPropertyName = "Timestamp";
            colTimestamp.HeaderText = "Timestamp";
            colTimestamp.Name = "colTimestamp";
            colTimestamp.ReadOnly = true;
            colTimestamp.Width = 125;
            // 
            // colDomain
            // 
            colDomain.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colDomain.DataPropertyName = "Domain";
            colDomain.HeaderText = "Domain";
            colDomain.Name = "colDomain";
            colDomain.ReadOnly = true;
            colDomain.Width = 75;
            // 
            // colPID
            // 
            colPID.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colPID.DataPropertyName = "PID";
            colPID.HeaderText = "PID";
            colPID.Name = "colPID";
            colPID.ReadOnly = true;
            colPID.Width = 52;
            // 
            // colFacility
            // 
            colFacility.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colFacility.DataPropertyName = "Facility";
            colFacility.HeaderText = "Facility";
            colFacility.Name = "colFacility";
            colFacility.ReadOnly = true;
            colFacility.Width = 70;
            // 
            // colSeverity
            // 
            colSeverity.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colSeverity.DataPropertyName = "Severity";
            colSeverity.HeaderText = "Severity";
            colSeverity.Name = "colSeverity";
            colSeverity.ReadOnly = true;
            // 
            // colMessage
            // 
            colMessage.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colMessage.DataPropertyName = "Message";
            colMessage.HeaderText = "Message";
            colMessage.Name = "colMessage";
            colMessage.ReadOnly = true;
            colMessage.Width = 1000;
            // 
            // pgeRaw
            // 
            pgeRaw.Controls.Add(rtbLogs);
            pgeRaw.Location = new Point(4, 24);
            pgeRaw.Name = "pgeRaw";
            pgeRaw.Padding = new Padding(3);
            pgeRaw.Size = new Size(670, 445);
            pgeRaw.TabIndex = 0;
            pgeRaw.Text = "Raw Logs";
            pgeRaw.UseVisualStyleBackColor = true;
            // 
            // rtbLogs
            // 
            rtbLogs.Dock = DockStyle.Fill;
            rtbLogs.Location = new Point(3, 3);
            rtbLogs.Name = "rtbLogs";
            rtbLogs.ReadOnly = true;
            rtbLogs.Size = new Size(664, 439);
            rtbLogs.TabIndex = 0;
            rtbLogs.Text = "";
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(txtLogPath);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 555);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(759, 29);
            pnlBottom.TabIndex = 2;
            // 
            // txtLogPath
            // 
            txtLogPath.BorderStyle = BorderStyle.FixedSingle;
            txtLogPath.Dock = DockStyle.Bottom;
            txtLogPath.Location = new Point(0, 6);
            txtLogPath.Name = "txtLogPath";
            txtLogPath.ReadOnly = true;
            txtLogPath.Size = new Size(759, 23);
            txtLogPath.TabIndex = 0;
            // 
            // clbSearchOptions
            // 
            clbSearchOptions.CheckOnClick = true;
            clbSearchOptions.FormattingEnabled = true;
            clbSearchOptions.Items.AddRange(new object[] { "Case Sensitive", "Regex", "Highlight All", "Whole Word", "Multiline" });
            clbSearchOptions.Location = new Point(630, 3);
            clbSearchOptions.Name = "clbSearchOptions";
            clbSearchOptions.Size = new Size(120, 94);
            clbSearchOptions.TabIndex = 8;
            ttpLogViewer.SetToolTip(clbSearchOptions, "Search Options");
            // 
            // FrmLogViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(759, 584);
            Controls.Add(tabLogs);
            Controls.Add(pnlControls);
            Controls.Add(pnlBottom);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Name = "FrmLogViewer";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Log Viewer";
            FormClosing += FrmLogViewer_FormClosing;
            pnlControls.ResumeLayout(false);
            pnlSearch.ResumeLayout(false);
            pnlSearch.PerformLayout();
            tabLogs.ResumeLayout(false);
            pgeGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            pgeRaw.ResumeLayout(false);
            pnlBottom.ResumeLayout(false);
            pnlBottom.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlControls;
        private TabControl tabLogs;
        private TabPage pgeRaw;
        private RichTextBox rtbLogs;
        private TabPage pgeGrid;
        private Button btnSearch;
        private Button btnRefresh;
        private Button btnArchive;
        private Button btnInvert;
        private Button btnBrowse;
        private DataGridView dgvLogs;
        private Button btnClose;
        private Button btnEmpty;
        private ToolTip ttpLogViewer;
        private Panel pnlBottom;
        private TextBox txtLogPath;
        private DataGridViewTextBoxColumn colTimestamp;
        private DataGridViewTextBoxColumn colDomain;
        private DataGridViewTextBoxColumn colPID;
        private DataGridViewTextBoxColumn colFacility;
        private DataGridViewTextBoxColumn colSeverity;
        private DataGridViewTextBoxColumn colMessage;
        private Panel pnlSearch;
        private TextBox txtSearch;
        private Button btnClearFilter;
        private Button btnNext;
        private Button btnPrevious;
        private CheckedListBox clbSearchOptions;
    }
}
