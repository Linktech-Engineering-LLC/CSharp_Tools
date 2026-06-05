/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/UserControls/UcPasswordEditor.cs
 * File: UcPasswordEditor.cs
 * Version: 1.0.4
 * Created: 2026-05-11
 * Modified: 2026-06-05
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
#region System Libraries
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion
#region Project Libraries
using Tools.Config;
using Tools.Converters;
using Tools.Enums;
using ToolsUI.Config;
using ToolsUI.Helpers;
#endregion

namespace ToolsUI.UserControls
{
    public partial class UcPasswordEditor : EditorBase
    {
        #region Private Fields
        private PasswordTarget _currentTarget;
        private string? _currentConnectionId;
        private DbConnection? _currentConnection; // only relevant for Database target
        private PasswordMetadata? _initialData;
        private PasswordMetadata? LoadedMetadata;
        private bool _isVaultMode;
        #endregion
        #region Constructors/Destructors
        public UcPasswordEditor()
        {
            InitializeComponent();
            WireEvents();
        }
        #endregion
        #region Public Properties/Methods
        public SettingsModel CurrentSettings { get; set; }
        public override Size GetPreferredSize(Size proposedSize)
        {
            return pnlRoot.Size;
        }
        public PasswordTarget SelectedTarget {  get; set; }
        public void Initialize(PasswordTarget target, string? connectionId = null)
        {
            _currentTarget = target;

            if (connectionId != null)
            {
                _currentConnectionId = connectionId;
                _currentConnection = CurrentSettings.Database.Connections.FirstOrDefault(c => c.ConnectionId == connectionId);
            }
            // This triggers the Target handler, which will populate the context list
            cboTarget.SelectedItem = target;

            // DO NOT set cboContext.SelectedItem or SelectedValue here

            LoadMetadata(GetMetadata(_currentTarget, _currentConnectionId));
        }
        public void LoadMetadata(PasswordMetadata meta)
        {
            // Backward compatibility: auto-populate EncryptionKeyName if missing
            if (meta.Representation == PasswordRepresentation.Encrypted &&
                string.IsNullOrEmpty(meta.EncryptionKeyName))
            {
                meta.EncryptionKeyName = meta.Key + ".enc";
            }
            _initialData = meta;
            LoadedMetadata = meta;

            // 4. Clear password fields (always blank on load)
            txtCurrent.Text = "";
            txtNew.Text = "";
            txtConfirm.Text = "";

            // 5. Populate context list (only matters for Database target)
            PopulateContextList();

            // 6. Restore context selection (using connection ID)
            if (_currentTarget == PasswordTarget.Database && _currentConnectionId != null)
                cboContext.SelectedValue = _currentConnectionId;

            // 7. Update buttons
            bool hasPassword = !string.IsNullOrEmpty(meta.Password);
            btnRemove.Enabled = hasPassword;
            btnUpdate.Text = hasPassword ? "Update" : "Add";
            txtCurrent.Enabled = hasPassword;
        }
        #endregion
        #region Private Helpers
        private void ApplyRepresentationLogic(
            PasswordMetadata meta,
            PasswordMetadata existing,
            string plaintext)
        {
            if (_currentTarget == PasswordTarget.Database)
            {
                string vaultKey = $"{Application.ProductName}.{_currentConnection.Engine}.{_currentConnection.ConnectionId}";
                Vault.Write(vaultKey, plaintext);
                meta.Password = vaultKey;
                return;
            }

            // Non-database: always encrypted inline
            string baseKey = $"{Application.ProductName}.{meta.Key}";
            meta.EncryptionKeyName = baseKey + ".enc";

            string encryptionKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            Vault.Write(meta.EncryptionKeyName, encryptionKey);

            meta.Password = MySqlCrypto.Encrypt(plaintext, encryptionKey);
        }
        private PasswordMetadata BuildMetadataFromUI(PasswordMetadata existing)
        {
            var meta = new PasswordMetadata();

            switch (_currentTarget)
            {
                case PasswordTarget.Database:
                    meta.Key = _currentConnectionId;
                    meta.Location = PasswordLocation.Vault;
                    meta.Representation = PasswordRepresentation.Secret;
                    break;

                default:
                    meta.Key = existing.Key; // App, Config, etc.
                    meta.Location = PasswordLocation.Inline;
                    meta.Representation = PasswordRepresentation.Encrypted;
                    break;
            }

            return meta;
        }
        private PasswordMetadata GetMetadata(PasswordTarget target, string? connectionId)
        {
            switch (target)
            {
                case PasswordTarget.Application:
                    return CurrentSettings.Passwords
                        .FirstOrDefault(p => p.Key == "App")
                        ?? new PasswordMetadata { Key = "App" };

                case PasswordTarget.Configuration:
                    return CurrentSettings.Passwords
                        .FirstOrDefault(p => p.Key == "Config")
                        ?? new PasswordMetadata { Key = "Config" };

                case PasswordTarget.Diagnostics:
                    return CurrentSettings.Passwords
                        .FirstOrDefault(p => p.Key == "Diag")
                        ?? new PasswordMetadata { Key = "Diag" };

                case PasswordTarget.Archiving:
                    return CurrentSettings.Passwords
                        .FirstOrDefault(p => p.Key == "Arch")
                        ?? new PasswordMetadata { Key = "Arch" };

                case PasswordTarget.Historical:
                    return CurrentSettings.Passwords
                        .FirstOrDefault(p => p.Key == "Hist")
                        ?? new PasswordMetadata { Key = "Hist" };

                case PasswordTarget.Database:
                    if (string.IsNullOrWhiteSpace(connectionId))
                        return new PasswordMetadata { Key = "Db" };

                    DbConnection? conn = CurrentSettings.Database.Connections
                        .FirstOrDefault(c => c.ConnectionId == connectionId);

                    if (conn == null)
                        return new PasswordMetadata { Key = $"Db:{connectionId}" };

                    // DB passwords are stored directly on the connection
                    return conn.Password ?? new PasswordMetadata { Key = $"Db:{connectionId}" };

                default:
                    // Should never happen, but safe fallback
                    return new PasswordMetadata { Key = "Unknown" };
            }
        }
        private void PopulateContextList()
        {
            // Always visible, but only enabled for Database target
            pnlDatabase.Enabled = (SelectedTarget == PasswordTarget.Database);

            if (!pnlDatabase.Enabled)
            {
                cboContext.DataSource = null;
                txtID.Text = string.Empty;
                return;
            }

            // Pull from your loaded settings
            var connections = CurrentSettings.Database.Connections
                .Select(c => new { c.ConnectionId, Display = $"{c.ConnectionId} ({c.Engine})" })
                .ToList();

            cboContext.DataSource = connections;
            cboContext.DisplayMember = "Display";
            cboContext.ValueMember = "ConnectionId";
            txtID.Text = _currentConnectionId ?? "";
        }
        private void RefreshRepresentationOptions()
        {
            // Adjust representation dropdown based on vault mode
        }
        private void RemoveMetadata(PasswordTarget target, string? connectionId)
        {
            PasswordMetadata existing = GetMetadata(target, connectionId);

            // Cleanup vault entries
            if (existing.Location == PasswordLocation.Vault)
            {
                Vault.Delete(existing.Password);
            }

            // Cleanup encrypted inline keys
            if (existing.Representation == PasswordRepresentation.Encrypted &&
                !string.IsNullOrEmpty(existing.EncryptionKeyName))
            {
                Vault.Delete(existing.EncryptionKeyName);
            }

            // Now remove metadata normally
            switch (target)
            {
                case PasswordTarget.Application:
                    CurrentSettings.Passwords.RemoveAll(p => p.Key == "App");
                    break;

                case PasswordTarget.Configuration:
                    CurrentSettings.Passwords.RemoveAll(p => p.Key == "Config");
                    break;

                case PasswordTarget.Database:
                    var conn = CurrentSettings.Database.Connections
                        .FirstOrDefault(c => c.ConnectionId == connectionId);

                    if (conn != null)
                        conn.Password = null;

                    break;

                    // Add Diag/Arch/Hist if needed
            }
        }
        private void SaveMetadata(PasswordMetadata meta, PasswordTarget target, string? connectionId)
        {
            switch (target)
            {
                case PasswordTarget.Application:
                    CurrentSettings.Passwords.RemoveAll(p => p.Key == "App");
                    CurrentSettings.Passwords.Add(meta);
                    break;

                case PasswordTarget.Configuration:
                    CurrentSettings.Passwords.RemoveAll(p => p.Key == "Config");
                    CurrentSettings.Passwords.Add(meta);
                    break;

                case PasswordTarget.Diagnostics:
                    CurrentSettings.Passwords.RemoveAll(p => p.Key == "Diag");
                    CurrentSettings.Passwords.Add(meta);
                    break;

                case PasswordTarget.Archiving:
                    CurrentSettings.Passwords.RemoveAll(p => p.Key == "Arch");
                    CurrentSettings.Passwords.Add(meta);
                    break;

                case PasswordTarget.Historical:
                    CurrentSettings.Passwords.RemoveAll(p => p.Key == "Hist");
                    CurrentSettings.Passwords.Add(meta);
                    break;

                case PasswordTarget.Database:
                    var conn = CurrentSettings.Database.Connections
                        .FirstOrDefault(c => c.ConnectionId == connectionId);

                    if (conn != null)
                        conn.Password = meta;

                    break;
            }
        }
        private bool VerifyPassword(PasswordMetadata meta)
        {
            string entered = txtCurrent.Text; // user-entered plaintext

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
                            // Encryption key name should be stored in metadata
                            string? encryptionKey = Vault.Read(meta.EncryptionKeyName);
                            if (encryptionKey == null)
                                return false;

                            string decrypted = MySqlCrypto.Decrypt(meta.Password, encryptionKey);
                            return decrypted == entered;
                    }
                    break;

