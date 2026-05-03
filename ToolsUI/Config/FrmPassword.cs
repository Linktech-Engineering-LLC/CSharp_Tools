/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/Config/FrmPassword.cs
 * File: FrmPassword.cs
 * Version: 1.0.0
 * Created: 2026-04-04
 * Modified: 2026-04-30
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

#endregion
#region Project Libraries
using Tools.Enums;
#endregion

namespace ToolsUI.Config
{
    public partial class FrmPassword : Form
    {
        #region Private Fields
        private readonly PasswordTarget _target;
        private readonly PasswordMetadata _initialData;
        #endregion
        #region Constructors/Destructors
        public FrmPassword(PasswordTarget target, PasswordMetadata initial)
        {
            InitializeComponent();
            InitializeControls();

            _target = target;
            _initialData = initial ?? new PasswordMetadata();

            LoadInitialMetadata();
        }
        #endregion
        #region Public Properties
        public PasswordDialogResult PasswordData { get; private set; }
        #endregion
        #region Private Properties
        private bool _isVaultMode = false;
        private string? _vaultKey = null;
        private string? _recoveredPassword = null;
        private string? _receivedPassword = null;
        #endregion
        #region Private Helpers
        private string GenerateVaultKey(PasswordTarget target)
        {
            return target switch
            {
                PasswordTarget.Database => $"{Application.ProductName}.dbengine",
                PasswordTarget.Application => $"{Application.ProductName}.app",
                PasswordTarget.Configuration => $"{Application.ProductName}.config",
                _ => $"{Application.ProductName}.password"
            };
        }
        private void InitializeControls()
        {
            cboRepresentation.DataSource = Enum.GetValues(typeof(PasswordRepresentation));
            btnCancel.Click += ButtonClicked;
            btnConfirm.Click += ButtonClicked;
            btnCurrent.Click += ButtonClicked;
            btnNew.Click += ButtonClicked;
            btnOk.Click += ButtonClicked;
            cbxVault.CheckedChanged += CheckBox_CheckedChanged;
        }
        private void LoadInitialMetadata()
        {
            // Load representation
            cboRepresentation.SelectedItem = _initialData.Representation;

            // Load password or vault key
            _receivedPassword = _initialData.Password;

            // Detect vault mode
            if (_initialData.Location == PasswordLocation.Vault)
            {
                cbxVault.Checked = true;
                _isVaultMode = true;

                // The received password is actually the vault key
                _vaultKey = _initialData.Password;

                // Attempt to read the real password
                var pw = Vault.Read(_vaultKey);
                if (pw != null)
                {
                    _recoveredPassword = pw;
                    txtCurrent.Text = pw;
                }
                else
                {
                    txtCurrent.Text = string.Empty;
                }
            }
            else if (LooksLikeVaultKey(_initialData.Password))
            {
                // Heuristic: if it looks like a vault key, treat it as such
                cbxVault.Checked = true;
                _isVaultMode = true;
                _vaultKey = _initialData.Password;
                var pw = Vault.Read(_vaultKey);
                if (pw != null)
                {
                    _recoveredPassword = pw;
                    txtCurrent.Text = pw;
                }
                else
                {
                    txtCurrent.Text = string.Empty;
                }
            }
            else
            {
                cbxVault.Checked = false;
                _isVaultMode = false;

                // Plaintext/encrypted/hashed password
                txtCurrent.Text = _initialData.Password;
            }
        }
        private bool LooksLikeVaultKey(string s)
        {
            return s.Contains('.') && !s.Any(char.IsWhiteSpace);
        }

        private void TogglePassword(TextBox txt, Button btn)
        {
            bool isShowing = txt.PasswordChar == '\0';

            // Toggle the mask
            txt.PasswordChar = isShowing ? '•' : '\0';

            // Update the button label
            btn.Text = isShowing ? "Show" : "Hide";
        }
        #endregion
        #region Private Event Functions
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            cboRepresentation.Enabled = !cbxVault.Checked;

            if (cbxVault.Checked)
            {
                // Switching INTO vault mode
                _isVaultMode = true;

                // If we already have a vault key, load the real password
                if (!string.IsNullOrEmpty(_vaultKey))
                {
                    var pw = Vault.Read(_vaultKey);
                    if (pw != null)
                    {
                        _recoveredPassword = pw;
                        txtCurrent.Text = pw;
                    }
                }
                else
                {
                    // No vault key yet — user is creating a new vault password
                    txtCurrent.Text = _receivedPassword;
                }
            }
            else
            {
                // Switching OUT of vault mode
                _isVaultMode = false;

                // Restore the original password (plaintext/encrypted/etc.)
                txtCurrent.Text = _receivedPassword;
            }
        }
        private void ButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is string Tags)
            {
                switch (Tags)
                {
                    case "Cancel":
                        DialogResult = DialogResult.Cancel;
                        Close();
                        break;
                    case "ConfirmShow":
                        TogglePassword(txtConfirm, btn);
                        break;
                    case "CurrentShow":
                        TogglePassword(txtCurrent, btn);
                        break;
                    case "NewShow":
                        TogglePassword(txtNew, btn);
                        break;
                    case "OK":
                        string newPassword =
                        !string.IsNullOrEmpty(txtNew.Text)
                            ? txtNew.Text
                            : txtCurrent.Text;
                        PasswordRepresentation? selectedRep = (PasswordRepresentation?)cboRepresentation.SelectedItem;

                        PasswordMetadata meta = new();

                        if (_isVaultMode)
                        {
                            // Generate vault key if needed
                            if (string.IsNullOrEmpty(_vaultKey))
                                _vaultKey = GenerateVaultKey(_target);

                            // Write password to vault
                            // Only write if the password actually changed
                            if (_recoveredPassword == null || newPassword != _recoveredPassword)
                            {
                                Vault.Write(_vaultKey, newPassword);
                            }

                            meta.Location = PasswordLocation.Vault;
                            meta.Representation = PasswordRepresentation.Secret;
                            meta.Password = _vaultKey; // store key, not password
                        }
                        else
                        {
                            // Non-vault mode
                            meta.Location = PasswordLocation.Database;
                            meta.Representation = selectedRep ?? PasswordRepresentation.Unknown;
                            meta.Password = newPassword;
                        }

                        PasswordData = new PasswordDialogResult
                        {
                            Accepted = true,
                            Password = newPassword,
                            Metadata = meta
                        };

                        DialogResult = DialogResult.OK;
                        Close();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
