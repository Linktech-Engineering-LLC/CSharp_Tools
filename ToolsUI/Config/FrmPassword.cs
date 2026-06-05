/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Config/FrmPassword.cs
 * File: FrmPassword.cs
 * Version: 1.0.2
 * Created: 2026-04-04
 * Modified: 2026-06-05
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
#region System Libraries
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools.Config;
using Tools.Converters;


#endregion
#region Project Libraries
using Tools.Enums;
using ToolsUI.Helpers;
#endregion

namespace ToolsUI.Config
{
    public partial class FrmPassword : Form
    {
        #region Private Variables
        private readonly PasswordMetadata _meta;
        private int _attempts = 0;
        private const int MaxAttempts = 3;
        #endregion
        #region Constructors/Destructors
        public FrmPassword(PasswordMetadata meta)
        {
            InitializeComponent();
            _meta = meta;
            InitializeControls();
        }
        #endregion
        #region Form Controls
        private void FrmPassword_Load(object sender, EventArgs e)
        {
            txtUser.Text = Environment.UserName;
            lblAttempts.Text = $"Attempt {_attempts + 1} of {MaxAttempts}";
        }
        private void Button_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is string Tags)
            {
                switch(Tags)
                {
                    case "Cancel":
                        DialogResult = DialogResult.Cancel;
                        Close();
                        break;
                    case "OK":
                        if (VerifyPassword(_meta, txtPassword.Text))
                        {
                            DialogResult = DialogResult.OK;
                            Close();
                            return;
                        }

                        _attempts++;
                        if (_attempts >= MaxAttempts)
                        {
                            MessageBox.Show("Too many failed attempts.", "Access Denied",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            DialogResult = DialogResult.Cancel;
                            Close();
                            return;
                        }

                        lblAttempts.Text = $"Attempt {_attempts + 1} of {MaxAttempts}";
                        MessageBox.Show("Incorrect password.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPassword.Clear();
                        txtPassword.Focus();
                        break;
                    case "Show":
                        UIHelperService.Instance.TogglePassword(txtPassword, btnShow);
                        break;
                }
            }
        }
        #endregion
        #region Private Methods
        private void InitializeControls()
        {
            Load += FrmPassword_Load;
            btnOk.Click += Button_Click;
            btnCancel.Click += Button_Click;
            btnShow.Click += Button_Click;
        }
        private bool VerifyPassword(PasswordMetadata meta, string entered)
        {
            switch (meta.Location)
            {
                case PasswordLocation.Inline:
                    switch (meta.Representation)
                    {
                        case PasswordRepresentation.Plaintext:
                            return meta.Password == entered;
                        case PasswordRepresentation.Hashed:
                            return Crypto.HashPassword(entered) == meta.Password;
                        case PasswordRepresentation.Encrypted:
                            string? key = Vault.Read(meta.EncryptionKeyName);
                            if (key == null) return false;
                            string decrypted = MySqlCrypto.Decrypt(meta.Password, key);
                            return decrypted == entered;
                    }
                    break;

                case PasswordLocation.Vault:
                    string? stored = Vault.Read(meta.Password);
                    return stored == entered;
            }

            return false;
        }
        #endregion
    }
}
