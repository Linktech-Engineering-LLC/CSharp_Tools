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
 * Version: 1.0.1
 * Created: 2026-05-11
 * Modified: 2026-05-13
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion
#region Project Libraries
using Tools.Config;
using Tools.Converters;
using Tools.Enums;
using ToolsUI.Config;
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
        private string? _vaultKey;
        private string? _recoveredPassword;
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
            _initialData = meta;
            LoadedMetadata = meta;

            // 1. Load metadata key (editable)
            txtKey.Text = meta.Key ?? "";

            // 2. Load representation
            cboRepresentation.SelectedItem = meta.Representation;

            // 3. Vault mode handling
            if (meta.Location == PasswordLocation.Vault)
            {
                chkVault.Checked = true;
                _isVaultMode = true;

                // meta.Password IS the vault key
                _vaultKey = meta.Password;

                var pw = Vault.Read(_vaultKey);
                txtCurrent.Text = pw ?? string.Empty;
                _recoveredPassword = pw;
            }
            else
            {
                chkVault.Checked = false;
                _isVaultMode = false;

                // Inline password
                txtCurrent.Text = meta.Password;
            }

            // 4. Populate context list (only matters for Database target)
            PopulateContextList();

            // 5. Restore context selection (using connection ID, NOT password key)
            if (_currentTarget == PasswordTarget.Database && _currentConnectionId != null)
            {
                cboContext.SelectedValue = _currentConnectionId;
            }
        }
        #endregion
        #region Private Helpers
        private void ApplyRepresentationLogic(PasswordMetadata meta, PasswordTarget target, string plaintext)
        {
            switch (meta.Location)
            {
                case PasswordLocation.Vault:
                    SaveVaultPassword(target, plaintext, meta);
                    break;

                case PasswordLocation.Inline:
                    switch (meta.Representation)
                    {
                        case PasswordRepresentation.Plaintext:
                            SaveInlinePlaintext(plaintext, meta);
                            break;

                        case PasswordRepresentation.Hashed:
                            SaveInlineHashed(plaintext, meta);
                            break;

                        case PasswordRepresentation.Encrypted:
                            SaveEncryptedInlinePassword(target, plaintext, meta);
                            break;
                    }
                    break;
            }
        }
        private PasswordMetadata BuildMetadataFromUI(PasswordMetadata existing)
        {
            // Determine final password value
            string newPassword =
                !string.IsNullOrEmpty(txtNew.Text)
                    ? txtNew.Text
                    : txtCurrent.Text;

            // Editable metadata key
            string newMetaKey = txtKey.Text;

            var meta = new PasswordMetadata
            {
                Key = newMetaKey
            };

            if (chkVault.Checked)
            {
                // Determine vault key
                string vaultKey;

                if (existing.Location == PasswordLocation.Vault)
                {
                    // Reuse existing vault key
                    vaultKey = existing.Password;
                }
                else
                {
                    // Generate vault key based on target
                    vaultKey = _currentTarget == PasswordTarget.Database
                        ? GenerateDbVaultKey(_currentConnection)
                        : GenerateVaultKey(_currentTarget);
                }

                // --- ORPHAN CLEANUP (correct placement) ---
                // If the old metadata was vault-based and the vault key changed,
                // delete the old vault entry.
                if (existing.Location == PasswordLocation.Vault &&
                    existing.Password != vaultKey)
                {
                    Vault.Delete(existing.Password);
                }

                // Write password into vault if changed
                if (Vault.Read(vaultKey) != newPassword)
                    Vault.Write(vaultKey, newPassword);

                meta.Location = PasswordLocation.Vault;
                meta.Representation = PasswordRepresentation.Secret;
                meta.Password = vaultKey; // store vault key, not password
            }
            else
            {
                // Inline mode
                meta.Location = PasswordLocation.Inline;
                meta.Representation = (PasswordRepresentation)cboRepresentation.SelectedItem!;
                meta.Password = newPassword;
            }

            return meta;
        }
        private string GenerateDbVaultKey(DbConnection conn) => $"{Application.ProductName}.{conn.Engine}";
        private string GenerateVaultKey(PasswordTarget target) => target switch
        {
            PasswordTarget.Application => $"{Application.ProductName}.app",
            PasswordTarget.Configuration => $"{Application.ProductName}.config",
            PasswordTarget.Diagnostics => $"{Application.ProductName}.diag",
            PasswordTarget.Archiving => $"{Application.ProductName}.arch",
            PasswordTarget.Historical => $"{Application.ProductName}.hist",
            _ => $"{Application.ProductName}.password"
        };
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
                    return new PasswordMetadata();
            }
        }
        private void PopulateContextList()
        {
            // Always visible, but only enabled for Database target
            cboContext.Enabled = (SelectedTarget == PasswordTarget.Database);

            if (!cboContext.Enabled)
            {
                cboContext.DataSource = null;
                return;
            }

            // Pull from your loaded settings
            var connections = CurrentSettings.Database.Connections
                .Select(c => new { c.ConnectionId, Display = $"{c.ConnectionId} ({c.Engine})" })
                .ToList();

            cboContext.DataSource = connections;
            cboContext.DisplayMember = "Display";
            cboContext.ValueMember = "ConnectionId";
        }
        private void RefreshRepresentationOptions()
        {
            // Adjust representation dropdown based on vault mode
        }
        private void RemoveMetadata(PasswordTarget target, string? connectionId)
        {
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
            }
        }
        private void SaveEncryptedInlinePassword(PasswordTarget target, string plaintextPassword, PasswordMetadata meta)
        {
            // 1. Derive vault key name
            string encKeyName = GenerateVaultKey(target) + ".enc";

            // 2. Generate new encryption key
            string encryptionKey = Crypto.GenerateKeyString();

            // 3. Store encryption key in vault
            Vault.Write(encKeyName, encryptionKey);

            // 4. Encrypt password
            string encryptedBlob = MySqlCrypto.Encrypt(plaintextPassword, encryptionKey);

            // 5. Update metadata
            meta.Password = encryptedBlob;
            meta.Location = PasswordLocation.Inline;
            meta.Representation = PasswordRepresentation.Encrypted;
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

                case PasswordTarget.Database:
                    var conn = CurrentSettings.Database.Connections
                        .FirstOrDefault(c => c.ConnectionId == connectionId);

                    if (conn != null)
                        conn.Password = meta;

                    break;
            }
        }
        private void SaveInlineHashed(string plaintext, PasswordMetadata meta)
        {
            // Whatever hashing function you already use
            string hash = Crypto.HashPassword(plaintext);

            meta.Password = hash;
            meta.Location = PasswordLocation.Inline;
            meta.Representation = PasswordRepresentation.Hashed;
        }
        private void SaveInlinePlaintext(string plaintext, PasswordMetadata meta)
        {
            meta.Password = plaintext;
            meta.Location = PasswordLocation.Inline;
            meta.Representation = PasswordRepresentation.Plaintext;
        }
        private void SaveVaultPassword(PasswordTarget target, string plaintext, PasswordMetadata meta)
        {
            // 1. Determine vault key name
            string vaultKeyName = GenerateVaultKey(target);

            // 2. Write the real password to the vault
            Vault.Write(vaultKeyName, plaintext);

            // 3. Update metadata to reference vault mode
            meta.Password = vaultKeyName;
            meta.Location = PasswordLocation.Vault;
            meta.Representation = PasswordRepresentation.Plaintext;
        }
        private void TogglePassword(TextBox txt, Button btn)
        {
            bool isShowing = txt.PasswordChar == '\0';

            // Toggle the mask
            txt.PasswordChar = isShowing ? '•' : '\0';

            // Update the button label
            btn.Text = isShowing ? "Show" : "Hide";
        }
        private bool ValidateNewPassword()
        {
            if (txtNew.Text != txtConfirm.Text)
            {
                MessageBox.Show("New and Confirm passwords do not match.");
                return false;
            }

            // Add more validation rules here

            return true;
        }
        private void WireEvents()
        {
            cboRepresentation.DataSource = Enum.GetValues(typeof(PasswordRepresentation));
            cboTarget.DataSource = Enum.GetValues(typeof(PasswordTarget));
            cboTarget.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            cboContext.SelectedIndexChanged += ComboBox_SelectedIndexChanged;

            chkVault.CheckedChanged += ChkVault_CheckedChanged;
            cboRepresentation.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            btnCurrent.Click += Button_Click;
            btnNew.Click += Button_Click;
            btnConfirm.Click += Button_Click;

            btnUpdate.Click += Button_Click;
            btnCancel.Click += Button_Click;
            btnRemove.Click += Button_Click;
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
                        TogglePassword(txtConfirm, btn);
                        break;
                    case "ShowCurrent":
                        TogglePassword(txtCurrent, btn);
                        break;
                    case "ShowNew":
                        TogglePassword(txtNew, btn);
                        break;
                    case "Update":
                        if (!ValidateNewPassword())
                            return;

                        // Load existing metadata (App/Config or DB)
                        PasswordMetadata existing = GetMetadata(_currentTarget, _currentConnectionId);

                        // Build updated metadata using vault/inline rules
                        PasswordMetadata updated = BuildMetadataFromUI(existing);

                        // NEW: Apply encryption / hashing / vault logic
                        string plaintext = txtNew.Text.Length > 0 ? txtNew.Text : txtCurrent.Text;
                        ApplyRepresentationLogic(updated, _currentTarget, plaintext);

                        // Save to correct location in SettingsModel
                        SaveMetadata(updated, _currentTarget, _currentConnectionId);

                        // Refresh UI
                        LoadMetadata(GetMetadata(_currentTarget, _currentConnectionId));
                        RequestClose();
                        break;
                    case "Cancel":
                        LoadMetadata(GetMetadata(_currentTarget, _currentConnectionId)); // reload from settings
                        RequestClose();
                        break;
                    case "Remove":
                        RemoveMetadata(_currentTarget, _currentConnectionId);
                        LoadMetadata(GetMetadata(_currentTarget, _currentConnectionId));
                        RequestClose();
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
        #endregion
    }
}
