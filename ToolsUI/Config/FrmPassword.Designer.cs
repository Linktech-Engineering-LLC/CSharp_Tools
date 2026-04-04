/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Config/FrmPassword.Designer.cs
 * File: FrmPassword.Designer.cs
 * Created: 2026-04-04
 * Modified: 2026-04-04
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
namespace ToolsUI.Config
{
    partial class FrmPassword
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
            pnlPassword = new Panel();
            pnlConfirm = new Panel();
            btnConfirm = new Button();
            txtConfirm = new TextBox();
            label4 = new Label();
            pnlNew = new Panel();
            btnNew = new Button();
            txtNew = new TextBox();
            label3 = new Label();
            pnlCurrent = new Panel();
            btnCurrent = new Button();
            txtCurrent = new TextBox();
            label2 = new Label();
            pnlStyle = new Panel();
            cboRepresentation = new ComboBox();
            cbxVault = new CheckBox();
            label1 = new Label();
            toolTip1 = new ToolTip(components);
            pnlButtons = new Panel();
            btnCancel = new Button();
            btnOk = new Button();
            pnlPassword.SuspendLayout();
            pnlConfirm.SuspendLayout();
            pnlNew.SuspendLayout();
            pnlCurrent.SuspendLayout();
            pnlStyle.SuspendLayout();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPassword
            // 
            pnlPassword.Controls.Add(pnlConfirm);
            pnlPassword.Controls.Add(pnlNew);
            pnlPassword.Controls.Add(pnlCurrent);
            pnlPassword.Controls.Add(pnlStyle);
            pnlPassword.Location = new Point(3, 5);
            pnlPassword.Name = "pnlPassword";
            pnlPassword.Size = new Size(361, 156);
            pnlPassword.TabIndex = 0;
            // 
            // pnlConfirm
            // 
            pnlConfirm.Controls.Add(btnConfirm);
            pnlConfirm.Controls.Add(txtConfirm);
            pnlConfirm.Controls.Add(label4);
            pnlConfirm.Location = new Point(4, 122);
            pnlConfirm.Name = "pnlConfirm";
            pnlConfirm.Size = new Size(351, 34);
            pnlConfirm.TabIndex = 7;
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(257, 4);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(75, 23);
            btnConfirm.TabIndex = 5;
            btnConfirm.Tag = "ConfirmShow";
            btnConfirm.Text = "Show";
            toolTip1.SetToolTip(btnConfirm, "Shows or Hides the Password");
            btnConfirm.UseVisualStyleBackColor = true;
            // 
            // txtConfirm
            // 
            txtConfirm.Location = new Point(74, 4);
            txtConfirm.Name = "txtConfirm";
            txtConfirm.PasswordChar = '*';
            txtConfirm.Size = new Size(176, 23);
            txtConfirm.TabIndex = 4;
            toolTip1.SetToolTip(txtConfirm, "Confirm the Password");
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
            pnlNew.Controls.Add(label3);
            pnlNew.Location = new Point(4, 82);
            pnlNew.Name = "pnlNew";
            pnlNew.Size = new Size(351, 34);
            pnlNew.TabIndex = 7;
            // 
            // btnNew
            // 
            btnNew.Location = new Point(257, 4);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(75, 23);
            btnNew.TabIndex = 5;
            btnNew.Tag = "NewShow";
            btnNew.Text = "Show";
            toolTip1.SetToolTip(btnNew, "Shows or Hides the Password");
            btnNew.UseVisualStyleBackColor = true;
            // 
            // txtNew
            // 
            txtNew.Location = new Point(74, 4);
            txtNew.Name = "txtNew";
            txtNew.PasswordChar = '*';
            txtNew.Size = new Size(176, 23);
            txtNew.TabIndex = 4;
            toolTip1.SetToolTip(txtNew, "New Password");
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 8);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 3;
            label3.Text = "New";
            // 
            // pnlCurrent
            // 
            pnlCurrent.Controls.Add(btnCurrent);
            pnlCurrent.Controls.Add(txtCurrent);
            pnlCurrent.Controls.Add(label2);
            pnlCurrent.Location = new Point(4, 42);
            pnlCurrent.Name = "pnlCurrent";
            pnlCurrent.Size = new Size(351, 34);
            pnlCurrent.TabIndex = 6;
            // 
            // btnCurrent
            // 
            btnCurrent.Location = new Point(257, 4);
            btnCurrent.Name = "btnCurrent";
            btnCurrent.Size = new Size(75, 23);
            btnCurrent.TabIndex = 5;
            btnCurrent.Tag = "CurrentShow";
            btnCurrent.Text = "Show";
            toolTip1.SetToolTip(btnCurrent, "Shows or Hides the Password");
            btnCurrent.UseVisualStyleBackColor = true;
            // 
            // txtCurrent
            // 
            txtCurrent.Location = new Point(74, 4);
            txtCurrent.Name = "txtCurrent";
            txtCurrent.PasswordChar = '*';
            txtCurrent.Size = new Size(176, 23);
            txtCurrent.TabIndex = 4;
            toolTip1.SetToolTip(txtCurrent, "Current Password");
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(8, 8);
            label2.Name = "label2";
            label2.Size = new Size(50, 15);
            label2.TabIndex = 3;
            label2.Text = "Current";
            // 
            // pnlStyle
            // 
            pnlStyle.Controls.Add(cboRepresentation);
            pnlStyle.Controls.Add(cbxVault);
            pnlStyle.Controls.Add(label1);
            pnlStyle.Location = new Point(25, 3);
            pnlStyle.Name = "pnlStyle";
            pnlStyle.Size = new Size(309, 34);
            pnlStyle.TabIndex = 5;
            // 
            // cboRepresentation
            // 
            cboRepresentation.FormattingEnabled = true;
            cboRepresentation.Location = new Point(159, 3);
            cboRepresentation.Name = "cboRepresentation";
            cboRepresentation.Size = new Size(134, 23);
            cboRepresentation.TabIndex = 2;
            cboRepresentation.Tag = "Representation";
            toolTip1.SetToolTip(cboRepresentation, "The Respresentation Storage Type");
            // 
            // cbxVault
            // 
            cbxVault.AutoSize = true;
            cbxVault.Location = new Point(5, 5);
            cbxVault.Name = "cbxVault";
            cbxVault.Size = new Size(54, 19);
            cbxVault.TabIndex = 0;
            cbxVault.Text = "Vault";
            toolTip1.SetToolTip(cbxVault, "Check if Storing Password in Vault\r\nThis will disable the Dropdown for Password Representation");
            cbxVault.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(58, 7);
            label1.Name = "label1";
            label1.Size = new Size(93, 15);
            label1.TabIndex = 1;
            label1.Text = "Representation";
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnOk);
            pnlButtons.Location = new Point(103, 167);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(161, 31);
            pnlButtons.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(84, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Tag = "Cancel";
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            btnOk.Location = new Point(3, 3);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(75, 23);
            btnOk.TabIndex = 0;
            btnOk.Tag = "OK";
            btnOk.Text = "O&K";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // FrmPassword
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(362, 200);
            Controls.Add(pnlButtons);
            Controls.Add(pnlPassword);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Name = "FrmPassword";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Define or Update Password";
            pnlPassword.ResumeLayout(false);
            pnlConfirm.ResumeLayout(false);
            pnlConfirm.PerformLayout();
            pnlNew.ResumeLayout(false);
            pnlNew.PerformLayout();
            pnlCurrent.ResumeLayout(false);
            pnlCurrent.PerformLayout();
            pnlStyle.ResumeLayout(false);
            pnlStyle.PerformLayout();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlPassword;
        private CheckBox cbxVault;
        private ToolTip toolTip1;
        private Label label1;
        private TextBox txtCurrent;
        private Label label2;
        private ComboBox cboRepresentation;
        private Panel pnlNew;
        private Panel pnlConfirm;
        private Button btnConfirm;
        private TextBox txtConfirm;
        private Label label4;
        private Button btnNew;
        private TextBox txtNew;
        private Label label3;
        private Panel pnlCurrent;
        private Button btnCurrent;
        private Panel pnlStyle;
        private Panel pnlButtons;
        private Button btnCancel;
        private Button btnOk;
    }
}