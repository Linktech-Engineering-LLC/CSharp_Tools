/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Config/FrmConfig.Designer.cs
 * File: FrmConfig.Designer.cs
 * Version: 1.0.3
 * Created: None
 * Modified: 2026-05-18
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
            TreeNode treeNode1 = new TreeNode("Security");
            TreeNode treeNode2 = new TreeNode("Paths");
            TreeNode treeNode3 = new TreeNode("Database");
            pnlConfigure = new Panel();
            pnlContent = new Panel();
            tvConfig = new TreeView();
            toolTip1 = new ToolTip(components);
            btnClose = new Button();
            btnSave = new Button();
            pnlButtons = new Panel();
            pnlConfigure.SuspendLayout();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlConfigure
            // 
            pnlConfigure.Controls.Add(pnlContent);
            pnlConfigure.Controls.Add(tvConfig);
            pnlConfigure.Location = new Point(1, 3);
            pnlConfigure.Name = "pnlConfigure";
            pnlConfigure.Size = new Size(418, 261);
            pnlConfigure.TabIndex = 3;
            // 
            // pnlContent
            // 
            pnlContent.AutoScroll = true;
            pnlContent.AutoSize = true;
            pnlContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(192, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(226, 261);
            pnlContent.TabIndex = 4;
            // 
            // tvConfig
            // 
            tvConfig.Dock = DockStyle.Left;
            tvConfig.Location = new Point(0, 0);
            tvConfig.Name = "tvConfig";
            treeNode1.Name = "Node0";
            treeNode1.Tag = "ucSecurity";
            treeNode1.Text = "Security";
            treeNode2.Name = "NodePaths";
            treeNode2.Tag = "ucPaths";
            treeNode2.Text = "Paths";
            treeNode3.Name = "NodeDatabase";
            treeNode3.Tag = "ucDatabase";
            treeNode3.Text = "Database";
            tvConfig.Nodes.AddRange(new TreeNode[] { treeNode1, treeNode2, treeNode3 });
            tvConfig.Size = new Size(192, 261);
            tvConfig.TabIndex = 3;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(84, 5);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 23);
            btnClose.TabIndex = 1;
            btnClose.Tag = "Close";
            btnClose.Text = "&Close";
            toolTip1.SetToolTip(btnClose, "Cancels any changes to the configuration and closes the form");
            btnClose.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Enabled = false;
            btnSave.Location = new Point(3, 5);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 0;
            btnSave.Tag = "Save";
            btnSave.Text = "Sa&ve";
            toolTip1.SetToolTip(btnSave, "Accepts the Configuration Modifications and Saves the Results");
            btnSave.UseVisualStyleBackColor = true;
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnClose);
            pnlButtons.Controls.Add(btnSave);
            pnlButtons.Location = new Point(127, 270);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(167, 33);
            pnlButtons.TabIndex = 4;
            // 
            // FrmConfig
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            CancelButton = btnClose;
            ClientSize = new Size(425, 305);
            ControlBox = false;
            Controls.Add(pnlButtons);
            Controls.Add(pnlConfigure);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "FrmConfig";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmConfig";
            pnlConfigure.ResumeLayout(false);
            pnlConfigure.PerformLayout();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ToolTip toolTip1;
        private Panel pnlConfigure;
        private TreeView tvConfig;
        private Panel pnlContent;
        private Panel pnlButtons;
        private Button btnClose;
        private Button btnSave;
    }
}