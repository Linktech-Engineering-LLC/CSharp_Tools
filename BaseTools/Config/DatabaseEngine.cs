/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Config/DatabaseEngine.cs
 * File: DatabaseEngine.cs
 * Version: 1.0.2
 * Created: 2026-03-31
 * Modified: 2026-05-14
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using LiteDB;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Linq;
using System.Text;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace Tools.Config
{
    public enum DatabaseEngine
    {
        MariaDB,
        MySQL,
        PostgreSql,
        SQLServer,
        Oracle,
        SQLite,
        DBLite
    }

    public class DatabaseSettings
    {
        public DatabaseEngine Engine { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Schema { get; set; }
        public string Instance { get; set; }
    }
    public class DbConnection
    {
        [BsonField("Id")]
        public string ConnectionId { get; set; } = string.Empty; // "Primary", "Archive", etc.
        public DatabaseEngine Engine { get; set; }

        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 3306;

        public string User { get; set; } = string.Empty;
        public PasswordMetadata Password { get; set; } = new PasswordMetadata();

        public string Schema { get; set; } = string.Empty;   // MySQL/MariaDB
        public string Instance { get; set; } = string.Empty; // SQL Server/Oracle
         
        // Overrides
        public bool? EnablePooling { get; set; }
        public int? Timeout { get; set; }
        public bool? EncryptConnection { get; set; }
    }
    public class DatabaseConfig
    {
        public bool EnablePooling { get; set; } = true;
        public int DefaultTimeout { get; set; } = 30;
        public bool EncryptConnections { get; set; } = false;
        public List<DbConnection> Connections { get; set; } = [];
    }
    public static class DatabaseEngineExtensions
    {
        public static int GetDefaultPort(this DatabaseEngine engine)
        {
            return engine switch
            {
                DatabaseEngine.MariaDB => 3306,
                DatabaseEngine.MySQL => 3306,
                DatabaseEngine.PostgreSql => 5432,
                DatabaseEngine.SQLServer => 1433,
                DatabaseEngine.Oracle => 1521,
                DatabaseEngine.SQLite => 0,   // no port
                DatabaseEngine.DBLite => 0,   // no port
                _ => 0
            };
        }
    }

}
