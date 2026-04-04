/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Config/DBLiteDiagnostics.cs
 * File: DBLiteDiagnostics.cs
 * Created: 2026-03-31
 * Modified: 2026-04-02
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using System;
using System.IO;

using Tools.Diagnostics;

namespace Tools.Files
{
    public static class DBLiteDiagnostics
    {
        /// <summary>
        /// Performs a full integrity check on the DBLite file.
        /// Uses LiteDB's Rebuild() internally, which validates
        /// headers, pages, collections, indexes, and BSON documents.
        /// </summary>
        public static bool CheckFileHealth(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return false;

                using var db = new DBLite(path);

                // Rebuild is the strongest integrity validator LiteDB offers.
                db.Rebuild();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Attempts to repair the DBLite file by performing a rebuild.
        /// If the file is corrupt but readable, this often restores it.
        /// </summary>
        public static bool Repair(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return false;

                using var db = new DBLite(path);
                db.Rebuild();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a detailed diagnostic snapshot of the DBLite file.
        /// </summary>
        public static DBLiteStatus GetStatus(string path)
        {
            var status = new DBLiteStatus
            {
                Path = path,
                Exists = File.Exists(path)
            };

            if (!status.Exists)
                return status;

            var info = new FileInfo(path);

            status.SizeBytes = info.Length;
            status.LastModified = info.LastWriteTime;

            // Lightweight open test
            try
            {
                using var db = new DBLite(path);
                status.Readable = true;
            }
            catch
            {
                status.Readable = false;
            }

            // Full integrity test
            status.Healthy = status.Readable && CheckFileHealth(path);

            return status;
        }
    }

    /// <summary>
    /// Represents the diagnostic state of a DBLite file.
    /// </summary>
    public class DBLiteStatus
    {
        public string Path { get; set; } = "";
        public bool Exists { get; set; }
        public bool Readable { get; set; }
        public bool Healthy { get; set; }
        public long SizeBytes { get; set; }
        public DateTime LastModified { get; set; }
        public List<DiagnosticResult> Diagnostics { get; set; } = new();
    }
}
