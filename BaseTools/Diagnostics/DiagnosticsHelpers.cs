/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Diagnostics/DiagnosticsHelpers.cs
 * File: DiagnosticsHelpers.cs
 * Created: None
 * Modified: 2026-04-02
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Diagnostics;

public static class DiagnosticsHelpers
{
    public static DiagnosticResult CheckFileExists(string configPath, string testName)
    {
        if (!File.Exists(configPath))
        {
            return new DiagnosticResult(
                testName,
                passed: false,
                error: $"Configuration file not found: {configPath}"
            );
        }

        return new DiagnosticResult(testName, passed: true);
    }

    public static DiagnosticResult CheckFileReadable(string configPath, string testName)
    {
        // First ensure it exists
        var exists = CheckFileExists(configPath, testName);
        if (!exists.Passed)
            return exists;

        try
        {
            using var stream = File.Open(
                configPath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.ReadWrite
            );
        }
        catch (Exception ex)
        {
            return new DiagnosticResult(
                testName,
                passed: false,
                error: ex.Message
            );
        }

        return new DiagnosticResult(testName, passed: true);
    }
}
