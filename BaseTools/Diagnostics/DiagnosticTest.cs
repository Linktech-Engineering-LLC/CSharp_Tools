/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Diagnostics/DiagnosticTest.cs
 * File: DiagnosticTest.cs
 * Created: 2026-04-03
 * Modified: 2026-04-03
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Diagnostics
{
    public enum DiagnosticTest
    {
        ConfigFileExists,
        ConfigFileReadable,
        ConfigFileValidLiteDB,
        RequiredCollections,

        // Paths
        LogsPathExists,
        DataPathExists,
        TempPathExists,
        PathWritable,

        // Passwords
        PasswordStyleValid,
        VaultCredentialExists,
        VaultCredentialReadable,
        EncryptedBlobExists,
        EncryptedBlobDecrypts,
        PlaintextValueExists,

        // Database
        HostValid,
        PortValid,
        UsernameExists,
        PasswordAvailable,
        ServerConnection,
        DatabaseConnection
    }
}