                case PasswordLocation.Vault:
                    string? stored = Vault.Read(meta.Password);
                    return stored == entered;
            }

            return false;
        }
        private void WireEvents()
        {
            cboTarget.DataSource = Enum.GetValues(typeof(PasswordTarget));
            cboTarget.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            cboContext.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

            btnCurrent.Click += Button_Click;
            btnNew.Click += Button_Click;
            btnConfirm.Click += Button_Click;

            btnUpdate.Click += Button_Click;
            btnRemove.Click += Button_Click;

            txtCurrent.Leave += TextBox_Leave;
            txtConfirm.Leave += TextBox_Leave;
        }

        #endregion
        #region Private Event Handlers
        private void Button_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is string Tags)
            {
                switch (Tags)
                {
                    case "ShowConfirm":
                        UIHelperService.Instance.TogglePassword(txtConfirm, btn);
                        break;
                    case "ShowCurrent":
                        UIHelperService.Instance.TogglePassword(txtCurrent, btn);
                        break;
                    case "ShowNew":
                        UIHelperService.Instance.TogglePassword(txtNew, btn);
                        break;
                    case "Update":
                        // Load existing metadata (App/Config or DB)
                        PasswordMetadata existing = GetMetadata(_currentTarget, _currentConnectionId);

                        // Build updated metadata using vault/inline rules
                        PasswordMetadata updated = BuildMetadataFromUI(existing);

                        // Apply encryption / hashing / vault logic
                        string plaintext = txtNew.Text.Length > 0 ? txtNew.Text : txtCurrent.Text;
                        ApplyRepresentationLogic(updated, existing, plaintext);

                        // Save to correct location in SettingsModel
                        SaveMetadata(updated, _currentTarget, _currentConnectionId);
                        // Reload editor into a clean state BEFORE the tree refresh
                        LoadMetadata(GetMetadata(_currentTarget, _currentConnectionId));

                        // Refresh UI
                        RequestRefresh();
                        break;
                    case "Remove":
                        RemoveMetadata(_currentTarget, _currentConnectionId);

                        // Reload editor into a clean state BEFORE the tree refresh
                        LoadMetadata(GetMetadata(_currentTarget, _currentConnectionId));

                        RequestRefresh();
                        break;
                }
            }
        }
        private void ComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (sender is ComboBox cbo && cbo.Tag is string Tags)
            {
                switch (Tags)
                {
                    case "Target":
                        if (cboTarget.SelectedItem is PasswordTarget target)
                        {
                            _currentTarget = target;
                            SelectedTarget = target;

                            // Populate context list for DB target
                            PopulateContextList();

                            if (target == PasswordTarget.Database)
                            {
                                cboContext.Enabled = true;

                                // If nothing selected, pick first available
                                if (cboContext.SelectedIndex < 0 && cboContext.Items.Count > 0)
                                    cboContext.SelectedIndex = 0;

                                // Correct: use SelectedValue, not SelectedItem
                                _currentConnectionId = cboContext.SelectedValue?.ToString();
                            }
                            else
                            {
                                cboContext.Enabled = false;
                                cboContext.SelectedIndex = -1;
                                _currentConnectionId = null;
                            }

                            // Load metadata for the selected target + connection
                            LoadMetadata(GetMetadata(_currentTarget, _currentConnectionId));
                        }
                        break;

                    case "Context":
                        if (!cboContext.Enabled)
                            return;

                        // Correct: use SelectedValue
                        _currentConnectionId = cboContext.SelectedValue?.ToString();

                        LoadMetadata(GetMetadata(_currentTarget, _currentConnectionId));
                        break;

                    case "Representation":
                        // Future enhancement
                        break;
                }
            }
        }
        private void ChkVault_CheckedChanged(object? sender, EventArgs e)
        {
            // Vault mode may override representation options
            RefreshRepresentationOptions();
        }
        private void TextBox_Leave(object? sender, EventArgs e)
        {
            if (sender is TextBox txt && txt.Tag is string Tags)
            {
                switch (Tags)
                {
                    case "Current":
                        if(!txt.Enabled)
                            return;
                        // Optional: verify current password on leave
                        if (LoadedMetadata != null && !VerifyPassword(LoadedMetadata))
                        {
                            MessageBox.Show("Current password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtCurrent.Focus();
                        }
                        break;
                    case "Confirm":
                        if (txtNew.Text != txtConfirm.Text)
                        {
                            MessageBox.Show("New password and confirmation do not match.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtConfirm.Focus();
                        }
                        break;
                }
            }
        }
        #endregion
    }
}
