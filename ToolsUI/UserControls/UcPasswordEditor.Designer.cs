/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/UserControls/UcPasswordEditor.Designer.cs
 * File: UcPasswordEditor.Designer.cs
 * Version: 1.0.3
 * Created: 2026-05-11
 * Modified: 2026-06-04
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
            cboTarget = new ComboBox();
            label1 = new Label();
            cboContext = new ComboBox();
            label2 = new Label();
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
            btnUpdate = new Button();
            btnRemove = new Button();
            txtID = new TextBox();
            pnlButtons = new Panel();
            lblTitle = new Label();
            pnlRoot = new Panel();
            pnlDatabase = new Panel();
            label7 = new Label();
            pnlTarget.SuspendLayout();
            pnlConfirm.SuspendLayout();
            pnlNew.SuspendLayout();
            pnlCurrent.SuspendLayout();
            pnlButtons.SuspendLayout();
            pnlRoot.SuspendLayout();
            pnlDatabase.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTarget
            // 
            pnlTarget.Controls.Add(cboTarget);
            pnlTarget.Controls.Add(label1);
            pnlTarget.Location = new Point(87, 28);
            pnlTarget.Name = "pnlTarget";
            pnlTarget.Size = new Size(195, 39);
            pnlTarget.TabIndex = 0;
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
            // cboContext
            // 
            cboContext.DropDownStyle = ComboBoxStyle.DropDownList;
            cboContext.FormattingEnabled = true;
            cboContext.Location = new Point(82, 3);
            cboContext.Name = "cboContext";
            cboContext.Size = new Size(132, 23);
            cboContext.TabIndex = 1;
            cboContext.Tag = "Connection";
            toolTip1.SetToolTip(cboContext, "Enabled when Target is the Database Password\r\nSelects the available connections configured");
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 7);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 0;
            label2.Text = "Connection";
            // 
            // pnlConfirm
            // 
            pnlConfirm.Controls.Add(btnConfirm);
            pnlConfirm.Controls.Add(txtConfirm);
            pnlConfirm.Controls.Add(label4);
            pnlConfirm.Location = new Point(24, 175);
            pnlConfirm.Name = "pnlConfirm";
            pnlConfirm.Size = new Size(321, 34);
            pnlConfirm.TabIndex = 10;
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(257, 4);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(48, 23);
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
            pnlNew.Location = new Point(24, 140);
            pnlNew.Name = "pnlNew";
            pnlNew.Size = new Size(321, 34);
            pnlNew.TabIndex = 9;
            // 
            // btnNew
            // 
            btnNew.Location = new Point(257, 4);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(48, 23);
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
            pnlCurrent.Location = new Point(24, 105);
            pnlCurrent.Name = "pnlCurrent";
            pnlCurrent.Size = new Size(321, 34);
            pnlCurrent.TabIndex = 8;
            // 
            // btnCurrent
            // 
            btnCurrent.AutoSize = true;
            btnCurrent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnCurrent.Location = new Point(257, 4);
            btnCurrent.Name = "btnCurrent";
            btnCurrent.Size = new Size(48, 25);
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
            txtCurrent.Tag = "Current";
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
            btnRemove.Location = new Point(84, 4);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(75, 23);
            btnRemove.TabIndex = 2;
            btnRemove.Tag = "Remove";
            btnRemove.Text = "&Remove";
            toolTip1.SetToolTip(btnRemove, "Removes the designated password");
            btnRemove.UseVisualStyleBackColor = true;
            // 
            // txtID
            // 
            txtID.Location = new Point(252, 3);
            txtID.Name = "txtID";
            txtID.Size = new Size(100, 23);
            txtID.TabIndex = 3;
            toolTip1.SetToolTip(txtID, "The ID String for the Database Connection");
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnRemove);
            pnlButtons.Controls.Add(btnUpdate);
            pnlButtons.Location = new Point(98, 210);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(172, 31);
            pnlButtons.TabIndex = 11;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(137, 6);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(95, 15);
            lblTitle.TabIndex = 12;
            lblTitle.Text = "Password Editor";
            // 
            // pnlRoot
            // 
            pnlRoot.AutoSize = true;
            pnlRoot.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlRoot.Controls.Add(pnlDatabase);
            pnlRoot.Controls.Add(lblTitle);
            pnlRoot.Controls.Add(pnlCurrent);
            pnlRoot.Controls.Add(pnlButtons);
            pnlRoot.Controls.Add(pnlTarget);
            pnlRoot.Controls.Add(pnlConfirm);
            pnlRoot.Controls.Add(pnlNew);
            pnlRoot.Location = new Point(3, 3);
            pnlRoot.Name = "pnlRoot";
            pnlRoot.Size = new Size(367, 244);
            pnlRoot.TabIndex = 14;
            // 
            // pnlDatabase
            // 
            pnlDatabase.Controls.Add(txtID);
            pnlDatabase.Controls.Add(label7);
            pnlDatabase.Controls.Add(cboContext);
            pnlDatabase.Controls.Add(label2);
            pnlDatabase.Enabled = false;
            pnlDatabase.Location = new Point(5, 71);
            pnlDatabase.Name = "pnlDatabase";
            pnlDatabase.Size = new Size(359, 32);
            pnlDatabase.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(226, 7);
            label7.Name = "label7";
            label7.Size = new Size(20, 15);
            label7.TabIndex = 2;
            label7.Text = "ID";
            // 
            // UcPasswordEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(pnlRoot);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Name = "UcPasswordEditor";
            Size = new Size(372, 246);
            pnlTarget.ResumeLayout(false);
            pnlTarget.PerformLayout();
            pnlConfirm.ResumeLayout(false);
            pnlConfirm.PerformLayout();
            pnlNew.ResumeLayout(false);
            pnlNew.PerformLayout();
            pnlCurrent.ResumeLayout(false);
            pnlCurrent.PerformLayout();
            pnlButtons.ResumeLayout(false);
            pnlRoot.ResumeLayout(false);
            pnlRoot.PerformLayout();
            pnlDatabase.ResumeLayout(false);
            pnlDatabase.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlTarget;
        private Label label1;
        private ComboBox cboTarget;
        private ComboBox cboContext;
        private Label label2;
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
        private Button btnUpdate;
        private Button btnRemove;
        private Label lblTitle;
        private Panel pnlRoot;
        private Panel pnlDatabase;
        private TextBox txtID;
        private Label label7;
    }
}
