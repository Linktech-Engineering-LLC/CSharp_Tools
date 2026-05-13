/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/UserControls/UcDatabaseEditor.Designer.cs
 * File: UcDatabaseEditor.Designer.cs
 * Version: 1.0.2
 * Created: 2026-05-12
 * Modified: 2026-05-13
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
// """

namespace ToolsUI.UserControls
{
    partial class UcDatabaseEditor
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
            panel1 = new Panel();
            txtID = new TextBox();
            label1 = new Label();
            lblTitle = new Label();
            toolTip1 = new ToolTip(components);
            cboEngine = new ComboBox();
            txtHost = new TextBox();
            numPort = new ToolsUI.Controls.NumericTextBox();
            txtSchema = new TextBox();
            txtInstance = new TextBox();
            btnRemove = new Button();
            btnCancel = new Button();
            btnUpdate = new Button();
            btnTest = new Button();
            btnShow = new Button();
            panel2 = new Panel();
            label8 = new Label();
            panel3 = new Panel();
            label2 = new Label();
            panel4 = new Panel();
            label10 = new Label();
            panel5 = new Panel();
            label3 = new Label();
            panel6 = new Panel();
            label4 = new Label();
            pnlButtons = new Panel();
            panel7 = new Panel();
            txtUser = new TextBox();
            label11 = new Label();
            panel8 = new Panel();
            txtPassword = new TextBox();
            label5 = new Label();
            pnlRoot = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            pnlButtons.SuspendLayout();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            pnlRoot.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(txtID);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(52, 24);
            panel1.Name = "panel1";
            panel1.Size = new Size(234, 35);
            panel1.TabIndex = 0;
            // 
            // txtID
            // 
            txtID.Location = new Point(97, 6);
            txtID.Name = "txtID";
            txtID.Size = new Size(128, 23);
            txtID.TabIndex = 1;
            txtID.Tag = "ConnID";
            toolTip1.SetToolTip(txtID, "Connection ID, must be unique");
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 10);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 0;
            label1.Text = "Connection ID";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(122, 7);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(94, 15);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Database Editor";
            // 
            // cboEngine
            // 
            cboEngine.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEngine.FormattingEnabled = true;
            cboEngine.Location = new Point(108, 7);
            cboEngine.Name = "cboEngine";
            cboEngine.Size = new Size(142, 23);
            cboEngine.TabIndex = 3;
            cboEngine.Tag = "Engines";
            toolTip1.SetToolTip(cboEngine, "Selects the Active Database Engine");
            // 
            // txtHost
            // 
            txtHost.Location = new Point(46, 6);
            txtHost.Name = "txtHost";
            txtHost.Size = new Size(128, 23);
            txtHost.TabIndex = 1;
            txtHost.Tag = "Host";
            toolTip1.SetToolTip(txtHost, "Name of the server hosting the database");
            // 
            // numPort
            // 
            numPort.AllowDecimal = false;
            numPort.AllowNegative = false;
            numPort.Borderless = false;
            numPort.BorderStyle = BorderStyle.FixedSingle;
            numPort.DarkMode = false;
            numPort.IntValue = 0;
            numPort.Location = new Point(44, 6);
            numPort.Maximum = new decimal(new int[] { 65553, 0, 0, 0 });
            numPort.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            numPort.Name = "numPort";
            numPort.Placeholder = "";
            numPort.Size = new Size(53, 23);
            numPort.TabIndex = 22;
            numPort.Tag = "Port";
            numPort.Text = "0";
            toolTip1.SetToolTip(numPort, "The Port, default is dependent on the Engine");
            numPort.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // txtSchema
            // 
            txtSchema.Location = new Point(62, 6);
            txtSchema.Name = "txtSchema";
            txtSchema.Size = new Size(128, 23);
            txtSchema.TabIndex = 1;
            txtSchema.Tag = "Schema";
            toolTip1.SetToolTip(txtSchema, "The target Database Schema");
            // 
            // txtInstance
            // 
            txtInstance.Location = new Point(65, 6);
            txtInstance.Name = "txtInstance";
            txtInstance.Size = new Size(128, 23);
            txtInstance.TabIndex = 1;
            txtInstance.Tag = "Instance";
            toolTip1.SetToolTip(txtInstance, "The instance (applicable for Sql Server or Oracle)");
            // 
            // btnRemove
            // 
            btnRemove.Location = new Point(165, 4);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(75, 23);
            btnRemove.TabIndex = 2;
            btnRemove.Tag = "Remove";
            btnRemove.Text = "&Remove";
            toolTip1.SetToolTip(btnRemove, "Removes the designated Database Connection\r\n");
            btnRemove.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(84, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Tag = "Cancel";
            btnCancel.Text = "&Cancel";
            toolTip1.SetToolTip(btnCancel, "Cancels the operation and closes the control");
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(3, 4);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 0;
            btnUpdate.Tag = "Update";
            btnUpdate.Text = "&Update";
            toolTip1.SetToolTip(btnUpdate, "Adds/Modifies the Database Info");
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnTest
            // 
            btnTest.Location = new Point(246, 4);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(75, 23);
            btnTest.TabIndex = 20;
            btnTest.Tag = "Test";
            btnTest.Text = "&Test";
            toolTip1.SetToolTip(btnTest, "Tests the database connection");
            btnTest.UseVisualStyleBackColor = true;
            // 
            // btnShow
            // 
            btnShow.Location = new Point(215, 6);
            btnShow.Name = "btnShow";
            btnShow.Size = new Size(75, 23);
            btnShow.TabIndex = 10;
            btnShow.Tag = "Show";
            btnShow.Text = "Show";
            toolTip1.SetToolTip(btnShow, "Hides or shows the password");
            btnShow.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(label8);
            panel2.Controls.Add(cboEngine);
            panel2.Location = new Point(41, 61);
            panel2.Name = "panel2";
            panel2.Size = new Size(256, 35);
            panel2.TabIndex = 2;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 11);
            label8.Name = "label8";
            label8.Size = new Size(98, 15);
            label8.TabIndex = 2;
            label8.Text = "Database Engine";
            // 
            // panel3
            // 
            panel3.Controls.Add(txtHost);
            panel3.Controls.Add(label2);
            panel3.Location = new Point(78, 98);
            panel3.Name = "panel3";
            panel3.Size = new Size(183, 35);
            panel3.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 10);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 0;
            label2.Text = "Host";
            // 
            // panel4
            // 
            panel4.Controls.Add(numPort);
            panel4.Controls.Add(label10);
            panel4.Location = new Point(114, 135);
            panel4.Name = "panel4";
            panel4.Size = new Size(111, 35);
            panel4.TabIndex = 4;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(8, 10);
            label10.Name = "label10";
            label10.Size = new Size(31, 15);
            label10.TabIndex = 21;
            label10.Text = "Port";
            // 
            // panel5
            // 
            panel5.Controls.Add(txtSchema);
            panel5.Controls.Add(label3);
            panel5.Location = new Point(69, 244);
            panel5.Name = "panel5";
            panel5.Size = new Size(200, 35);
            panel5.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(7, 10);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 0;
            label3.Text = "Schema";
            // 
            // panel6
            // 
            panel6.Controls.Add(txtInstance);
            panel6.Controls.Add(label4);
            panel6.Location = new Point(69, 281);
            panel6.Name = "panel6";
            panel6.Size = new Size(200, 35);
            panel6.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(7, 10);
            label4.Name = "label4";
            label4.Size = new Size(54, 15);
            label4.TabIndex = 0;
            label4.Text = "Instance";
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnTest);
            pnlButtons.Controls.Add(btnRemove);
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnUpdate);
            pnlButtons.Location = new Point(6, 318);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(327, 31);
            pnlButtons.TabIndex = 12;
            // 
            // panel7
            // 
            panel7.Controls.Add(txtUser);
            panel7.Controls.Add(label11);
            panel7.Location = new Point(69, 172);
            panel7.Name = "panel7";
            panel7.Size = new Size(200, 34);
            panel7.TabIndex = 13;
            // 
            // txtUser
            // 
            txtUser.Location = new Point(52, 6);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(142, 23);
            txtUser.TabIndex = 9;
            txtUser.Tag = "User";
            toolTip1.SetToolTip(txtUser, "Name of User Managing this Connection");
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(6, 10);
            label11.Name = "label11";
            label11.Size = new Size(33, 15);
            label11.TabIndex = 8;
            label11.Text = "User";
            // 
            // panel8
            // 
            panel8.Controls.Add(btnShow);
            panel8.Controls.Add(txtPassword);
            panel8.Controls.Add(label5);
            panel8.Location = new Point(23, 208);
            panel8.Name = "panel8";
            panel8.Size = new Size(292, 34);
            panel8.TabIndex = 14;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(69, 6);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.ReadOnly = true;
            txtPassword.Size = new Size(142, 23);
            txtPassword.TabIndex = 9;
            txtPassword.Tag = "Password";
            toolTip1.SetToolTip(txtPassword, "Password Readonly, Editing must be done via the Password Editor");
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 10);
            label5.Name = "label5";
            label5.Size = new Size(59, 15);
            label5.TabIndex = 8;
            label5.Text = "Password";
            // 
            // pnlRoot
            // 
            pnlRoot.AutoSize = true;
            pnlRoot.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlRoot.Controls.Add(lblTitle);
            pnlRoot.Controls.Add(panel8);
            pnlRoot.Controls.Add(panel1);
            pnlRoot.Controls.Add(panel7);
            pnlRoot.Controls.Add(panel2);
            pnlRoot.Controls.Add(pnlButtons);
            pnlRoot.Controls.Add(panel3);
            pnlRoot.Controls.Add(panel6);
            pnlRoot.Controls.Add(panel4);
            pnlRoot.Controls.Add(panel5);
            pnlRoot.Location = new Point(3, 3);
            pnlRoot.Name = "pnlRoot";
            pnlRoot.Size = new Size(336, 352);
            pnlRoot.TabIndex = 15;
            // 
            // UcDatabaseEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(pnlRoot);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Name = "UcDatabaseEditor";
            Size = new Size(342, 367);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            pnlButtons.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            pnlRoot.ResumeLayout(false);
            pnlRoot.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label lblTitle;
        private TextBox txtID;
        private Label label1;
        private ToolTip toolTip1;
        private Panel panel2;
        private Label label8;
        private ComboBox cboEngine;
        private Panel panel3;
        private TextBox txtHost;
        private Label label2;
        private Panel panel4;
        private Controls.NumericTextBox numPort;
        private Label label10;
        private Panel panel5;
        private TextBox txtSchema;
        private Label label3;
        private Panel panel6;
        private TextBox txtInstance;
        private Label label4;
        private Panel pnlButtons;
        private Button btnRemove;
        private Button btnCancel;
        private Button btnUpdate;
        private Button btnTest;
        private Panel panel7;
        private TextBox txtUser;
        private Label label11;
        private Panel panel8;
        private TextBox txtPassword;
        private Label label5;
        private Button btnShow;
        private Panel pnlRoot;
    }
}
