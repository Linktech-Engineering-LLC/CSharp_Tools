/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/UserControls/UcPathEditor.Designer.cs
 * File: UcPathEditor.Designer.cs
 * Version: 1.0.0
 * Created: 2026-05-11
 * Modified: 2026-05-11
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
namespace ToolsUI.UserControls
{
    partial class UcPathEditor
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
            pnlEditor = new Panel();
            pnlButtons = new Panel();
            btnRemove = new Button();
            btnCancel = new Button();
            btnUpdate = new Button();
            panel2 = new Panel();
            btnBrowse = new Button();
            label5 = new Label();
            txtPath = new TextBox();
            panel1 = new Panel();
            cboPathType = new ComboBox();
            label2 = new Label();
            pnlPathName = new Panel();
            cboPathName = new ComboBox();
            label1 = new Label();
            lblTitle = new Label();
            toolTip1 = new ToolTip(components);
            pnlEditor.SuspendLayout();
            pnlButtons.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            pnlPathName.SuspendLayout();
            SuspendLayout();
            // 
            // pnlEditor
            // 
            pnlEditor.Controls.Add(pnlButtons);
            pnlEditor.Controls.Add(panel2);
            pnlEditor.Controls.Add(panel1);
            pnlEditor.Controls.Add(pnlPathName);
            pnlEditor.Controls.Add(lblTitle);
            pnlEditor.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            pnlEditor.Location = new Point(0, 0);
            pnlEditor.Name = "pnlEditor";
            pnlEditor.Size = new Size(326, 189);
            pnlEditor.TabIndex = 0;
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnRemove);
            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnUpdate);
            pnlButtons.Location = new Point(39, 146);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(248, 31);
            pnlButtons.TabIndex = 12;
            // 
            // btnRemove
            // 
            btnRemove.Location = new Point(167, 4);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(75, 23);
            btnRemove.TabIndex = 2;
            btnRemove.Tag = "Remove";
            btnRemove.Text = "&Remove";
            toolTip1.SetToolTip(btnRemove, "Remove the selected Path");
            btnRemove.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(84, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Tag = "Cancel";
            btnCancel.Text = "&Cancel";
            toolTip1.SetToolTip(btnCancel, "Cancel the Operation and Close the UC");
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
            toolTip1.SetToolTip(btnUpdate, "Update/Modify the Paths");
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnBrowse);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(txtPath);
            panel2.Location = new Point(6, 104);
            panel2.Name = "panel2";
            panel2.Size = new Size(314, 37);
            panel2.TabIndex = 3;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(229, 7);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 9;
            btnBrowse.Tag = "Browse";
            btnBrowse.Text = "Browse";
            toolTip1.SetToolTip(btnBrowse, "Browse for the Target Folder");
            btnBrowse.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(8, 11);
            label5.Name = "label5";
            label5.Size = new Size(32, 15);
            label5.TabIndex = 7;
            label5.Text = "Path";
            // 
            // txtPath
            // 
            txtPath.Location = new Point(52, 7);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(171, 23);
            txtPath.TabIndex = 8;
            txtPath.Tag = "Path";
            toolTip1.SetToolTip(txtPath, "The Selected Folder");
            // 
            // panel1
            // 
            panel1.Controls.Add(cboPathType);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(49, 64);
            panel1.Name = "panel1";
            panel1.Size = new Size(229, 35);
            panel1.TabIndex = 2;
            // 
            // cboPathType
            // 
            cboPathType.FormattingEnabled = true;
            cboPathType.Location = new Point(80, 6);
            cboPathType.Name = "cboPathType";
            cboPathType.Size = new Size(139, 23);
            cboPathType.TabIndex = 14;
            cboPathType.Tag = "PathType";
            toolTip1.SetToolTip(cboPathType, "The Type of Path Classification");
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 10);
            label2.Name = "label2";
            label2.Size = new Size(61, 15);
            label2.TabIndex = 13;
            label2.Text = "Path Type";
            // 
            // pnlPathName
            // 
            pnlPathName.Controls.Add(cboPathName);
            pnlPathName.Controls.Add(label1);
            pnlPathName.Location = new Point(49, 24);
            pnlPathName.Name = "pnlPathName";
            pnlPathName.Size = new Size(229, 35);
            pnlPathName.TabIndex = 1;
            // 
            // cboPathName
            // 
            cboPathName.FormattingEnabled = true;
            cboPathName.Location = new Point(82, 6);
            cboPathName.Name = "cboPathName";
            cboPathName.Size = new Size(139, 23);
            cboPathName.TabIndex = 13;
            cboPathName.Tag = "PathName";
            toolTip1.SetToolTip(cboPathName, "The Pathname used for the Key for this Path Selection");
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 10);
            label1.Name = "label1";
            label1.Size = new Size(68, 15);
            label1.TabIndex = 12;
            label1.Text = "Path Name";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(129, 4);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(68, 15);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Path Editor";
            // 
            // UcPathEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnlEditor);
            Name = "UcPathEditor";
            Size = new Size(328, 189);
            pnlEditor.ResumeLayout(false);
            pnlEditor.PerformLayout();
            pnlButtons.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            pnlPathName.ResumeLayout(false);
            pnlPathName.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlEditor;
        private Label lblTitle;
        private Panel pnlPathName;
        private ComboBox cboPathName;
        private Label label1;
        private Panel panel1;
        private ComboBox cboPathType;
        private Label label2;
        private Panel panel2;
        private Button btnBrowse;
        private Label label5;
        private TextBox txtPath;
        private Panel pnlButtons;
        private Button btnRemove;
        private Button btnCancel;
        private Button btnUpdate;
        private ToolTip toolTip1;
    }
}
