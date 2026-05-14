/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/UserControls/EditorBase.cs
 * File: EditorBase.cs
 * Version: 1.0.2
 * Created: 2026-05-13
 * Modified: 2026-05-14
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToolsUI.UserControls
{
    public partial class EditorBase : UserControl
    {
        public event EventHandler? RefreshRequested;
        public event EventHandler CloseRequested;

        protected void RequestRefresh()
        {
            RefreshRequested?.Invoke(this, EventArgs.Empty);
        }
        protected void RequestClose()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        public EditorBase()
        {
            InitializeComponent();
        }
    }
}
