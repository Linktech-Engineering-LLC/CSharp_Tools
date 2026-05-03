/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Config/FrmConfig.Designer.cs
 * File: FrmConfig.Designer.cs
 * Version: 1.0.0
 * Created: None
 * Modified: 2026-05-03
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
namespace ToolsUI.Config
{
    partial class FrmConfig
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tabConfig = new TabControl();
            pgeConfigure = new TabPage();
            pnlConfigure = new Panel();
            pnlConfigureGroups = new Panel();
            pnlCfgLeft = new Panel();
            grpPaths = new GroupBox();
            cboPathType = new ComboBox();
            label2 = new Label();
            cboPathName = new ComboBox();
            label1 = new Label();
            btnBrowsePath = new Button();
            label5 = new Label();
            txtPath = new TextBox();
            grpSecurity = new GroupBox();
            grpPassword = new GroupBox();
            panel1 = new Panel();
            btnPasswordEditor = new Button();
            cboPassword = new ComboBox();
            pnlConfigureButtons = new Panel();
            btnRead = new Button();
            btnCancel = new Button();
            btnSave = new Button();
            grpDatabase = new GroupBox();
            numDbPort = new ToolsUI.Controls.NumericTextBox();
            btnTest = new Button();
            txtDbUser = new TextBox();
            label8 = new Label();
            txtDbInstance = new TextBox();
            cboEngines = new ComboBox();
            label13 = new Label();
            label9 = new Label();
            txtDbSchema = new TextBox();
            txtDbHost = new TextBox();
            label12 = new Label();
            label10 = new Label();
            label11 = new Label();
            pgeDiagnostics = new TabPage();
            pnlDiagnostics = new Panel();
            pnlDiagnosticsRight = new Panel();
            pnlDetails = new Panel();
            txtRepair = new TextBox();
            txtActual = new TextBox();
            txtExpected = new TextBox();
            txtDescription = new TextBox();
            lblTestName = new Label();
            pnlDiagnosticButtons = new Panel();
            btnRunGroup = new Button();
            btnRunTest = new Button();
            lstDiagnosticsResults = new ListBox();
            tvDiagnostics = new TreeView();
            toolTip1 = new ToolTip(components);
            tabConfig.SuspendLayout();
            pgeConfigure.SuspendLayout();
            pnlConfigure.SuspendLayout();
            pnlConfigureGroups.SuspendLayout();
            pnlCfgLeft.SuspendLayout();
            grpPaths.SuspendLayout();
            grpSecurity.SuspendLayout();
            grpPassword.SuspendLayout();
            panel1.SuspendLayout();
            pnlConfigureButtons.SuspendLayout();
            grpDatabase.SuspendLayout();
            pgeDiagnostics.SuspendLayout();
            pnlDiagnostics.SuspendLayout();
            pnlDiagnosticsRight.SuspendLayout();
            pnlDetails.SuspendLayout();
            pnlDiagnosticButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tabConfig
            // 
            tabConfig.Controls.Add(pgeConfigure);
            tabConfig.Controls.Add(pgeDiagnostics);
            tabConfig.Location = new Point(2, 7);
            tabConfig.Name = "tabConfig";
            tabConfig.SelectedIndex = 0;
            tabConfig.Size = new Size(750, 530);
            tabConfig.TabIndex = 0;
            // 
            // pgeConfigure
            // 
            pgeConfigure.Controls.Add(pnlConfigure);
            pgeConfigure.Location = new Point(4, 24);
            pgeConfigure.Name = "pgeConfigure";
            pgeConfigure.Padding = new Padding(3);
            pgeConfigure.Size = new Size(742, 502);
            pgeConfigure.TabIndex = 0;
            pgeConfigure.Text = "Configure";
            pgeConfigure.UseVisualStyleBackColor = true;
            // 
            // pnlConfigure
            // 
            pnlConfigure.Controls.Add(pnlConfigureGroups);
            pnlConfigure.Dock = DockStyle.Top;
            pnlConfigure.Location = new Point(3, 3);
            pnlConfigure.Name = "pnlConfigure";
            pnlConfigure.Size = new Size(736, 350);
            pnlConfigure.TabIndex = 3;
            // 
            // pnlConfigureGroups
            // 
            pnlConfigureGroups.Controls.Add(pnlCfgLeft);
            pnlConfigureGroups.Controls.Add(pnlConfigureButtons);
            pnlConfigureGroups.Controls.Add(grpDatabase);
            pnlConfigureGroups.Location = new Point(3, 3);
            pnlConfigureGroups.Name = "pnlConfigureGroups";
            pnlConfigureGroups.Size = new Size(726, 247);
            pnlConfigureGroups.TabIndex = 2;
            // 
            // pnlCfgLeft
            // 
            pnlCfgLeft.Controls.Add(grpPaths);
            pnlCfgLeft.Controls.Add(grpSecurity);
            pnlCfgLeft.Location = new Point(0, 0);
            pnlCfgLeft.Name = "pnlCfgLeft";
            pnlCfgLeft.Size = new Size(395, 167);
            pnlCfgLeft.TabIndex = 4;
            // 
            // grpPaths
            // 
            grpPaths.Controls.Add(cboPathType);
            grpPaths.Controls.Add(label2);
            grpPaths.Controls.Add(cboPathName);
            grpPaths.Controls.Add(label1);
            grpPaths.Controls.Add(btnBrowsePath);
            grpPaths.Controls.Add(label5);
            grpPaths.Controls.Add(txtPath);
            grpPaths.Dock = DockStyle.Top;
            grpPaths.Location = new Point(0, 73);
            grpPaths.Name = "grpPaths";
            grpPaths.Size = new Size(395, 94);
            grpPaths.TabIndex = 3;
            grpPaths.TabStop = false;
            grpPaths.Text = "Paths";
            // 
            // cboPathType
            // 
            cboPathType.FormattingEnabled = true;
            cboPathType.Location = new Point(234, 15);
            cboPathType.Name = "cboPathType";
            cboPathType.Size = new Size(139, 23);
            cboPathType.TabIndex = 12;
            cboPathType.Tag = "PathType";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(195, 19);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 11;
            label2.Text = "Type";
            // 
            // cboPathName
            // 
            cboPathName.FormattingEnabled = true;
            cboPathName.Location = new Point(50, 15);
            cboPathName.Name = "cboPathName";
            cboPathName.Size = new Size(139, 23);
            cboPathName.TabIndex = 10;
            cboPathName.Tag = "PathName";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 9;
            label1.Text = "Name";
            // 
            // btnBrowsePath
            // 
            btnBrowsePath.Location = new Point(262, 44);
            btnBrowsePath.Name = "btnBrowsePath";
            btnBrowsePath.Size = new Size(75, 23);
            btnBrowsePath.TabIndex = 6;
            btnBrowsePath.Tag = "LogSelector";
            btnBrowsePath.Text = "Browse";
            toolTip1.SetToolTip(btnBrowsePath, "Browse for the Logging Folder");
            btnBrowsePath.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(41, 48);
            label5.Name = "label5";
            label5.Size = new Size(32, 15);
            label5.TabIndex = 0;
            label5.Text = "Path";
            // 
            // txtPath
            // 
            txtPath.Location = new Point(85, 44);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(171, 23);
            txtPath.TabIndex = 1;
            txtPath.Tag = "Path";
            toolTip1.SetToolTip(txtPath, "Directory which contains the logs");
            // 
            // grpSecurity
            // 
            grpSecurity.Controls.Add(grpPassword);
            grpSecurity.Dock = DockStyle.Top;
            grpSecurity.Location = new Point(0, 0);
            grpSecurity.Name = "grpSecurity";
            grpSecurity.Size = new Size(395, 73);
            grpSecurity.TabIndex = 2;
            grpSecurity.TabStop = false;
            grpSecurity.Text = "Security";
            // 
            // grpPassword
            // 
            grpPassword.Controls.Add(panel1);
            grpPassword.Dock = DockStyle.Top;
            grpPassword.Location = new Point(3, 19);
            grpPassword.Name = "grpPassword";
            grpPassword.Size = new Size(389, 54);
            grpPassword.TabIndex = 3;
            grpPassword.TabStop = false;
            grpPassword.Text = "Passwords";
            // 
            // panel1
            // 
            panel1.Controls.Add(btnPasswordEditor);
            panel1.Controls.Add(cboPassword);
            panel1.Location = new Point(76, 18);
            panel1.Name = "panel1";
            panel1.Size = new Size(242, 32);
            panel1.TabIndex = 0;
            // 
            // btnPasswordEditor
            // 
            btnPasswordEditor.Location = new Point(160, 3);
            btnPasswordEditor.Name = "btnPasswordEditor";
            btnPasswordEditor.Size = new Size(75, 23);
            btnPasswordEditor.TabIndex = 1;
            btnPasswordEditor.Tag = "PasswordEditor";
            btnPasswordEditor.Text = "&Edit";
            btnPasswordEditor.UseVisualStyleBackColor = true;
            // 
            // cboPassword
            // 
            cboPassword.FormattingEnabled = true;
            cboPassword.Location = new Point(3, 3);
            cboPassword.Name = "cboPassword";
            cboPassword.Size = new Size(139, 23);
            cboPassword.TabIndex = 0;
            cboPassword.Tag = "Password";
            // 
            // pnlConfigureButtons
            // 
            pnlConfigureButtons.Controls.Add(btnRead);
            pnlConfigureButtons.Controls.Add(btnCancel);
            pnlConfigureButtons.Controls.Add(btnSave);
            pnlConfigureButtons.Location = new Point(67, 179);
            pnlConfigureButtons.Name = "pnlConfigureButtons";
            pnlConfigureButtons.Size = new Size(260, 30);
            pnlConfigureButtons.TabIndex = 1;
            // 
            // btnRead
            // 
            btnRead.Location = new Point(6, 4);
            btnRead.Name = "btnRead";
            btnRead.Size = new Size(75, 23);
            btnRead.TabIndex = 2;
            btnRead.Tag = "Read";
            btnRead.Text = "&Read";
            toolTip1.SetToolTip(btnRead, "Reads the Configuation");
            btnRead.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(174, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Tag = "Cancel";
            btnCancel.Text = "&Cancel";
            toolTip1.SetToolTip(btnCancel, "Cancels Editing the Configuration, and closes the form");
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(90, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 0;
            btnSave.Tag = "Save";
            btnSave.Text = "&Save";
            toolTip1.SetToolTip(btnSave, "Saves the configuration and closes the form");
            btnSave.UseVisualStyleBackColor = true;
            // 
            // grpDatabase
            // 
            grpDatabase.Controls.Add(numDbPort);
            grpDatabase.Controls.Add(btnTest);
            grpDatabase.Controls.Add(txtDbUser);
            grpDatabase.Controls.Add(label8);
            grpDatabase.Controls.Add(txtDbInstance);
            grpDatabase.Controls.Add(cboEngines);
            grpDatabase.Controls.Add(label13);
            grpDatabase.Controls.Add(label9);
            grpDatabase.Controls.Add(txtDbSchema);
            grpDatabase.Controls.Add(txtDbHost);
            grpDatabase.Controls.Add(label12);
            grpDatabase.Controls.Add(label10);
            grpDatabase.Controls.Add(label11);
            grpDatabase.Dock = DockStyle.Right;
            grpDatabase.Location = new Point(397, 0);
            grpDatabase.Name = "grpDatabase";
            grpDatabase.Size = new Size(329, 247);
            grpDatabase.TabIndex = 4;
            grpDatabase.TabStop = false;
            grpDatabase.Text = "Database";
            // 
            // numDbPort
            // 
            numDbPort.AllowDecimal = false;
            numDbPort.AllowNegative = false;
            numDbPort.Borderless = false;
            numDbPort.BorderStyle = BorderStyle.FixedSingle;
            numDbPort.DarkMode = false;
            numDbPort.IntValue = 0;
            numDbPort.Location = new Point(120, 81);
            numDbPort.Maximum = new decimal(new int[] { 65553, 0, 0, 0 });
            numDbPort.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            numDbPort.Name = "numDbPort";
            numDbPort.Placeholder = "";
            numDbPort.Size = new Size(53, 23);
            numDbPort.TabIndex = 20;
            numDbPort.Tag = "Port";
            numDbPort.Text = "0";
            numDbPort.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // btnTest
            // 
            btnTest.Location = new Point(154, 211);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(75, 23);
            btnTest.TabIndex = 19;
            btnTest.Tag = "Test";
            btnTest.Text = "&Test";
            btnTest.UseVisualStyleBackColor = true;
            // 
            // txtDbUser
            // 
            txtDbUser.Location = new Point(120, 112);
            txtDbUser.Name = "txtDbUser";
            txtDbUser.Size = new Size(142, 23);
            txtDbUser.TabIndex = 7;
            txtDbUser.Tag = "User";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(9, 25);
            label8.Name = "label8";
            label8.Size = new Size(98, 15);
            label8.TabIndex = 0;
            label8.Text = "Database Engine";
            // 
            // txtDbInstance
            // 
            txtDbInstance.Location = new Point(120, 175);
            txtDbInstance.Name = "txtDbInstance";
            txtDbInstance.Size = new Size(142, 23);
            txtDbInstance.TabIndex = 15;
            txtDbInstance.Tag = "Instance";
            // 
            // cboEngines
            // 
            cboEngines.FormattingEnabled = true;
            cboEngines.Location = new Point(120, 19);
            cboEngines.Name = "cboEngines";
            cboEngines.Size = new Size(142, 23);
            cboEngines.TabIndex = 1;
            cboEngines.Tag = "Engines";
            toolTip1.SetToolTip(cboEngines, "Selects the Active Database Engine");
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(46, 179);
            label13.Name = "label13";
            label13.Size = new Size(54, 15);
            label13.TabIndex = 14;
            label13.Text = "Instance";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(74, 54);
            label9.Name = "label9";
            label9.Size = new Size(33, 15);
            label9.TabIndex = 2;
            label9.Text = "Host";
            // 
            // txtDbSchema
            // 
            txtDbSchema.Location = new Point(120, 144);
            txtDbSchema.Name = "txtDbSchema";
            txtDbSchema.Size = new Size(142, 23);
            txtDbSchema.TabIndex = 13;
            txtDbSchema.Tag = "Schema";
            // 
            // txtDbHost
            // 
            txtDbHost.Location = new Point(120, 50);
            txtDbHost.Name = "txtDbHost";
            txtDbHost.Size = new Size(142, 23);
            txtDbHost.TabIndex = 3;
            txtDbHost.Tag = "Host";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(49, 148);
            label12.Name = "label12";
            label12.Size = new Size(51, 15);
            label12.TabIndex = 12;
            label12.Text = "Schema";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(76, 85);
            label10.Name = "label10";
            label10.Size = new Size(31, 15);
            label10.TabIndex = 4;
            label10.Text = "Port";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(74, 116);
            label11.Name = "label11";
            label11.Size = new Size(33, 15);
            label11.TabIndex = 6;
            label11.Text = "User";
            // 
            // pgeDiagnostics
            // 
            pgeDiagnostics.Controls.Add(pnlDiagnostics);
            pgeDiagnostics.Location = new Point(4, 24);
            pgeDiagnostics.Name = "pgeDiagnostics";
            pgeDiagnostics.Size = new Size(742, 502);
            pgeDiagnostics.TabIndex = 3;
            pgeDiagnostics.Text = "Diagnostics";
            pgeDiagnostics.UseVisualStyleBackColor = true;
            // 
            // pnlDiagnostics
            // 
            pnlDiagnostics.AutoScroll = true;
            pnlDiagnostics.Controls.Add(pnlDiagnosticsRight);
            pnlDiagnostics.Controls.Add(tvDiagnostics);
            pnlDiagnostics.Dock = DockStyle.Top;
            pnlDiagnostics.Location = new Point(0, 0);
            pnlDiagnostics.Name = "pnlDiagnostics";
            pnlDiagnostics.Size = new Size(742, 499);
            pnlDiagnostics.TabIndex = 0;
            // 
            // pnlDiagnosticsRight
            // 
            pnlDiagnosticsRight.Controls.Add(pnlDetails);
            pnlDiagnosticsRight.Controls.Add(pnlDiagnosticButtons);
            pnlDiagnosticsRight.Controls.Add(lstDiagnosticsResults);
            pnlDiagnosticsRight.Dock = DockStyle.Fill;
            pnlDiagnosticsRight.Location = new Point(187, 0);
            pnlDiagnosticsRight.Name = "pnlDiagnosticsRight";
            pnlDiagnosticsRight.Size = new Size(555, 499);
            pnlDiagnosticsRight.TabIndex = 6;
            // 
            // pnlDetails
            // 
            pnlDetails.Controls.Add(txtRepair);
            pnlDetails.Controls.Add(txtActual);
            pnlDetails.Controls.Add(txtExpected);
            pnlDetails.Controls.Add(txtDescription);
            pnlDetails.Controls.Add(lblTestName);
            pnlDetails.Dock = DockStyle.Fill;
            pnlDetails.Location = new Point(0, 240);
            pnlDetails.Name = "pnlDetails";
            pnlDetails.Size = new Size(555, 259);
            pnlDetails.TabIndex = 2;
            // 
            // txtRepair
            // 
            txtRepair.Dock = DockStyle.Top;
            txtRepair.Location = new Point(0, 195);
            txtRepair.Multiline = true;
            txtRepair.Name = "txtRepair";
            txtRepair.Size = new Size(555, 60);
            txtRepair.TabIndex = 4;
            // 
            // txtActual
            // 
            txtActual.Dock = DockStyle.Top;
            txtActual.Location = new Point(0, 135);
            txtActual.Multiline = true;
            txtActual.Name = "txtActual";
            txtActual.Size = new Size(555, 60);
            txtActual.TabIndex = 3;
            // 
            // txtExpected
            // 
            txtExpected.Dock = DockStyle.Top;
            txtExpected.Location = new Point(0, 75);
            txtExpected.Multiline = true;
            txtExpected.Name = "txtExpected";
            txtExpected.Size = new Size(555, 60);
            txtExpected.TabIndex = 2;
            // 
            // txtDescription
            // 
            txtDescription.Dock = DockStyle.Top;
            txtDescription.Location = new Point(0, 15);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(555, 60);
            txtDescription.TabIndex = 1;
            // 
            // lblTestName
            // 
            lblTestName.AutoSize = true;
            lblTestName.Dock = DockStyle.Top;
            lblTestName.Location = new Point(0, 0);
            lblTestName.Name = "lblTestName";
            lblTestName.Size = new Size(66, 15);
            lblTestName.TabIndex = 0;
            lblTestName.Text = "Test Name";
            // 
            // pnlDiagnosticButtons
            // 
            pnlDiagnosticButtons.Controls.Add(btnRunGroup);
            pnlDiagnosticButtons.Controls.Add(btnRunTest);
            pnlDiagnosticButtons.Dock = DockStyle.Top;
            pnlDiagnosticButtons.Location = new Point(0, 200);
            pnlDiagnosticButtons.Name = "pnlDiagnosticButtons";
            pnlDiagnosticButtons.Size = new Size(555, 40);
            pnlDiagnosticButtons.TabIndex = 1;
            // 
            // btnRunGroup
            // 
            btnRunGroup.Location = new Point(127, 5);
            btnRunGroup.Name = "btnRunGroup";
            btnRunGroup.Size = new Size(75, 23);
            btnRunGroup.TabIndex = 1;
            btnRunGroup.Tag = "RunGroup";
            btnRunGroup.Text = "Run Group";
            btnRunGroup.UseVisualStyleBackColor = true;
            // 
            // btnRunTest
            // 
            btnRunTest.Location = new Point(28, 5);
            btnRunTest.Name = "btnRunTest";
            btnRunTest.Size = new Size(75, 23);
            btnRunTest.TabIndex = 0;
            btnRunTest.Tag = "RunTest";
            btnRunTest.Text = "Run Test";
            btnRunTest.UseVisualStyleBackColor = true;
            // 
            // lstDiagnosticsResults
            // 
            lstDiagnosticsResults.Dock = DockStyle.Top;
            lstDiagnosticsResults.DrawMode = DrawMode.OwnerDrawFixed;
            lstDiagnosticsResults.FormattingEnabled = true;
            lstDiagnosticsResults.IntegralHeight = false;
            lstDiagnosticsResults.Location = new Point(0, 0);
            lstDiagnosticsResults.Name = "lstDiagnosticsResults";
            lstDiagnosticsResults.Size = new Size(555, 200);
            lstDiagnosticsResults.TabIndex = 0;
            // 
            // tvDiagnostics
            // 
            tvDiagnostics.Dock = DockStyle.Left;
            tvDiagnostics.Location = new Point(0, 0);
            tvDiagnostics.Name = "tvDiagnostics";
            tvDiagnostics.Size = new Size(187, 499);
            tvDiagnostics.TabIndex = 5;
            // 
            // FrmConfig
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(749, 298);
            Controls.Add(tabConfig);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "FrmConfig";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmConfig";
            tabConfig.ResumeLayout(false);
            pgeConfigure.ResumeLayout(false);
            pnlConfigure.ResumeLayout(false);
            pnlConfigureGroups.ResumeLayout(false);
            pnlCfgLeft.ResumeLayout(false);
            grpPaths.ResumeLayout(false);
            grpPaths.PerformLayout();
            grpSecurity.ResumeLayout(false);
            grpPassword.ResumeLayout(false);
            panel1.ResumeLayout(false);
            pnlConfigureButtons.ResumeLayout(false);
            grpDatabase.ResumeLayout(false);
            grpDatabase.PerformLayout();
            pgeDiagnostics.ResumeLayout(false);
            pnlDiagnostics.ResumeLayout(false);
            pnlDiagnosticsRight.ResumeLayout(false);
            pnlDetails.ResumeLayout(false);
            pnlDetails.PerformLayout();
            pnlDiagnosticButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabConfig;
        private TabPage pgeConfigure;
        private ToolTip toolTip1;
        private Panel pnlConfigureButtons;
        private Button btnCancel;
        private Button btnSave;
        private Button btnRead;
        private Label label5;
        private TextBox txtPath;
        private ComboBox cboEngines;
        private Label label8;
        private TextBox txtDbHost;
        private Label label9;
        private TextBox txtDbSchema;
        private Label label12;
        private TextBox txtDbUser;
        private Label label11;
        private Label label10;
        private TextBox txtDbInstance;
        private Label label13;
        private Controls.NumericTextBox numDbPort;
        private Button btnTest;
        private TabPage pgeDiagnostics;
        private Panel pnlConfigureGroups;
        private GroupBox grpSecurity;
        private GroupBox grpDatabase;
        private GroupBox grpPaths;
        private Panel pnlConfigure;
        private Button btnBrowsePath;
        private Panel pnlDiagnostics;
        private Panel pnlCfgLeft;
        private TreeView tvDiagnostics;
        private Panel pnlDiagnosticsRight;
        private ListBox lstDiagnosticsResults;
        private Panel pnlDiagnosticButtons;
        private Button btnRunTest;
        private Button btnRunGroup;
        private Panel pnlDetails;
        private TextBox txtRepair;
        private TextBox txtActual;
        private TextBox txtExpected;
        private TextBox txtDescription;
        private Label lblTestName;
        private GroupBox grpPassword;
        private Panel panel1;
        private Button btnPasswordEditor;
        private ComboBox cboPassword;
        private ComboBox cboPathName;
        private Label label1;
        private ComboBox cboPathType;
        private Label label2;
    }
}