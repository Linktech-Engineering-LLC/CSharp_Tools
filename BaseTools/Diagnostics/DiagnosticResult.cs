/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Diagnostics/DiagnosticResult.cs
 * File: DiagnosticResult.cs
 * Created: 2026-04-01
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
    public sealed class DiagnosticResult
    {
        public string TestName { get; }
        public bool Passed { get; }
        public string? Error { get; }

        // Optional structured details
        public IReadOnlyList<string> MissingItems { get; }
        public IReadOnlyList<string> ExtraItems { get; }

        public DiagnosticResult(
            string testName,
            bool passed,
            string? error = null,
            IEnumerable<string>? missingItems = null,
            IEnumerable<string>? extraItems = null)
        {
            TestName = testName;
            Passed = passed;
            Error = error;

            MissingItems = missingItems?.ToList() ?? new List<string>();
            ExtraItems = extraItems?.ToList() ?? new List<string>();
        }
        public static DiagnosticResult RunTest(DiagnosticTest test, string configPath)
        {
            return test switch
            {
                DiagnosticTest.ConfigFileExists =>
                    DiagnosticsTests.Test_ConfigFileExists(configPath),

                DiagnosticTest.ConfigFileReadable =>
                    DiagnosticsTests.Test_ConfigFileReadable(configPath),

                DiagnosticTest.ConfigFileValidLiteDB =>
                    DiagnosticsTests.Test_ConfigFileValidLiteDB(configPath),

                _ => new DiagnosticResult("Unknown Test", false, "Not implemented")
            };
        }
        public string ToDisplayString()
        {
            if (Passed)
                return $"✔ {TestName}";

            if (!string.IsNullOrWhiteSpace(Error))
                return $"✖ {TestName}: {Error}";

            return $"✖ {TestName}";
        }
    }
}
