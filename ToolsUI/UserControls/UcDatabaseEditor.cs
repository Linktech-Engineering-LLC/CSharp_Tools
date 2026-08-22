/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: ToolsUI
 * Program: ToolsUI.dll
 * Path: Tools/ToolsUI/UserControls/UcDatabaseEditor.cs
 * File: UcDatabaseEditor.cs
 * Version: 1.0.4
 * Created: 2026-05-12
 * Modified: 2026-08-22
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
#region System Libraries
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
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
using ToolsUI.Helpers;
#endregion
namespace ToolsUI.UserControls
{
    public partial class UcDatabaseEditor : EditorBase
    {
        #region Private Fields
        private DbConnection _conn;
        private SettingsModel _settings;
        private PasswordTarget _currentTarget = PasswordTarget.Database;
        private string _currentConnectionId;
        #endregion
        #region Constructors/Destructors
        public UcDatabaseEditor()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion
        #region Public Properties/Methods
        public SettingsModel CurrentSettings { get; set; }
        public override Size GetPreferredSize(Size proposedSize)
        {
            return pnlRoot.Size;
        }
        public void Initialize(DbConnection conn)
        {
            _conn = conn;
            _settings = CurrentSettings;
            _currentTarget = PasswordTarget.Database;
            _currentConnectionId = conn.ConnectionId;

            // 1. Bind enums
            ComboBoxHelper.BindEnum<DatabaseEngine>(cboEngine);

            // 2. Populate fields
            txtID.Text = conn.ConnectionId;
            cboEngine.SelectedItem = conn.Engine;
            txtHost.Text = conn.Host;
            numPort.Value = conn.Port;
            txtHost.Text = conn.Host;
            txtUser.Text = conn.User;
            txtSchema.Text = conn.Schema;
            txtInstance.Text = conn.Instance;

            // Load password metadata
            var meta = conn.Password;

            // Load actual password value
            txtPassword.Text = ResolvePassword(meta);

            // 3. Apply engine-specific rules
            ApplyEngineRules(conn.Engine);

            // 4. Hook events
            btnUpdate.Click += Button_Click;
            btnCancel.Click += Button_Click;
            btnRemove.Click += Button_Click;
            btnShow.Click += Button_Click;
            btnTest.Click += Button_Click;
            cboEngine.SelectedIndexChanged += (s, e) =>
            {
                var engine = (DatabaseEngine)cboEngine.SelectedItem;
                ApplyEngineRules(engine);
            };
        }
        #endregion
        #region Private Helpers
        private void ApplyEngineRules(DatabaseEngine engine)
        {
            // Default port
            numPort.Value = engine.GetDefaultPort();

            // SQLite / DBLite have no host or port
            bool isFileDb = engine is DatabaseEngine.SQLite or DatabaseEngine.DBLite;

            txtHost.Enabled = !isFileDb;
            numPort.Enabled = !isFileDb;

            // SQL Server instance name
            txtInstance.Enabled = engine == DatabaseEngine.SQLServer;
        }
        private string BuildConnectionString(DbConnection conn, string password)
        {
            switch (conn.Engine)
            {
                case DatabaseEngine.MariaDB:
                case DatabaseEngine.MySQL:
                    return BuildMySqlConnectionString(conn, password);

                case DatabaseEngine.PostgreSql:
                    return BuildPostgresConnectionString(conn, password);

                case DatabaseEngine.SQLServer:
                    return BuildSqlServerConnectionString(conn, password);

                case DatabaseEngine.SQLite:
                case DatabaseEngine.DBLite:
                    return BuildSqliteConnectionString(conn);

                default:
                    throw new NotSupportedException($"Engine {conn.Engine} is not supported.");
            }
        }
        private string BuildMySqlConnectionString(DbConnection conn, string password)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = conn.Host,
                Port = (uint)conn.Port,
                UserID = conn.User,
                Password = password,
                Database = conn.Schema,
                SslMode = MySqlSslMode.Preferred
            };

