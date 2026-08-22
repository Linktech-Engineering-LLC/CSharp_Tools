/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/UserControls/UcLoggerEditor.Designer.cs
 * File: UcLoggerEditor.Designer.cs
 * Version: 1.0.4
 * Created: 2026-06-05
 * Modified: 2026-08-22
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
namespace ToolsUI.UserControls
{
    partial class UcLoggerEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlRoot = new Panel();
            btnUpdate = new Button();
            grpLocations = new GroupBox();
            txtArchiveDirectory = new TextBox();
            label3 = new Label();
            txtRotateDirectory = new TextBox();
            label2 = new Label();
            txtLogDirectory = new TextBox();
            label1 = new Label();
            grpOptions = new GroupBox();
            clbOptions = new CheckedListBox();
            grpRotation = new GroupBox();
            numRetention = new ToolsUI.Controls.NumericTextBox();
            numMaxLogSize = new ToolsUI.Controls.NumericTextBox();
            cboRetention = new ComboBox();
            cboSize = new ComboBox();
            cboArchiveType = new ComboBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            cboRotationType = new ComboBox();
            label5 = new Label();
            grpBehavior = new GroupBox();
            cboMinimumLevel = new ComboBox();
            label4 = new Label();
            toolTip1 = new ToolTip(components);
            pnlRoot.SuspendLayout();
            grpLocations.SuspendLayout();
            grpOptions.SuspendLayout();
            grpRotation.SuspendLayout();
            grpBehavior.SuspendLayout();
            SuspendLayout();
            // 
            // pnlRoot
            // 
            pnlRoot.AutoSize = true;
            pnlRoot.Controls.Add(btnUpdate);
            pnlRoot.Controls.Add(grpLocations);
            pnlRoot.Controls.Add(grpOptions);
            pnlRoot.Controls.Add(grpRotation);
            pnlRoot.Controls.Add(grpBehavior);
            pnlRoot.Location = new Point(3, 3);
            pnlRoot.Name = "pnlRoot";
            pnlRoot.Size = new Size(466, 367);
            pnlRoot.TabIndex = 0;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(194, 328);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 4;
            btnUpdate.Text = "&Update";
            toolTip1.SetToolTip(btnUpdate, "Updates the Logger Information");
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // grpLocations
            // 
            grpLocations.Controls.Add(txtArchiveDirectory);
            grpLocations.Controls.Add(label3);
            grpLocations.Controls.Add(txtRotateDirectory);
            grpLocations.Controls.Add(label2);
            grpLocations.Controls.Add(txtLogDirectory);
            grpLocations.Controls.Add(label1);
            grpLocations.Location = new Point(4, 4);
            grpLocations.Name = "grpLocations";
            grpLocations.Size = new Size(455, 119);
            grpLocations.TabIndex = 0;
            grpLocations.TabStop = false;
            grpLocations.Text = "Log Locations";
            // 
            // txtArchiveDirectory
            // 
            txtArchiveDirectory.Location = new Point(125, 78);
            txtArchiveDirectory.Name = "txtArchiveDirectory";
            txtArchiveDirectory.ReadOnly = true;
            txtArchiveDirectory.ScrollBars = ScrollBars.Horizontal;
            txtArchiveDirectory.Size = new Size(324, 23);
            txtArchiveDirectory.TabIndex = 5;
            toolTip1.SetToolTip(txtArchiveDirectory, "Log Archive Directory\r\nDefaults to Archives under Log Directory\r\nClicking Opens the Path Editor");
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 82);
            label3.Name = "label3";
            label3.Size = new Size(106, 15);
            label3.TabIndex = 4;
            label3.Text = "Archive Directory";
            // 
            // txtRotateDirectory
            // 
            txtRotateDirectory.Location = new Point(124, 47);
            txtRotateDirectory.Name = "txtRotateDirectory";
            txtRotateDirectory.ReadOnly = true;
            txtRotateDirectory.ScrollBars = ScrollBars.Horizontal;
            txtRotateDirectory.Size = new Size(324, 23);
            txtRotateDirectory.TabIndex = 3;
            toolTip1.SetToolTip(txtRotateDirectory, "Log Rotation Directory,\r\nDefaults to Rotate under Log Directory\r\nClicking opens the Path Editor");
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 51);
            label2.Name = "label2";
            label2.Size = new Size(101, 15);
            label2.TabIndex = 2;
            label2.Text = "Rotate Directory";
            // 
            // txtLogDirectory
            // 
            txtLogDirectory.Location = new Point(124, 18);
            txtLogDirectory.Name = "txtLogDirectory";
            txtLogDirectory.ReadOnly = true;
            txtLogDirectory.ScrollBars = ScrollBars.Horizontal;
            txtLogDirectory.Size = new Size(324, 23);
            txtLogDirectory.TabIndex = 1;
            toolTip1.SetToolTip(txtLogDirectory, "Base Logging Directory, Required\r\nClicking will open the Path Editor");
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 22);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 0;
            label1.Text = "Log Directory";
            // 
            // grpOptions
            // 
            grpOptions.Controls.Add(clbOptions);
            grpOptions.Location = new Point(280, 127);
            grpOptions.Name = "grpOptions";
            grpOptions.Size = new Size(179, 194);
            grpOptions.TabIndex = 3;
            grpOptions.TabStop = false;
            grpOptions.Text = "Options";
            // 
            // clbOptions
            // 
            clbOptions.FormattingEnabled = true;
            clbOptions.Location = new Point(7, 19);
            clbOptions.Name = "clbOptions";
            clbOptions.Size = new Size(166, 166);
            clbOptions.TabIndex = 7;
            toolTip1.SetToolTip(clbOptions, "Flags that determines how the logs are managed");
            // 
            // grpRotation
            // 
            grpRotation.Controls.Add(numRetention);
            grpRotation.Controls.Add(numMaxLogSize);
            grpRotation.Controls.Add(cboRetention);
            grpRotation.Controls.Add(cboSize);
            grpRotation.Controls.Add(cboArchiveType);
            grpRotation.Controls.Add(label8);
            grpRotation.Controls.Add(label7);
            grpRotation.Controls.Add(label6);
            grpRotation.Controls.Add(cboRotationType);
            grpRotation.Controls.Add(label5);
            grpRotation.Location = new Point(4, 184);
            grpRotation.Name = "grpRotation";
            grpRotation.Size = new Size(271, 137);
            grpRotation.TabIndex = 2;
            grpRotation.TabStop = false;
            grpRotation.Text = "Rotation Settings";
            // 
            // numRetention
            // 
            numRetention.AllowDecimal = false;
            numRetention.AllowNegative = false;
            numRetention.Borderless = false;
            numRetention.BorderStyle = BorderStyle.FixedSingle;
            numRetention.DarkMode = false;
            numRetention.IntValue = 0;
            numRetention.Location = new Point(106, 74);
            numRetention.Maximum = new decimal(new int[] { 3650, 0, 0, 0 });
            numRetention.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numRetention.Name = "numRetention";
            numRetention.Placeholder = "";
            numRetention.Size = new Size(86, 23);
            numRetention.TabIndex = 11;
            toolTip1.SetToolTip(numRetention, "Number of units the log is to be retained");
            numRetention.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // numMaxLogSize
            // 
            numMaxLogSize.AllowDecimal = false;
            numMaxLogSize.AllowNegative = false;
            numMaxLogSize.Borderless = false;
            numMaxLogSize.BorderStyle = BorderStyle.FixedSingle;
            numMaxLogSize.DarkMode = false;
            numMaxLogSize.IntValue = 0;
            numMaxLogSize.Location = new Point(105, 45);
            numMaxLogSize.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numMaxLogSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMaxLogSize.Name = "numMaxLogSize";
            numMaxLogSize.Placeholder = "";
            numMaxLogSize.Size = new Size(86, 23);
            numMaxLogSize.TabIndex = 10;
            toolTip1.SetToolTip(numMaxLogSize, "Max Log Size, Numeric Only");
            numMaxLogSize.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // cboRetention
            // 
            cboRetention.FormattingEnabled = true;
            cboRetention.Items.AddRange(new object[] { "Days", "Weeks", "Months", "Years" });
            cboRetention.Location = new Point(197, 74);
            cboRetention.Name = "cboRetention";
            cboRetention.Size = new Size(68, 23);
            cboRetention.TabIndex = 9;
            toolTip1.SetToolTip(cboRetention, "That Units of Log Retainment, \r\nDays, Weeks, Months, Years");
            // 
            // cboSize
            // 
            cboSize.FormattingEnabled = true;
            cboSize.Items.AddRange(new object[] { "B", "K", "M", "G", "T" });
            cboSize.Location = new Point(197, 45);
            cboSize.Name = "cboSize";
            cboSize.Size = new Size(68, 23);
            cboSize.TabIndex = 8;
            toolTip1.SetToolTip(cboSize, "Max Log Size Units\r\nB = Bytes\r\nK = KiloBytes\r\nM = MegaBytes\r\nG = GigaBytes\r\nT = TerraBytes");
            // 
            // cboArchiveType
            // 
            cboArchiveType.FormattingEnabled = true;
            cboArchiveType.Location = new Point(104, 103);
            cboArchiveType.Name = "cboArchiveType";
            cboArchiveType.Size = new Size(161, 23);
            cboArchiveType.TabIndex = 7;
            cboArchiveType.Tag = "ArchiveType";
            toolTip1.SetToolTip(cboArchiveType, "Type of Log Archive");
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(8, 107);
            label8.Name = "label8";
            label8.Size = new Size(79, 15);
            label8.TabIndex = 6;
            label8.Text = "Archive Type";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(8, 78);
            label7.Name = "label7";
            label7.Size = new Size(63, 15);
            label7.TabIndex = 4;
            label7.Text = "Retention";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(8, 49);
            label6.Name = "label6";
            label6.Size = new Size(80, 15);
            label6.TabIndex = 2;
            label6.Text = "Max Log Size";
            // 
            // cboRotationType
            // 
            cboRotationType.FormattingEnabled = true;
            cboRotationType.Location = new Point(104, 16);
            cboRotationType.Name = "cboRotationType";
            cboRotationType.Size = new Size(161, 23);
            cboRotationType.TabIndex = 1;
            cboRotationType.Tag = "RotationType";
            toolTip1.SetToolTip(cboRotationType, "Type of Log Rotation");
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(8, 20);
            label5.Name = "label5";
            label5.Size = new Size(84, 15);
            label5.TabIndex = 0;
            label5.Text = "Rotation Type";
            // 
            // grpBehavior
            // 
            grpBehavior.Controls.Add(cboMinimumLevel);
            grpBehavior.Controls.Add(label4);
            grpBehavior.Location = new Point(4, 127);
            grpBehavior.Name = "grpBehavior";
            grpBehavior.Size = new Size(270, 56);
            grpBehavior.TabIndex = 1;
            grpBehavior.TabStop = false;
            grpBehavior.Text = "Logging Behavior";
            // 
            // cboMinimumLevel
            // 
            cboMinimumLevel.FormattingEnabled = true;
            cboMinimumLevel.Location = new Point(113, 19);
            cboMinimumLevel.Name = "cboMinimumLevel";
            cboMinimumLevel.Size = new Size(146, 23);
            cboMinimumLevel.TabIndex = 1;
            cboMinimumLevel.Tag = "LoggingLevel";
            toolTip1.SetToolTip(cboMinimumLevel, "Minimum Logging Level");
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 23);
            label4.Name = "label4";
            label4.Size = new Size(93, 15);
            label4.TabIndex = 0;
            label4.Text = "Minimum Level";
            // 
            // UcLoggerEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlRoot);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Name = "UcLoggerEditor";
            Size = new Size(473, 381);
            pnlRoot.ResumeLayout(false);
            grpLocations.ResumeLayout(false);
            grpLocations.PerformLayout();
            grpOptions.ResumeLayout(false);
            grpRotation.ResumeLayout(false);
            grpRotation.PerformLayout();
            grpBehavior.ResumeLayout(false);
            grpBehavior.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlRoot;
        private ToolTip toolTip1;
        private GroupBox grpLocations;
        private Label label1;
        private TextBox txtArchiveDirectory;
        private Label label3;
        private TextBox txtRotateDirectory;
        private Label label2;
        private TextBox txtLogDirectory;
        private GroupBox grpBehavior;
        private ComboBox cboMinimumLevel;
        private Label label4;
        private GroupBox grpRotation;
        private Label label6;
        private ComboBox cboRotationType;
        private Label label5;
        private ComboBox cboArchiveType;
        private Label label8;
        private Label label7;
        private GroupBox grpOptions;
        private CheckedListBox clbOptions;
        private Button btnUpdate;
        private ComboBox cboSize;
        private ComboBox cboRetention;
        private Controls.NumericTextBox numRetention;
        private Controls.NumericTextBox numMaxLogSize;
    }
}
