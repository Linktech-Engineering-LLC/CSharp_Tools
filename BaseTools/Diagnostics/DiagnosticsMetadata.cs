/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Diagnostics/DiagnosticsMetadata.cs
 * File: DiagnosticsMetadata.cs
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
    public static class DiagnosticsMetadata
    {
        public static string GetDescription(DiagnosticTest test) =>
            test switch
            {
                DiagnosticTest.ConfigFileExists =>
                    "Checks whether the configuration file exists at the expected path.",

                DiagnosticTest.ConfigFileReadable =>
                    "Verifies that the configuration file can be opened and read.",

                DiagnosticTest.ConfigFileValidLiteDB =>
                    "Ensures the configuration file is a valid LiteDB database.",

                _ => ""
            };

        public static string GetExpected(DiagnosticTest test) =>
            test switch
            {
                DiagnosticTest.ConfigFileExists =>
                    "The configuration file should exist at the configured path.",

                DiagnosticTest.ConfigFileReadable =>
                    "The configuration file should be readable without permission errors.",

                DiagnosticTest.ConfigFileValidLiteDB =>
                    "The configuration file should open as a valid LiteDB database.",

                _ => ""
            };

        public static string GetRepair(DiagnosticTest test) =>
            test switch
            {
                DiagnosticTest.ConfigFileExists =>
                    "Verify the file path in Settings. Ensure the file has not been deleted.",

                DiagnosticTest.ConfigFileReadable =>
                    "Check file permissions. Ensure the application has read access.",

                DiagnosticTest.ConfigFileValidLiteDB =>
                    "Replace the file with a valid LiteDB database or restore from backup.",

                _ => ""
            };
    }
}
