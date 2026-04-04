/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Layout/TabLayoutManager.cs
 * File: TabLayoutManager.cs
 * Created: 2026-03-31
 * Modified: 2026-04-03
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 *     Centralized, deterministic layout manager for tab-based configuration forms.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ToolsUI.Layout
{
    public sealed class TabLayoutManager
    {
        private readonly Form _form;
        private readonly TabControl _tabs;
        private readonly Dictionary<string, TabLayoutInfo> _map;
        private int CalculateContentHeight(TabPage page)
        {
            int maxBottom = 0;

            foreach (Control c in page.Controls)
            {
                if (!c.Visible)
                    continue;

                int bottom = c.Bottom + c.Margin.Bottom;
                if (bottom > maxBottom)
                    maxBottom = bottom;
            }

            return maxBottom;
        }
        private int CalculateContentWidth(TabPage page)
        {
            int maxRight = 0;

            foreach (Control c in page.Controls)
            {
                if (!c.Visible)
                    continue;

                int right = c.Right + c.Margin.Right;
                if (right > maxRight)
                    maxRight = right;
            }

            return maxRight;
        }

        public TabLayoutManager(
            Form form,
            TabControl tabs,
            Dictionary<string, TabLayoutInfo> pages)
        {
            _form = form;
            _tabs = tabs;
            _map = pages;

            _tabs.SelectedIndexChanged += (_, _) => ApplyLayout();
        }

        public void ApplyLayout()
        {
            if (_tabs.SelectedTab is null)
                return;

            if (!_map.TryGetValue(_tabs.SelectedTab.Name, out var info))
                return;

            info.OnSelected?.Invoke();

            // Measure content size
            int contentHeight = CalculateContentHeight(_tabs.SelectedTab);
            int contentWidth = CalculateContentWidth(_tabs.SelectedTab);

            // Form chrome
            int chromeHeight = _form.Height - _form.ClientSize.Height;
            int chromeWidth = _form.Width - _form.ClientSize.Width;

            // Tab header height
            int tabHeader = _tabs.Height - _tabs.DisplayRectangle.Height;

            // Final size
            int targetHeight =
                contentHeight
                + chromeHeight
                + tabHeader
                + info.ExtraHeight;

            int targetWidth =
                contentWidth
                + chromeWidth
                + info.ExtraWidth;   // Add this to your map entries

            // Lock size
            _form.MaximumSize = _form.MinimumSize = new Size(targetWidth, targetHeight);
            _form.Size = new Size(targetWidth, targetHeight);
        }
    }

    public sealed class TabLayoutInfo
    {
        public Control Panel { get; set; }
        public int ExtraHeight { get; set; }
        public int ExtraWidth { get; set; }   // ← Add this
        public Action OnSelected { get; set; }
    }
}