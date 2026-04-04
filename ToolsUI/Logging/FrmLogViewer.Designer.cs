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
            btnClose = new Button();
            btnEmpty = new Button();
            btnSearch = new Button();
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
            pnlOptions = new Panel();
            clbOptions = new CheckedListBox();
            label1 = new Label();
            pnlArchives = new Panel();
            lblArchiveTypes = new Label();
            clbArchiveTypes = new CheckedListBox();
            pnlRightFlow = new FlowLayoutPanel();
            pnlMetaTags = new Panel();
            lblAutoRotate = new Label();
            lblVerbose = new Label();
            lblRawIncluded = new Label();
            lblCompression = new Label();
            lblArchiveType = new Label();
            grpFromatInfo = new GroupBox();
            lblFallbackCount = new Label();
            lblParsedCount = new Label();
            lblFormatMode = new Label();
            pnlControls.SuspendLayout();
            tabLogs.SuspendLayout();
            pgeGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            pgeRaw.SuspendLayout();
            pnlBottom.SuspendLayout();
            pnlOptions.SuspendLayout();
            pnlArchives.SuspendLayout();
            pnlRightFlow.SuspendLayout();
            pnlMetaTags.SuspendLayout();
            grpFromatInfo.SuspendLayout();
            SuspendLayout();
            // 
            // pnlControls
            // 
            pnlControls.Controls.Add(btnClose);
            pnlControls.Controls.Add(btnEmpty);
            pnlControls.Controls.Add(btnSearch);
            pnlControls.Controls.Add(btnRefresh);
            pnlControls.Controls.Add(btnArchive);
            pnlControls.Controls.Add(btnInvert);
            pnlControls.Controls.Add(btnBrowse);
            pnlControls.Dock = DockStyle.Top;
            pnlControls.Location = new Point(0, 0);
            pnlControls.Name = "pnlControls";
            pnlControls.Size = new Size(985, 46);
            pnlControls.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(561, 10);
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
            btnEmpty.Location = new Point(469, 10);
            btnEmpty.Name = "btnEmpty";
            btnEmpty.Size = new Size(75, 23);
            btnEmpty.TabIndex = 5;
            btnEmpty.Tag = "Empty";
            btnEmpty.Text = "&Empty";
            ttpLogViewer.SetToolTip(btnEmpty, "Empties/Clears the log");
            btnEmpty.UseVisualStyleBackColor = true;
            btnEmpty.Click += Button_Click;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(101, 10);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 23);
            btnSearch.TabIndex = 4;
            btnSearch.Tag = "Search";
            btnSearch.Text = "&Search";
            ttpLogViewer.SetToolTip(btnSearch, "Searches the log");
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += Button_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(193, 10);
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
            btnArchive.Location = new Point(377, 10);
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
            btnInvert.Location = new Point(285, 10);
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
            tabLogs.Location = new Point(0, 46);
            tabLogs.Name = "tabLogs";
            tabLogs.SelectedIndex = 0;
            tabLogs.Size = new Size(807, 509);
            tabLogs.TabIndex = 1;
            // 
            // pgeGrid
            // 
            pgeGrid.Controls.Add(dgvLogs);
            pgeGrid.Location = new Point(4, 24);
            pgeGrid.Name = "pgeGrid";
            pgeGrid.Padding = new Padding(3);
            pgeGrid.Size = new Size(799, 481);
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
            dgvLogs.Size = new Size(793, 475);
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
            pgeRaw.Size = new Size(977, 636);
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
            rtbLogs.Size = new Size(971, 630);
            rtbLogs.TabIndex = 0;
            rtbLogs.Text = "";
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(txtLogPath);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 555);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(985, 29);
            pnlBottom.TabIndex = 2;
            // 
            // txtLogPath
            // 
            txtLogPath.BorderStyle = BorderStyle.FixedSingle;
            txtLogPath.Dock = DockStyle.Bottom;
            txtLogPath.Location = new Point(0, 6);
            txtLogPath.Name = "txtLogPath";
            txtLogPath.ReadOnly = true;
            txtLogPath.Size = new Size(985, 23);
            txtLogPath.TabIndex = 0;
            // 
            // pnlOptions
            // 
            pnlOptions.AutoSize = true;
            pnlOptions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlOptions.Controls.Add(clbOptions);
            pnlOptions.Controls.Add(label1);
            pnlOptions.Location = new Point(3, 269);
            pnlOptions.Name = "pnlOptions";
            pnlOptions.Size = new Size(172, 118);
            pnlOptions.TabIndex = 10;
            // 
            // clbOptions
            // 
            clbOptions.FormattingEnabled = true;
            clbOptions.Location = new Point(0, 21);
            clbOptions.Name = "clbOptions";
            clbOptions.Size = new Size(169, 94);
            clbOptions.TabIndex = 1;
            clbOptions.Tag = "LoggerOption";
            clbOptions.ItemCheck += CheckedListBox_ItemCheck;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(59, 3);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 0;
            label1.Text = "Options";
            // 
            // pnlArchives
            // 
            pnlArchives.AutoSize = true;
            pnlArchives.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlArchives.Controls.Add(lblArchiveTypes);
            pnlArchives.Controls.Add(clbArchiveTypes);
            pnlArchives.Location = new Point(0, 0);
            pnlArchives.Margin = new Padding(0);
            pnlArchives.MinimumSize = new Size(175, 0);
            pnlArchives.Name = "pnlArchives";
            pnlArchives.Size = new Size(175, 154);
            pnlArchives.TabIndex = 9;
            // 
            // lblArchiveTypes
            // 
            lblArchiveTypes.AutoSize = true;
            lblArchiveTypes.Location = new Point(42, 3);
            lblArchiveTypes.Name = "lblArchiveTypes";
            lblArchiveTypes.Size = new Size(84, 15);
            lblArchiveTypes.TabIndex = 8;
            lblArchiveTypes.Text = "Archive Types";
            // 
            // clbArchiveTypes
            // 
            clbArchiveTypes.CheckOnClick = true;
            clbArchiveTypes.FormattingEnabled = true;
            clbArchiveTypes.Location = new Point(19, 21);
            clbArchiveTypes.Name = "clbArchiveTypes";
            clbArchiveTypes.Size = new Size(131, 130);
            clbArchiveTypes.TabIndex = 0;
            clbArchiveTypes.Tag = "ArchiveType";
            clbArchiveTypes.ItemCheck += CheckedListBox_ItemCheck;
            // 
            // pnlRightFlow
            // 
            pnlRightFlow.AutoSize = true;
            pnlRightFlow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlRightFlow.Controls.Add(pnlArchives);
            pnlRightFlow.Controls.Add(pnlMetaTags);
            pnlRightFlow.Controls.Add(pnlOptions);
            pnlRightFlow.Controls.Add(grpFromatInfo);
            pnlRightFlow.Dock = DockStyle.Right;
            pnlRightFlow.FlowDirection = FlowDirection.TopDown;
            pnlRightFlow.Location = new Point(807, 46);
            pnlRightFlow.MinimumSize = new Size(175, 0);
            pnlRightFlow.Name = "pnlRightFlow";
            pnlRightFlow.Size = new Size(178, 509);
            pnlRightFlow.TabIndex = 4;
            pnlRightFlow.WrapContents = false;
            // 
            // pnlMetaTags
            // 
            pnlMetaTags.Controls.Add(lblAutoRotate);
            pnlMetaTags.Controls.Add(lblVerbose);
            pnlMetaTags.Controls.Add(lblRawIncluded);
            pnlMetaTags.Controls.Add(lblCompression);
            pnlMetaTags.Controls.Add(lblArchiveType);
            pnlMetaTags.Location = new Point(0, 154);
            pnlMetaTags.Margin = new Padding(0);
            pnlMetaTags.Name = "pnlMetaTags";
            pnlMetaTags.Size = new Size(175, 112);
            pnlMetaTags.TabIndex = 11;
            // 
            // lblAutoRotate
            // 
            lblAutoRotate.AutoSize = true;
            lblAutoRotate.Location = new Point(22, 89);
            lblAutoRotate.Name = "lblAutoRotate";
            lblAutoRotate.Size = new Size(78, 15);
            lblAutoRotate.TabIndex = 4;
            lblAutoRotate.Tag = "AutoRotateLogs";
            lblAutoRotate.Text = "Auto Rotate:";
            // 
            // lblVerbose
            // 
            lblVerbose.AutoSize = true;
            lblVerbose.Location = new Point(22, 68);
            lblVerbose.Name = "lblVerbose";
            lblVerbose.Size = new Size(102, 15);
            lblVerbose.TabIndex = 3;
            lblVerbose.Tag = "VerboseArchiveLogging";
            lblVerbose.Text = "Verbose Logging:";
            // 
            // lblRawIncluded
            // 
            lblRawIncluded.AutoSize = true;
            lblRawIncluded.Location = new Point(22, 47);
            lblRawIncluded.Name = "lblRawIncluded";
            lblRawIncluded.Size = new Size(62, 15);
            lblRawIncluded.TabIndex = 2;
            lblRawIncluded.Tag = "IncludeRawLogs";
            lblRawIncluded.Text = "Raw Logs:";
            // 
            // lblCompression
            // 
            lblCompression.AutoSize = true;
            lblCompression.Location = new Point(22, 26);
            lblCompression.Name = "lblCompression";
            lblCompression.Size = new Size(81, 15);
            lblCompression.TabIndex = 1;
            lblCompression.Tag = "EnableCompression";
            lblCompression.Text = "Compression:";
            // 
            // lblArchiveType
            // 
            lblArchiveType.AutoSize = true;
            lblArchiveType.Location = new Point(22, 5);
            lblArchiveType.Name = "lblArchiveType";
            lblArchiveType.Size = new Size(82, 15);
            lblArchiveType.TabIndex = 0;
            lblArchiveType.Tag = "ArchiveType";
            lblArchiveType.Text = "Archive Type:";
            // 
            // grpFromatInfo
            // 
            grpFromatInfo.Controls.Add(lblFallbackCount);
            grpFromatInfo.Controls.Add(lblParsedCount);
            grpFromatInfo.Controls.Add(lblFormatMode);
            grpFromatInfo.Dock = DockStyle.Top;
            grpFromatInfo.Location = new Point(3, 393);
            grpFromatInfo.Name = "grpFromatInfo";
            grpFromatInfo.Padding = new Padding(0);
            grpFromatInfo.Size = new Size(172, 80);
            grpFromatInfo.TabIndex = 12;
            grpFromatInfo.TabStop = false;
            grpFromatInfo.Text = "Format Info";
            // 
            // lblFallbackCount
            // 
            lblFallbackCount.AutoSize = true;
            lblFallbackCount.Location = new Point(32, 55);
            lblFallbackCount.Name = "lblFallbackCount";
            lblFallbackCount.Size = new Size(64, 15);
            lblFallbackCount.TabIndex = 2;
            lblFallbackCount.Text = "Fallback: 0";
            // 
            // lblParsedCount
            // 
            lblParsedCount.AutoSize = true;
            lblParsedCount.Location = new Point(32, 36);
            lblParsedCount.Name = "lblParsedCount";
            lblParsedCount.Size = new Size(57, 15);
            lblParsedCount.TabIndex = 1;
            lblParsedCount.Text = "Parsed: 0";
            // 
            // lblFormatMode
            // 
            lblFormatMode.AutoSize = true;
            lblFormatMode.Location = new Point(32, 17);
            lblFormatMode.Name = "lblFormatMode";
            lblFormatMode.Size = new Size(99, 15);
            lblFormatMode.TabIndex = 0;
            lblFormatMode.Text = "Mode: Unknown";
            // 
            // FrmLogViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(985, 584);
            Controls.Add(tabLogs);
            Controls.Add(pnlRightFlow);
            Controls.Add(pnlControls);
            Controls.Add(pnlBottom);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Name = "FrmLogViewer";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Log Viewer";
            FormClosing += FrmLogViewer_FormClosing;
            pnlControls.ResumeLayout(false);
            tabLogs.ResumeLayout(false);
            pgeGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            pgeRaw.ResumeLayout(false);
            pnlBottom.ResumeLayout(false);
            pnlBottom.PerformLayout();
            pnlOptions.ResumeLayout(false);
            pnlOptions.PerformLayout();
            pnlArchives.ResumeLayout(false);
            pnlArchives.PerformLayout();
            pnlRightFlow.ResumeLayout(false);
            pnlRightFlow.PerformLayout();
            pnlMetaTags.ResumeLayout(false);
            pnlMetaTags.PerformLayout();
            grpFromatInfo.ResumeLayout(false);
            grpFromatInfo.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private CheckedListBox clbArchiveTypes;
        private Panel pnlArchives;
        private Label lblArchiveTypes;
        private Panel pnlOptions;
        private Label label1;
        private CheckedListBox clbOptions;
        private FlowLayoutPanel pnlRightFlow;
        private Panel pnlMetaTags;
        private Label lblVerbose;
        private Label lblRawIncluded;
        private Label lblCompression;
        private Label lblArchiveType;
        private Label lblAutoRotate;
        private GroupBox grpFromatInfo;
        private Label lblFallbackCount;
        private Label lblParsedCount;
        private Label lblFormatMode;
    }
}
