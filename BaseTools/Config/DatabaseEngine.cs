/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Config/DatabaseEngine.cs
 * File: DatabaseEngine.cs
 * Created: 2026-03-31
 * Modified: 2026-04-01
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

}
