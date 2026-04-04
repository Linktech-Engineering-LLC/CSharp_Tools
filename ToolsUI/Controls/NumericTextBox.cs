/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Controls/NumericTextBox.cs
 * File: NumericTextBox.cs
 * Created: None
 * Modified: 2026-04-02
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ToolsUI.Controls
{
    public class NumericTextBox : TextBox
    {
        private bool IsDesignMode =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
            DesignMode;
        private bool _allowDecimal = false;
        private bool _allowNegative = false;
        private decimal _minimum = 0;
        private decimal _maximum = 1000000;
        private string _placeholder = "";
        private bool _darkMode = false;
        private bool _borderless = false;

        public NumericTextBox()
        {
            if (!IsDesignMode)
            {
                BorderStyle = BorderStyle.FixedSingle;
            }
            BorderStyle = BorderStyle.FixedSingle;
        }

        // -------------------------
        // Placeholder
        // -------------------------
        [Category("Appearance")]
        public string Placeholder
        {
            get => _placeholder;
            set { _placeholder = value; Invalidate(); }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new string PlaceholderText
        {
            get => base.PlaceholderText;
            set => base.PlaceholderText = value;
        }

        // -------------------------
        // Dark Mode
        // -------------------------
        [Category("Appearance")]
        public bool DarkMode
        {
            get => _darkMode;
            set { _darkMode = value; Invalidate(); }
        }

        // -------------------------
        // Borderless
        // -------------------------
        [Category("Appearance")]
        public bool Borderless
        {
            get => _borderless;
            set { _borderless = value; Invalidate(); }
        }

        // -------------------------
        // Numeric Options
        // -------------------------
        [Category("Behavior")]
        public bool AllowDecimal
        {
            get => _allowDecimal;
            set => _allowDecimal = value;
        }

        [Category("Behavior")]
        public bool AllowNegative
        {
            get => _allowNegative;
            set => _allowNegative = value;
        }

        [Category("Behavior")]
        public decimal Minimum
        {
            get => _minimum;
            set => _minimum = value;
        }

        [Category("Behavior")]
        public decimal Maximum
        {
            get => _maximum;
            set => _maximum = value;
        }

        [Browsable(false)]
        public bool HasValue => decimal.TryParse(Text, out _);

        // -------------------------
        // Numeric Validation
        // -------------------------
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            // Allow control keys
            if (char.IsControl(e.KeyChar))
                return;

            // Digits always allowed
            if (char.IsDigit(e.KeyChar))
                return;

            // Decimal point
            if (_allowDecimal && e.KeyChar == '.' && !Text.Contains("."))
                return;

            // Negative sign
            if (_allowNegative && e.KeyChar == '-' && SelectionStart == 0 && !Text.Contains("-"))
                return;

            // Otherwise block
            e.Handled = true;
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            if (IsDesignMode)
            {
                base.OnValidating(e);
                return;
            }

            base.OnValidating(e);

            if (decimal.TryParse(Text, out decimal val))
            {
                if (val < _minimum || val > _maximum)
                    e.Cancel = true;
            }
            else if (Text.Length > 0)
            {
                e.Cancel = true;
            }
        }
        // -------------------------
        // Strongly-typed numeric value
        // -------------------------
        [Browsable(false)]
        public decimal Value
        {
            get
            {
                if (decimal.TryParse(Text, out var v))
                    return v;

                return 0;
            }
            set
            {
                decimal v = Math.Clamp(value, _minimum, _maximum);
                Text = v.ToString();
                OnValueChanged(EventArgs.Empty);
            }
        }

        [Browsable(false)]
        public int IntValue
        {
            get => (int)Value;
            set => Value = value;
        }

        // -------------------------
        // ValueChanged event
        // -------------------------
        public event EventHandler ValueChanged;

        protected virtual void OnValueChanged(EventArgs e)
        {
            ValueChanged?.Invoke(this, e);
        }

        // Fire ValueChanged when text changes to a valid number
        protected override void OnTextChanged(EventArgs e)
        {
            if (IsDesignMode)
            {
                base.OnTextChanged(e);
                return;
            }

            base.OnTextChanged(e);

            if (decimal.TryParse(Text, out var v))
            {
                if (v < _minimum) v = _minimum;
                if (v > _maximum) v = _maximum;

                OnValueChanged(EventArgs.Empty);
            }
        }

        // -------------------------
        // Custom Painting
        // -------------------------
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (IsDesignMode)
                return;

            const int WM_PAINT = 0x000F;

            if (m.Msg == WM_PAINT)
            {
                using (Graphics g = Graphics.FromHwnd(Handle))
                {
                    if (string.IsNullOrEmpty(Text) && !Focused && !string.IsNullOrEmpty(_placeholder))
                    {
                        TextRenderer.DrawText(
                            g,
                            _placeholder,
                            Font,
                            ClientRectangle,
                            Color.Gray,
                            TextFormatFlags.VerticalCenter | TextFormatFlags.Left
                        );
                    }

                    if (_borderless)
                    {
                        using Pen p = new Pen(_darkMode ? Color.DimGray : Color.Gray);
                        g.DrawRectangle(p, 0, 0, Width - 1, Height - 1);
                    }
                }
            }
        }
    }
}
