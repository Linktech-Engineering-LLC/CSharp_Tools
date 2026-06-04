/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Config/FrmPassword.Designer.cs
 * File: FrmPassword.Designer.cs
 * Version: 1.0.1
 * Created: 2026-04-04
 * Modified: 2026-06-04
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
            grpPassword = new GroupBox();
            panel1 = new Panel();
            btnShow = new Button();
            txtPassword = new TextBox();
            lblAttempts = new Label();
            grpUser = new GroupBox();
            txtUser = new TextBox();
            toolTip1 = new ToolTip(components);
            pnlButtons = new Panel();
            btnCancel = new Button();
            btnOk = new Button();
            pnlPassword.SuspendLayout();
            grpPassword.SuspendLayout();
            panel1.SuspendLayout();
            grpUser.SuspendLayout();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlPassword
            // 
            pnlPassword.AutoSize = true;
            pnlPassword.Controls.Add(grpPassword);
            pnlPassword.Controls.Add(grpUser);
            pnlPassword.Location = new Point(3, 5);
            pnlPassword.Name = "pnlPassword";
            pnlPassword.Size = new Size(265, 175);
            pnlPassword.TabIndex = 0;
            // 
            // grpPassword
            // 
            grpPassword.Controls.Add(panel1);
            grpPassword.Controls.Add(lblAttempts);
            grpPassword.Location = new Point(4, 54);
            grpPassword.Name = "grpPassword";
            grpPassword.Size = new Size(256, 81);
            grpPassword.TabIndex = 1;
            grpPassword.TabStop = false;
            grpPassword.Text = "Password";
            // 
            // panel1
            // 
            panel1.Controls.Add(btnShow);
            panel1.Controls.Add(txtPassword);
            panel1.Location = new Point(7, 22);
            panel1.Name = "panel1";
            panel1.Size = new Size(243, 34);
            panel1.TabIndex = 3;
            // 
            // btnShow
            // 
            btnShow.Location = new Point(176, 5);
            btnShow.Name = "btnShow";
            btnShow.Size = new Size(55, 23);
            btnShow.TabIndex = 1;
            btnShow.Tag = "Show";
            btnShow.Text = "&Show";
            toolTip1.SetToolTip(btnShow, "Shows/Hides the Password");
            btnShow.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(6, 5);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(164, 23);
            txtPassword.TabIndex = 0;
            // 
            // lblAttempts
            // 
            lblAttempts.AutoSize = true;
            lblAttempts.Location = new Point(84, 59);
            lblAttempts.Name = "lblAttempts";
            lblAttempts.Size = new Size(89, 15);
            lblAttempts.TabIndex = 2;
            lblAttempts.Text = "Attempt x of y";
            // 
            // grpUser
            // 
            grpUser.Controls.Add(txtUser);
            grpUser.Location = new Point(43, 5);
            grpUser.Name = "grpUser";
            grpUser.Size = new Size(178, 48);
            grpUser.TabIndex = 0;
            grpUser.TabStop = false;
            grpUser.Text = "User";
            // 
            // txtUser
            // 
            txtUser.Location = new Point(7, 19);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(164, 23);
            txtUser.TabIndex = 0;
            txtUser.TabStop = false;
            toolTip1.SetToolTip(txtUser, "Username, defaults to current user");
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnOk);
            pnlButtons.Location = new Point(52, 142);
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
            ClientSize = new Size(269, 183);
            Controls.Add(pnlButtons);
            Controls.Add(pnlPassword);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Name = "FrmPassword";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Password Authenticator";
            pnlPassword.ResumeLayout(false);
            grpPassword.ResumeLayout(false);
            grpPassword.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            grpUser.ResumeLayout(false);
            grpUser.PerformLayout();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlPassword;
        private ToolTip toolTip1;
        private Panel pnlButtons;
        private Button btnCancel;
        private Button btnOk;
        private GroupBox grpPassword;
        private GroupBox grpUser;
        private TextBox txtUser;
        private Panel panel1;
        private Label lblAttempts;
        private Button btnShow;
        private TextBox txtPassword;
    }
}