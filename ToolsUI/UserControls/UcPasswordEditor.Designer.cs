/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/UserControls/UcPasswordEditor.Designer.cs
 * File: UcPasswordEditor.Designer.cs
 * Version: 1.0.0
 * Created: 2026-05-11
 * Modified: 2026-05-11
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
namespace ToolsUI.UserControls
{
    partial class UcPasswordEditor
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
            pnlTarget = new Panel();
            cboContext = new ComboBox();
            label2 = new Label();
            cboTarget = new ComboBox();
            label1 = new Label();
            pnlStyle = new Panel();
            cboRepresentation = new ComboBox();
            chkVault = new CheckBox();
            label3 = new Label();
            pnlConfirm = new Panel();
            btnConfirm = new Button();
            txtConfirm = new TextBox();
            label4 = new Label();
            pnlNew = new Panel();
            btnNew = new Button();
            txtNew = new TextBox();
            label5 = new Label();
            pnlCurrent = new Panel();
            btnCurrent = new Button();
            txtCurrent = new TextBox();
            label6 = new Label();
            toolTip1 = new ToolTip(components);
            btnCancel = new Button();
            btnUpdate = new Button();
            btnRemove = new Button();
            txtKey = new TextBox();
            pnlButtons = new Panel();
            lblTitle = new Label();
            panel1 = new Panel();
            label7 = new Label();
            pnlEditor = new Panel();
            pnlTarget.SuspendLayout();
            pnlStyle.SuspendLayout();
            pnlConfirm.SuspendLayout();
            pnlNew.SuspendLayout();
            pnlCurrent.SuspendLayout();
            pnlButtons.SuspendLayout();
            panel1.SuspendLayout();
            pnlEditor.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTarget
            // 
            pnlTarget.Controls.Add(cboContext);
            pnlTarget.Controls.Add(label2);
            pnlTarget.Controls.Add(cboTarget);
            pnlTarget.Controls.Add(label1);
            pnlTarget.Location = new Point(7, 28);
            pnlTarget.Name = "pnlTarget";
            pnlTarget.Size = new Size(429, 39);
            pnlTarget.TabIndex = 0;
            // 
            // cboContext
            // 
            cboContext.Enabled = false;
            cboContext.FormattingEnabled = true;
            cboContext.Location = new Point(274, 6);
            cboContext.Name = "cboContext";
            cboContext.Size = new Size(132, 23);
            cboContext.TabIndex = 1;
            toolTip1.SetToolTip(cboContext, "Enabled when Target is the Database Password\r\nSelects the available connections configured");
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(199, 10);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 0;
            label2.Text = "Connection";
            // 
            // cboTarget
            // 
            cboTarget.FormattingEnabled = true;
            cboTarget.Location = new Point(57, 6);
            cboTarget.Name = "cboTarget";
            cboTarget.Size = new Size(132, 23);
            cboTarget.TabIndex = 1;
            cboTarget.Tag = "Target";
            toolTip1.SetToolTip(cboTarget, "Target Password to be modified/removed");
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 10);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 0;
            label1.Text = "Target";
            // 
            // pnlStyle
            // 
            pnlStyle.Controls.Add(cboRepresentation);
            pnlStyle.Controls.Add(chkVault);
            pnlStyle.Controls.Add(label3);
            pnlStyle.Location = new Point(67, 68);
            pnlStyle.Name = "pnlStyle";
            pnlStyle.Size = new Size(309, 34);
            pnlStyle.TabIndex = 6;
            // 
            // cboRepresentation
            // 
            cboRepresentation.FormattingEnabled = true;
            cboRepresentation.Location = new Point(166, 3);
            cboRepresentation.Name = "cboRepresentation";
            cboRepresentation.Size = new Size(134, 23);
            cboRepresentation.TabIndex = 2;
            cboRepresentation.Tag = "Representation";
            toolTip1.SetToolTip(cboRepresentation, "The style of the target password");
            // 
            // chkVault
            // 
            chkVault.AutoSize = true;
            chkVault.Location = new Point(5, 5);
            chkVault.Name = "chkVault";
            chkVault.Size = new Size(54, 19);
            chkVault.TabIndex = 0;
            chkVault.Text = "Vault";
            toolTip1.SetToolTip(chkVault, "Checked if the Password is stored in the Windows Vault");
            chkVault.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(66, 7);
            label3.Name = "label3";
            label3.Size = new Size(93, 15);
            label3.TabIndex = 1;
            label3.Text = "Representation";
            // 
            // pnlConfirm
            // 
            pnlConfirm.Controls.Add(btnConfirm);
            pnlConfirm.Controls.Add(txtConfirm);
            pnlConfirm.Controls.Add(label4);
            pnlConfirm.Location = new Point(46, 207);
            pnlConfirm.Name = "pnlConfirm";
            pnlConfirm.Size = new Size(351, 34);
            pnlConfirm.TabIndex = 10;
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(257, 4);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(75, 23);
            btnConfirm.TabIndex = 5;
            btnConfirm.Tag = "ShowConfirm";
            btnConfirm.Text = "Show";
            toolTip1.SetToolTip(btnConfirm, "Hides or shows the password");
            btnConfirm.UseVisualStyleBackColor = true;
            // 
            // txtConfirm
            // 
            txtConfirm.Location = new Point(74, 4);
            txtConfirm.Name = "txtConfirm";
            txtConfirm.PasswordChar = '*';
            txtConfirm.Size = new Size(176, 23);
            txtConfirm.TabIndex = 4;
            toolTip1.SetToolTip(txtConfirm, "Confirmation of the new password");
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(8, 8);
            label4.Name = "label4";
            label4.Size = new Size(52, 15);
            label4.TabIndex = 3;
            label4.Text = "Confirm";
            // 
            // pnlNew
            // 
            pnlNew.Controls.Add(btnNew);
            pnlNew.Controls.Add(txtNew);
            pnlNew.Controls.Add(label5);
            pnlNew.Location = new Point(46, 172);
            pnlNew.Name = "pnlNew";
            pnlNew.Size = new Size(351, 34);
            pnlNew.TabIndex = 9;
            // 
            // btnNew
            // 
            btnNew.Location = new Point(257, 4);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(75, 23);
            btnNew.TabIndex = 5;
            btnNew.Tag = "ShowNew";
            btnNew.Text = "Show";
            toolTip1.SetToolTip(btnNew, "Hides or shows the password");
            btnNew.UseVisualStyleBackColor = true;
            // 
            // txtNew
            // 
            txtNew.Location = new Point(74, 4);
            txtNew.Name = "txtNew";
            txtNew.PasswordChar = '*';
            txtNew.Size = new Size(176, 23);
            txtNew.TabIndex = 4;
            toolTip1.SetToolTip(txtNew, "The New Password");
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(8, 8);
            label5.Name = "label5";
            label5.Size = new Size(33, 15);
            label5.TabIndex = 3;
            label5.Text = "New";
            // 
            // pnlCurrent
            // 
            pnlCurrent.Controls.Add(btnCurrent);
            pnlCurrent.Controls.Add(txtCurrent);
            pnlCurrent.Controls.Add(label6);
            pnlCurrent.Location = new Point(46, 137);
            pnlCurrent.Name = "pnlCurrent";
            pnlCurrent.Size = new Size(351, 34);
            pnlCurrent.TabIndex = 8;
            // 
            // btnCurrent
            // 
            btnCurrent.Location = new Point(257, 4);
            btnCurrent.Name = "btnCurrent";
            btnCurrent.Size = new Size(75, 23);
            btnCurrent.TabIndex = 5;
            btnCurrent.Tag = "ShowCurrent";
            btnCurrent.Text = "Show";
            toolTip1.SetToolTip(btnCurrent, "Hides or shows the password");
            btnCurrent.UseVisualStyleBackColor = true;
            // 
            // txtCurrent
            // 
            txtCurrent.Location = new Point(74, 4);
            txtCurrent.Name = "txtCurrent";
            txtCurrent.PasswordChar = '*';
            txtCurrent.Size = new Size(176, 23);
            txtCurrent.TabIndex = 4;
            toolTip1.SetToolTip(txtCurrent, "The current password");
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(8, 8);
            label6.Name = "label6";
            label6.Size = new Size(50, 15);
            label6.TabIndex = 3;
            label6.Text = "Current";
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(84, 3);
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
            btnUpdate.Location = new Point(3, 3);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 0;
            btnUpdate.Tag = "Update";
            btnUpdate.Text = "&Update";
            toolTip1.SetToolTip(btnUpdate, "Adds/Modifies the Password Info");
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            btnRemove.Location = new Point(167, 4);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(75, 23);
            btnRemove.TabIndex = 2;
            btnRemove.Tag = "Remove";
            btnRemove.Text = "&Remove";
            toolTip1.SetToolTip(btnRemove, "Removes the designated password");
            btnRemove.UseVisualStyleBackColor = true;
            // 
            // txtKey
            // 
            txtKey.Location = new Point(42, 4);
            txtKey.Name = "txtKey";
            txtKey.Size = new Size(176, 23);
            txtKey.TabIndex = 4;
            toolTip1.SetToolTip(txtKey, "The Password Key");
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnRemove);
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnUpdate);
            pnlButtons.Location = new Point(97, 242);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(248, 31);
            pnlButtons.TabIndex = 11;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(174, 6);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(95, 15);
            lblTitle.TabIndex = 12;
            lblTitle.Text = "Password Editor";
            // 
            // panel1
            // 
            panel1.Controls.Add(txtKey);
            panel1.Controls.Add(label7);
            panel1.Location = new Point(108, 102);
            panel1.Name = "panel1";
            panel1.Size = new Size(227, 34);
            panel1.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(8, 8);
            label7.Name = "label7";
            label7.Size = new Size(28, 15);
            label7.TabIndex = 3;
            label7.Text = "Key";
            // 
            // pnlEditor
            // 
            pnlEditor.Controls.Add(lblTitle);
            pnlEditor.Controls.Add(panel1);
            pnlEditor.Controls.Add(pnlTarget);
            pnlEditor.Controls.Add(pnlStyle);
            pnlEditor.Controls.Add(pnlButtons);
            pnlEditor.Controls.Add(pnlCurrent);
            pnlEditor.Controls.Add(pnlConfirm);
            pnlEditor.Controls.Add(pnlNew);
            pnlEditor.Dock = DockStyle.Fill;
            pnlEditor.Location = new Point(0, 0);
            pnlEditor.Name = "pnlEditor";
            pnlEditor.Size = new Size(443, 278);
            pnlEditor.TabIndex = 14;
            // 
            // UcPasswordEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(pnlEditor);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Name = "UcPasswordEditor";
            Size = new Size(443, 278);
            pnlTarget.ResumeLayout(false);
            pnlTarget.PerformLayout();
            pnlStyle.ResumeLayout(false);
            pnlStyle.PerformLayout();
            pnlConfirm.ResumeLayout(false);
            pnlConfirm.PerformLayout();
            pnlNew.ResumeLayout(false);
            pnlNew.PerformLayout();
            pnlCurrent.ResumeLayout(false);
            pnlCurrent.PerformLayout();
            pnlButtons.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            pnlEditor.ResumeLayout(false);
            pnlEditor.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTarget;
        private Label label1;
        private ComboBox cboTarget;
        private ComboBox cboContext;
        private Label label2;
        private Panel pnlStyle;
        private ComboBox cboRepresentation;
        private CheckBox chkVault;
        private Label label3;
        private Panel pnlConfirm;
        private Button btnConfirm;
        private TextBox txtConfirm;
        private Label label4;
        private Panel pnlNew;
        private Button btnNew;
        private TextBox txtNew;
        private Label label5;
        private Panel pnlCurrent;
        private Button btnCurrent;
        private TextBox txtCurrent;
        private Label label6;
        private ToolTip toolTip1;
        private Panel pnlButtons;
        private Button btnCancel;
        private Button btnUpdate;
        private Button btnRemove;
        private Label lblTitle;
        private Panel panel1;
        private TextBox txtKey;
        private Label label7;
        private Panel pnlEditor;
    }
}
