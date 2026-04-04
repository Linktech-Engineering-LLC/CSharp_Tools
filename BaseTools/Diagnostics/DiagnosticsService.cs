/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Diagnostics/DiagnosticsService.cs
 * File: DiagnosticsService.cs
 * Created: 2026-04-02
 * Modified: 2026-04-02
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Tools.Config;

namespace Tools.Diagnostics
{
    public static class DiagnosticsService
    {
        // 1. Extract required fields from SettingsModel (schema source)
        // ------------------------------------------------------------
        private static List<string> GetRequiredFields()
        {
            return typeof(SettingsModel)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => p.Name)
                .ToList();
        }

        // ------------------------------------------------------------
        // 2. Extract actual fields from DBLite Settings document
        // ------------------------------------------------------------
        private static List<string> GetActualFields(string dbPath)
        {
            using var db = new LiteDatabase(dbPath);

            var col = db.GetCollection("Settings");
            var doc = col.FindById(1);

            if (doc == null)
                return new List<string>();

            return doc.Keys.ToList();
        }

        // ------------------------------------------------------------
        // 3. Test: Required fields exist
        // ------------------------------------------------------------
        public static DiagnosticResult TestRequiredFields(string dbPath)
        {
            List<string> required = GetRequiredFields();
            List<string> actual = GetActualFields(dbPath);
            // Remove ignored fields
            required = [.. required.Where(f => !DBLiteSchema.IgnoredFields.Contains(f))];
            actual = [.. actual.Where(f => !DBLiteSchema.IgnoredFields.Contains(f))];
            List<string> missing = required.Except(actual).ToList();
            List<string> extra = actual.Except(required).ToList();

            return new DiagnosticResult(
                "Schema Field Validation",
                missing.Count == 0,
                missing.Count == 0 ? null : "One or more required fields are missing.",
                missingItems: missing,
                extraItems: extra
            );
        }

        // ------------------------------------------------------------
        // 4. Test: Settings document exists
        // ------------------------------------------------------------
        public static DiagnosticResult TestSettingsDocumentExists(string dbPath)
        {
            using var db = new LiteDatabase(dbPath);

            var col = db.GetCollection("Settings");
            var doc = col.FindById(1);

            bool ok = doc != null;

            return new DiagnosticResult(
                "Settings Document Exists",
                ok,
                ok ? null : "Settings document (_id = 1) is missing."
            );
        }

        // ------------------------------------------------------------
        // 5. Test: Schema version matches SettingsModel.Version
        // ------------------------------------------------------------
        public static DiagnosticResult TestSchemaVersion(string dbPath, SettingsModel settings)
        {
            using LiteDatabase db = new(dbPath);

            ILiteCollection<BsonDocument> col = db.GetCollection("Settings");
            BsonDocument doc = col.FindById(1);

            if (doc == null)
            {
                return new DiagnosticResult(
                    "Schema Version",
                    false,
                    "Settings document not found."
                );
            }

            int dbVersion = doc["Version"].AsInt32;
            int modelVersion = settings.Version;

            bool ok = dbVersion == modelVersion;

            return new DiagnosticResult(
                "Schema Version",
                ok,
                ok ? null : $"Schema version mismatch. DB={dbVersion}, Model={modelVersion}"
            );
        }

        // ------------------------------------------------------------
        // 6. Test: DBLite file can be opened (not locked)
        // ------------------------------------------------------------
        public static DiagnosticResult TestDatabaseOpenable(string dbPath)
        {
            try
            {
                using var db = new LiteDatabase(dbPath);
                return new DiagnosticResult("Database Openable", true);
            }
            catch (Exception ex)
            {
                return new DiagnosticResult(
                    "Database Openable",
                    false,
                    error: ex.Message
                );
            }
        }

        // ------------------------------------------------------------
        // 7. Run all diagnostics
        // ------------------------------------------------------------
        public static List<DiagnosticResult> RunAll(string dbPath, SettingsModel settings)
        {
            return new List<DiagnosticResult>
            {
                TestDatabaseOpenable(dbPath),
                TestSettingsDocumentExists(dbPath),
                TestRequiredFields(dbPath),
                TestSchemaVersion(dbPath,settings)
            };
        }
    }
}
