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
 * Version: 1.0.1
 * Created: None
 * Modified: 2026-05-13
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
            pnlConfigure.SuspendLayout();
            SuspendLayout();
            // 
            // pnlConfigure
            // 
            pnlConfigure.Controls.Add(pnlContent);
            pnlConfigure.Controls.Add(tvConfig);
            pnlConfigure.Location = new Point(1, 3);
            pnlConfigure.Name = "pnlConfigure";
            pnlConfigure.Size = new Size(568, 358);
            pnlConfigure.TabIndex = 3;
            // 
            // pnlContent
            // 
            pnlContent.AutoScroll = true;
            pnlContent.AutoSize = true;
            pnlContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(223, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(345, 358);
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
            tvConfig.Size = new Size(223, 358);
            tvConfig.TabIndex = 3;
            // 
            // FrmConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(581, 390);
            Controls.Add(pnlConfigure);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "FrmConfig";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmConfig";
            pnlConfigure.ResumeLayout(false);
            pnlConfigure.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ToolTip toolTip1;
        private Panel pnlConfigure;
        private TreeView tvConfig;
        private Panel pnlContent;
    }
}