            return builder.ConnectionString;
        }
        private string BuildPostgresConnectionString(DbConnection conn, string password)
        {
            return $"Host={conn.Host};Port={conn.Port};Username={conn.User};Password={password};Database={conn.Schema}";
        }
        private string BuildSqliteConnectionString(DbConnection conn)
        {
            return $"Data Source={conn.Schema};";
        }
        private string BuildSqlServerConnectionString(DbConnection conn, string password)
        {
            var instance = string.IsNullOrWhiteSpace(conn.Instance)
                ? conn.Host
                : $"{conn.Host}\\{conn.Instance}";

            return $"Server={instance};Database={conn.Schema};User Id={conn.User};Password={password};TrustServerCertificate=True;";
        }
        private void EditPassword()
        {
            var editor = new UcPasswordEditor
            {
                CurrentSettings = CurrentSettings
            };

            editor.Initialize(PasswordTarget.Database, _conn.ConnectionId);

            editor.RefreshRequested += (s, e) =>
            {
                // Reload resolved password
                var meta = _conn.Password;
                txtPassword.Text = ResolvePassword(meta);
                RequestRefresh();
            };

            using (var frm = new Form())
            {
                frm.Text = "Edit Password";
                frm.Width = 600;
                frm.Height = 400;
                editor.Dock = DockStyle.Fill;
                frm.Controls.Add(editor);
                frm.StartPosition = FormStartPosition.CenterParent;   // ⭐ THIS LINE 
                frm.ShowDialog();
            }
        }
        private void Initialize()
        {
            txtPassword.ReadOnly = true;
            txtPassword.Click += (s, e) => EditPassword();
        }
        private void ReloadMetadata()
        {
            // Reload the connection fields from the authoritative DbConnection object
            txtID.Text = _conn.ConnectionId;
            cboEngine.SelectedItem = _conn.Engine;
            txtHost.Text = _conn.Host;
            numPort.Value = _conn.Port;
            txtUser.Text = _conn.User;
            txtSchema.Text = _conn.Schema;
            txtInstance.Text = _conn.Instance;

            // Reload the resolved password
            txtPassword.Text = ResolvePassword(_conn.Password);
        }
        private void RemoveConnection()
        {
            _settings.Database.Connections.Remove(_conn);
        }
        private string ResolvePassword(PasswordMetadata meta)
        {
            switch (meta.Location)
            {
                case PasswordLocation.Vault:
                    // meta.Password is the vault key
                    return Vault.Read(meta.Password) ?? string.Empty;

                case PasswordLocation.Inline:
                    return ResolveInlinePassword(meta);

                case PasswordLocation.Unknown:
                default:
                    // Treat as inline fallback
                    return ResolveInlinePassword(meta);
            }
        }
        private string ResolveInlinePassword(PasswordMetadata meta)
        {
            switch (meta.Representation)
            {
                case PasswordRepresentation.Plaintext:
                    return meta.Password ?? string.Empty;

                case PasswordRepresentation.Encrypted:
                    string encKeyName = $"{Application.ProductName}.{_conn.Engine}.enc";
                    string encryptionKey = Vault.Read(encKeyName);
                    return MySqlCrypto.Decrypt(meta.Password, encryptionKey);

                case PasswordRepresentation.Hashed:
                    // Cannot recover a hashed password
                    return string.Empty;

                default:
                    return meta.Password ?? string.Empty;
            }
        }
        private void SaveConnection()
        {
            _conn.Engine = (DatabaseEngine)cboEngine.SelectedItem;
            _conn.Host = txtHost.Text;
            _conn.Port = (int)numPort.Value;
            _conn.User = txtUser.Text;
            _conn.Schema = txtSchema.Text;
            _conn.Instance = txtInstance.Text;
        }
        private void TogglePassword(TextBox txt, Button btn)
        {
            bool isShowing = txt.PasswordChar == '\0';

            // Toggle the mask
            txt.PasswordChar = isShowing ? '•' : '\0';

            // Update the button label
            btn.Text = isShowing ? "Show" : "Hide";
        }
        private void UpdateConnection()
        {
            SaveConnection();
            RequestClose();
        }
        #endregion
        #region Event Handlers
        private void Button_Click(object? sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is string Tags)
            {
                switch (Tags)
                {
                    case "Cancel":
                        ReloadMetadata(); // restores original values
                        RequestClose();
                        break;
                    case "Remove":
                        RemoveConnection();
                        RequestRefresh();
                        break;
                    case "Show":
                        TogglePassword(txtPassword, btn);
                        break;
                    case "Test":
                        try
                        {
                            string password = txtPassword.Text; // already decrypted or vault-resolved
                            string conn = BuildConnectionString(_conn, password);

                            using MySqlConnection db = new MySqlConnection(conn);
                            db.Open();

                            MessageBox.Show("Connection successful.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Connection failed:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case "Update":
                        SaveConnection();   // update host, port, engine, schema, etc.
                        RequestRefresh();
                        break;

                }
            }
        }
        #endregion
    }
}
