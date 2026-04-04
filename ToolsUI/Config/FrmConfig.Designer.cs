/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Config/FrmConfig.Designer.cs
 * File: FrmConfig.Designer.cs
 * Created: None
 * Modified: 2026-04-04
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
            btnBrowseTempPath = new Button();
            btnBrowseDataPath = new Button();
            btnBrowseLogPath = new Button();
            txtTempPath = new TextBox();
            label7 = new Label();
            label5 = new Label();
            txtDataPath = new TextBox();
            txtLogPath = new TextBox();
            label6 = new Label();
            grpSecurity = new GroupBox();
            grpPasswords = new GroupBox();
            btnCfgShow = new Button();
            btnAppShow = new Button();
            txtCfgPwd = new TextBox();
            txtAppPwd = new TextBox();
            label3 = new Label();
            label2 = new Label();
            pnlStyle = new Panel();
            btnEncrypt = new Button();
            cboPwdStyle = new ComboBox();
            label1 = new Label();
            grpDatabase = new GroupBox();
            numDbPort = new ToolsUI.Controls.NumericTextBox();
            btnTest = new Button();
            txtDbUser = new TextBox();
            label8 = new Label();
            txtDbInstance = new TextBox();
            cboEngines = new ComboBox();
            label13 = new Label();
            label9 = new Label();
            txtDbName = new TextBox();
            txtDbHost = new TextBox();
            label12 = new Label();
            label10 = new Label();
            btnDbfShow = new Button();
            label11 = new Label();
            txtDbfPwd = new TextBox();
            label4 = new Label();
            pnlConfigureButtons = new Panel();
            btnRead = new Button();
            btnCancel = new Button();
            btnSave = new Button();
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
            btnAppNew = new Button();
            tabConfig.SuspendLayout();
            pgeConfigure.SuspendLayout();
            pnlConfigure.SuspendLayout();
            pnlConfigureGroups.SuspendLayout();
            pnlCfgLeft.SuspendLayout();
            grpPaths.SuspendLayout();
            grpSecurity.SuspendLayout();
            grpPasswords.SuspendLayout();
            pnlStyle.SuspendLayout();
            grpDatabase.SuspendLayout();
            pnlConfigureButtons.SuspendLayout();
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
            pnlConfigure.Controls.Add(pnlConfigureButtons);
            pnlConfigure.Dock = DockStyle.Top;
            pnlConfigure.Location = new Point(3, 3);
            pnlConfigure.Name = "pnlConfigure";
            pnlConfigure.Size = new Size(736, 317);
            pnlConfigure.TabIndex = 3;
            // 
            // pnlConfigureGroups
            // 
            pnlConfigureGroups.Controls.Add(pnlCfgLeft);
            pnlConfigureGroups.Controls.Add(grpDatabase);
            pnlConfigureGroups.Location = new Point(3, 3);
            pnlConfigureGroups.Name = "pnlConfigureGroups";
            pnlConfigureGroups.Size = new Size(726, 274);
            pnlConfigureGroups.TabIndex = 2;
            // 
            // pnlCfgLeft
            // 
            pnlCfgLeft.Controls.Add(grpPaths);
            pnlCfgLeft.Controls.Add(grpSecurity);
            pnlCfgLeft.Location = new Point(0, 0);
            pnlCfgLeft.Name = "pnlCfgLeft";
            pnlCfgLeft.Size = new Size(395, 274);
            pnlCfgLeft.TabIndex = 4;
            // 
            // grpPaths
            // 
            grpPaths.Controls.Add(btnBrowseTempPath);
            grpPaths.Controls.Add(btnBrowseDataPath);
            grpPaths.Controls.Add(btnBrowseLogPath);
            grpPaths.Controls.Add(txtTempPath);
            grpPaths.Controls.Add(label7);
            grpPaths.Controls.Add(label5);
            grpPaths.Controls.Add(txtDataPath);
            grpPaths.Controls.Add(txtLogPath);
            grpPaths.Controls.Add(label6);
            grpPaths.Dock = DockStyle.Top;
            grpPaths.Location = new Point(0, 144);
            grpPaths.Name = "grpPaths";
            grpPaths.Size = new Size(395, 127);
            grpPaths.TabIndex = 3;
            grpPaths.TabStop = false;
            grpPaths.Text = "Paths";
            // 
            // btnBrowseTempPath
            // 
            btnBrowseTempPath.Location = new Point(262, 73);
            btnBrowseTempPath.Name = "btnBrowseTempPath";
            btnBrowseTempPath.Size = new Size(75, 23);
            btnBrowseTempPath.TabIndex = 8;
            btnBrowseTempPath.Tag = "TempSelector";
            btnBrowseTempPath.Text = "Browse";
            toolTip1.SetToolTip(btnBrowseTempPath, "Browse for the Temporary Folder");
            btnBrowseTempPath.UseVisualStyleBackColor = true;
            // 
            // btnBrowseDataPath
            // 
            btnBrowseDataPath.Location = new Point(262, 44);
            btnBrowseDataPath.Name = "btnBrowseDataPath";
            btnBrowseDataPath.Size = new Size(75, 23);
            btnBrowseDataPath.TabIndex = 7;
            btnBrowseDataPath.Tag = "DataSelector";
            btnBrowseDataPath.Text = "Browse";
            toolTip1.SetToolTip(btnBrowseDataPath, "Browse for the Data Folder");
            btnBrowseDataPath.UseVisualStyleBackColor = true;
            // 
            // btnBrowseLogPath
            // 
            btnBrowseLogPath.Location = new Point(262, 15);
            btnBrowseLogPath.Name = "btnBrowseLogPath";
            btnBrowseLogPath.Size = new Size(75, 23);
            btnBrowseLogPath.TabIndex = 6;
            btnBrowseLogPath.Tag = "LogSelector";
            btnBrowseLogPath.Text = "Browse";
            toolTip1.SetToolTip(btnBrowseLogPath, "Browse for the Logging Folder");
            btnBrowseLogPath.UseVisualStyleBackColor = true;
            // 
            // txtTempPath
            // 
            txtTempPath.Location = new Point(85, 73);
            txtTempPath.Name = "txtTempPath";
            txtTempPath.Size = new Size(171, 23);
            txtTempPath.TabIndex = 5;
            toolTip1.SetToolTip(txtTempPath, "Directory which contains the Temporary Files");
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(13, 77);
            label7.Name = "label7";
            label7.Size = new Size(66, 15);
            label7.TabIndex = 4;
            label7.Text = "Temp Path";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(24, 19);
            label5.Name = "label5";
            label5.Size = new Size(55, 15);
            label5.TabIndex = 0;
            label5.Text = "Log Path";
            // 
            // txtDataPath
            // 
            txtDataPath.Location = new Point(85, 44);
            txtDataPath.Name = "txtDataPath";
            txtDataPath.Size = new Size(171, 23);
            txtDataPath.TabIndex = 3;
            toolTip1.SetToolTip(txtDataPath, "Directory which Contains the Data Files");
            // 
            // txtLogPath
            // 
            txtLogPath.Location = new Point(85, 15);
            txtLogPath.Name = "txtLogPath";
            txtLogPath.Size = new Size(171, 23);
            txtLogPath.TabIndex = 1;
            toolTip1.SetToolTip(txtLogPath, "Directory which contains the logs");
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(18, 48);
            label6.Name = "label6";
            label6.Size = new Size(61, 15);
            label6.TabIndex = 2;
            label6.Text = "Data Path";
            // 
            // grpSecurity
            // 
            grpSecurity.Controls.Add(grpPasswords);
            grpSecurity.Controls.Add(pnlStyle);
            grpSecurity.Dock = DockStyle.Top;
            grpSecurity.Location = new Point(0, 0);
            grpSecurity.Name = "grpSecurity";
            grpSecurity.Size = new Size(395, 144);
            grpSecurity.TabIndex = 2;
            grpSecurity.TabStop = false;
            grpSecurity.Text = "Security";
            // 
            // grpPasswords
            // 
            grpPasswords.Controls.Add(btnAppNew);
            grpPasswords.Controls.Add(btnCfgShow);
            grpPasswords.Controls.Add(btnAppShow);
            grpPasswords.Controls.Add(txtCfgPwd);
            grpPasswords.Controls.Add(txtAppPwd);
            grpPasswords.Controls.Add(label3);
            grpPasswords.Controls.Add(label2);
            grpPasswords.Dock = DockStyle.Top;
            grpPasswords.Location = new Point(3, 51);
            grpPasswords.Name = "grpPasswords";
            grpPasswords.Size = new Size(389, 85);
            grpPasswords.TabIndex = 2;
            grpPasswords.TabStop = false;
            grpPasswords.Text = "Passwords";
            // 
            // btnCfgShow
            // 
            btnCfgShow.Location = new Point(292, 48);
            btnCfgShow.Name = "btnCfgShow";
            btnCfgShow.Size = new Size(46, 23);
            btnCfgShow.TabIndex = 7;
            btnCfgShow.Tag = "CfgShow";
            btnCfgShow.Text = "Show";
            btnCfgShow.UseVisualStyleBackColor = true;
            // 
            // btnAppShow
            // 
            btnAppShow.Location = new Point(292, 19);
            btnAppShow.Name = "btnAppShow";
            btnAppShow.Size = new Size(46, 23);
            btnAppShow.TabIndex = 6;
            btnAppShow.Tag = "AppShow";
            btnAppShow.Text = "Show";
            btnAppShow.UseVisualStyleBackColor = true;
            // 
            // txtCfgPwd
            // 
            txtCfgPwd.Location = new Point(104, 48);
            txtCfgPwd.Name = "txtCfgPwd";
            txtCfgPwd.PasswordChar = '*';
            txtCfgPwd.Size = new Size(180, 23);
            txtCfgPwd.TabIndex = 4;
            toolTip1.SetToolTip(txtCfgPwd, "Password to Secure the Configuration");
            // 
            // txtAppPwd
            // 
            txtAppPwd.Location = new Point(106, 19);
            txtAppPwd.Name = "txtAppPwd";
            txtAppPwd.PasswordChar = '*';
            txtAppPwd.Size = new Size(180, 23);
            txtAppPwd.TabIndex = 3;
            toolTip1.SetToolTip(txtAppPwd, "Password To Secure the Application");
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 52);
            label3.Name = "label3";
            label3.Size = new Size(83, 15);
            label3.TabIndex = 1;
            label3.Text = "Configuration";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 23);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 0;
            label2.Text = "Application";
            // 
            // pnlStyle
            // 
            pnlStyle.Controls.Add(btnEncrypt);
            pnlStyle.Controls.Add(cboPwdStyle);
            pnlStyle.Controls.Add(label1);
            pnlStyle.Dock = DockStyle.Top;
            pnlStyle.Location = new Point(3, 19);
            pnlStyle.Name = "pnlStyle";
            pnlStyle.Size = new Size(389, 32);
            pnlStyle.TabIndex = 4;
            // 
            // btnEncrypt
            // 
            btnEncrypt.Location = new Point(225, 3);
            btnEncrypt.Name = "btnEncrypt";
            btnEncrypt.Size = new Size(75, 23);
            btnEncrypt.TabIndex = 3;
            btnEncrypt.Tag = "Encrypt";
            btnEncrypt.Text = "&Encrypt";
            btnEncrypt.UseVisualStyleBackColor = true;
            btnEncrypt.Visible = false;
            // 
            // cboPwdStyle
            // 
            cboPwdStyle.FormattingEnabled = true;
            cboPwdStyle.Location = new Point(97, 3);
            cboPwdStyle.Name = "cboPwdStyle";
            cboPwdStyle.Size = new Size(118, 23);
            cboPwdStyle.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2, 7);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 0;
            label1.Text = "Password Style";
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
            grpDatabase.Controls.Add(txtDbName);
            grpDatabase.Controls.Add(txtDbHost);
            grpDatabase.Controls.Add(label12);
            grpDatabase.Controls.Add(label10);
            grpDatabase.Controls.Add(btnDbfShow);
            grpDatabase.Controls.Add(label11);
            grpDatabase.Controls.Add(txtDbfPwd);
            grpDatabase.Controls.Add(label4);
            grpDatabase.Dock = DockStyle.Right;
            grpDatabase.Location = new Point(397, 0);
            grpDatabase.Name = "grpDatabase";
            grpDatabase.Size = new Size(329, 274);
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
            numDbPort.Text = "0";
            numDbPort.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // btnTest
            // 
            btnTest.Location = new Point(154, 241);
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
            txtDbInstance.Location = new Point(120, 205);
            txtDbInstance.Name = "txtDbInstance";
            txtDbInstance.Size = new Size(142, 23);
            txtDbInstance.TabIndex = 15;
            // 
            // cboEngines
            // 
            cboEngines.FormattingEnabled = true;
            cboEngines.Location = new Point(120, 19);
            cboEngines.Name = "cboEngines";
            cboEngines.Size = new Size(142, 23);
            cboEngines.TabIndex = 1;
            toolTip1.SetToolTip(cboEngines, "Selects the Active Database Engine");
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(46, 209);
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
            // txtDbName
            // 
            txtDbName.Location = new Point(120, 174);
            txtDbName.Name = "txtDbName";
            txtDbName.Size = new Size(142, 23);
            txtDbName.TabIndex = 13;
            // 
            // txtDbHost
            // 
            txtDbHost.Location = new Point(120, 50);
            txtDbHost.Name = "txtDbHost";
            txtDbHost.Size = new Size(142, 23);
            txtDbHost.TabIndex = 3;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(49, 178);
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
            // btnDbfShow
            // 
            btnDbfShow.Location = new Point(265, 143);
            btnDbfShow.Name = "btnDbfShow";
            btnDbfShow.Size = new Size(46, 23);
            btnDbfShow.TabIndex = 11;
            btnDbfShow.Tag = "DbfShow";
            btnDbfShow.Text = "Show";
            btnDbfShow.UseVisualStyleBackColor = true;
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
            // txtDbfPwd
            // 
            txtDbfPwd.Location = new Point(120, 143);
            txtDbfPwd.Name = "txtDbfPwd";
            txtDbfPwd.PasswordChar = '*';
            txtDbfPwd.Size = new Size(142, 23);
            txtDbfPwd.TabIndex = 10;
            toolTip1.SetToolTip(txtDbfPwd, "Password to Connect to the Database");
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(48, 147);
            label4.Name = "label4";
            label4.Size = new Size(59, 15);
            label4.TabIndex = 9;
            label4.Text = "Password";
            // 
            // pnlConfigureButtons
            // 
            pnlConfigureButtons.Controls.Add(btnRead);
            pnlConfigureButtons.Controls.Add(btnCancel);
            pnlConfigureButtons.Controls.Add(btnSave);
            pnlConfigureButtons.Location = new Point(241, 279);
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
            // btnAppNew
            // 
            btnAppNew.Location = new Point(342, 22);
            btnAppNew.Name = "btnAppNew";
            btnAppNew.Size = new Size(46, 23);
            btnAppNew.TabIndex = 8;
            btnAppNew.Tag = "AppNew";
            btnAppNew.Text = "New";
            btnAppNew.UseVisualStyleBackColor = true;
            // 
            // FrmConfig
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(749, 521);
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
            grpPasswords.ResumeLayout(false);
            grpPasswords.PerformLayout();
            pnlStyle.ResumeLayout(false);
            pnlStyle.PerformLayout();
            grpDatabase.ResumeLayout(false);
            grpDatabase.PerformLayout();
            pnlConfigureButtons.ResumeLayout(false);
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
        private ComboBox cboPwdStyle;
        private Label label1;
        private Button btnEncrypt;
        private GroupBox grpPasswords;
        private TextBox txtCfgPwd;
        private TextBox txtAppPwd;
        private Label label3;
        private Label label2;
        private Button btnCfgShow;
        private Button btnAppShow;
        private Panel pnlConfigureButtons;
        private Button btnCancel;
        private Button btnSave;
        private Button btnRead;
        private Label label5;
        private TextBox txtLogPath;
        private TextBox txtTempPath;
        private Label label7;
        private TextBox txtDataPath;
        private Label label6;
        private Panel pnlStyle;
        private ComboBox cboEngines;
        private Label label8;
        private TextBox txtDbHost;
        private Label label9;
        private TextBox txtDbName;
        private Label label12;
        private TextBox txtDbfPwd;
        private Label label4;
        private TextBox txtDbUser;
        private Label label11;
        private Label label10;
        private TextBox txtDbInstance;
        private Label label13;
        private Button btnDbfShow;
        private Controls.NumericTextBox numDbPort;
        private Button btnTest;
        private TabPage pgeDiagnostics;
        private Panel pnlConfigureGroups;
        private GroupBox grpSecurity;
        private GroupBox grpDatabase;
        private GroupBox grpPaths;
        private Panel pnlConfigure;
        private Button btnBrowseTempPath;
        private Button btnBrowseDataPath;
        private Button btnBrowseLogPath;
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
        private Button btnAppNew;
    }
}