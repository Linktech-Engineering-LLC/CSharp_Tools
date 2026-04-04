/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Diagnostics/DiagnosticsTests.cs
 * File: DiagnosticsTests.cs
 * Created: 2026-04-01
 * Modified: 2026-04-03
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Tools.Config;
using Tools.Files;

namespace Tools.Diagnostics
{
    public static class DiagnosticsTests
    {
        public static DiagnosticResult Test_ConfigFileExists(string configPath)
        {
            return DiagnosticsHelpers.CheckFileExists(configPath, "Config File Exists");
        }
        public static DiagnosticResult Test_ConfigFileReadable(string configPath)
        {
            return DiagnosticsHelpers.CheckFileReadable(configPath, "Config File Readable");
        }
        public static DiagnosticResult Test_ConfigFileValidLiteDB(string configPath)
        {
            // Reuse shared checks
            DiagnosticResult readable = DiagnosticsHelpers.CheckFileReadable(configPath, "Config File Valid LiteDB");
            if (!readable.Passed)
                return readable;

            // Now test LiteDB validity
            try
            {
                using var db = new LiteDatabase(configPath);
            }
            catch (Exception ex)
            {
                return new DiagnosticResult(
                    "Config File Valid LiteDB",
                    passed: false,
                    error: $"Invalid LiteDB file: {ex.Message}"
                );
            }

            return new DiagnosticResult("Config File Valid LiteDB", passed: true);
        }
    }
}
