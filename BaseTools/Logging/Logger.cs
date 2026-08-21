/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Logging/Logger.cs
 * File: Logger.cs
 * Version: 1.0.1
 * Created: 2025-12-29
 * Modified: 2026-08-21
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
#region System Libraries
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
#endregion
#region Project Libraries
using Tools.Helpers;
using Tools.Config;
#endregion

namespace Tools.Logging
{
    public enum ArchiveType
    {
        Daily,
        Monthly,
        Annual
    }
    public enum LogFormat
    {
        PlainText,
        Json,
        Xml
    }
    public enum LogLevel
    {
        Trace,
        Debug,
        Info,
        Warning,
        Error,
        Critical,
        Audit
    }
    public enum LoggerOption
    {
        EnableCompression,
        AutoRotateLogs,
        IncludeRawLogs,
        VerboseArchiveLogging
    }
    public enum RotationType
    {
        SizeBased,
        TimeBased,
        CountBased,
        None
    }
    public class Logger
    {
        #region Private Variables
        #endregion
        #region Constructors/Destructors
        public Logger(string appname, LoggerConfig cfg)
        {
            Config = cfg;

            AppName = appname;
            LogFileName = appname + ".log";

            Directory.CreateDirectory(Config.LogDirectory.PathValue);
        }
        #endregion
        #region Public Properties
        public LoggerConfig Config { get; }
        public string AppName { get; }
        public string LogFileName { get; set; }
        public string FullLogPath => Path.Combine(Config.LogDirectory.PathValue, LogFileName);
        #endregion
        #region Private Methods
        private string BuildArchiveName(ArchiveType type, DateTime? dt = null)
        {
            DateTime ts = dt ?? DateTime.Now;

            string dailyBase = Path.GetFileNameWithoutExtension(LogFileName);
            string periodicBase = AppName;
            bool compressed = Config.Options.Contains(LoggerOption.EnableCompression);

            return type switch
            {
                ArchiveType.Daily =>
                    compressed
                        // Compressed daily → no time component
                        ? $"{dailyBase}_{ts:yyyyMMdd}.zip"
                        // Uncompressed daily → include time
                        : $"{dailyBase}_{ts:yyyyMMdd_HHmmss}.log",


                ArchiveType.Monthly =>
                    $"{periodicBase}_{ts:yyyyMM}.zip",

                ArchiveType.Annual =>
                    $"{periodicBase}_{ts:yyyy}.zip",

                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
        }
        private void CleanArchives()
        {
            if (Config.RetentionDays <= 0)
                return;

            string archiveDir = GetArchiveDirectory();
            DateTime cutoff = DateTime.Now.AddDays(-Config.RetentionDays);

            foreach (string file in Directory.GetFiles(archiveDir))
            {
                DateTime modified = File.GetLastWriteTime(file);

                if (modified < cutoff)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        // Log retention failure but do not interrupt the archive pipeline
                        Warning("Retention", $"Failed to delete {file}: {ex.Message}");
                    }
                }
            }
        }
        private void CompressFile(string sourcePath)
        {
            // Safety: if compression is disabled, do nothing
            if (!Config.Options.Contains(LoggerOption.EnableCompression))
                return;

            // If the file is already a .zip, do nothing
            if (sourcePath.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                return;

            // Build the compressed file path
            string zipPath = Path.ChangeExtension(sourcePath, ".zip");

            // Create the .zip archive
            using (FileStream zipStream = new(zipPath, FileMode.Create))
            using (ZipArchive archive = new(zipStream, ZipArchiveMode.Create))
            {
                archive.CreateEntryFromFile(sourcePath, Path.GetFileName(sourcePath));
            }

            // Remove the uncompressed archive file
            try
            {
                File.Delete(sourcePath);
            }
            catch (Exception ex)
            {
                Warning("ArchiveCompression", $"Failed to delete {sourcePath}: {ex.Message}");
            }
        }
        private void CompressPeriodicArchives(ArchiveType type, string rollupPath)
        {
            // Determine which files to include in the roll-up
            IEnumerable<string> filesToArchive = type switch
            {
                ArchiveType.Monthly => GetDailyArchivesForMonth(rollupPath),
                ArchiveType.Annual => GetMonthlyArchivesForYear(rollupPath),
                _ => throw new InvalidOperationException("Periodic archives must be Monthly or Annual")
            };

            if (!filesToArchive.Any())
                return;

            using (FileStream zipToOpen = new(rollupPath, FileMode.Create))
            using (ZipArchive archive = new(zipToOpen, ZipArchiveMode.Create))
            {
                foreach (string file in filesToArchive)
                {
                    string entryName = Path.GetFileName(file);
                    archive.CreateEntryFromFile(file, entryName, CompressionLevel.Optimal);
                }
            }
        }
        private void Flush()
        {
            // Ensure the file exists before archiving
            if (!File.Exists(FullLogPath))
                using (File.Create(FullLogPath)) { }
        }
        private string GetArchiveDirectory()
        {
            Directory.CreateDirectory(Config.ArchivePath.PathValue);
            return Config.ArchivePath.PathValue;
        }
        private IEnumerable<string> GetDailyArchives()
        {
            string archiveDir = Config.ArchivePath.PathValue;
            string dailyBase = Path.GetFileNameWithoutExtension(LogFileName);

            // Matches:
            //   Medical_YYYYMMDD.log
            //   Medical_YYYYMMDD_HHMMSS.log
            //   Medical_YYYYMMDD.zip
            return Directory.EnumerateFiles(archiveDir)
                .Where(f =>
                {
                    string fn = Path.GetFileNameWithoutExtension(f);

                    if (!fn.StartsWith(dailyBase + "_"))
                        return false;

                    // Extract the date segment (YYYYMMDD or YYYYMMDD_HHMMSS)
                    string[] segs = fn.Split('_');
                    if (segs.Length < 2)
                        return false;

                    string yyyymmdd = segs[1];

                    // Must be at least YYYYMMDD
                    return yyyymmdd.Length >= 8;
                });
        }
        private IEnumerable<string> GetDailyArchivesForMonth(string rollupPath)
        {
            string archiveDir = Config.ArchivePath.PathValue;

            // Extract YYYYMM from the rollup file name
            string name = Path.GetFileNameWithoutExtension(rollupPath);
            string[] parts = name.Split('_');
            string yearMonth = parts[^1]; // e.g., "202602"

            string dailyBase = Path.GetFileNameWithoutExtension(LogFileName);

            return Directory.EnumerateFiles(archiveDir)
                .Where(f =>
                {
                    string fn = Path.GetFileNameWithoutExtension(f);

                    // Must start with "<dailyBase>_"
                    if (!fn.StartsWith(dailyBase + "_"))
                        return false;

                    // Extract the date segment: YYYYMMDD or YYYYMMDD_HHMMSS
                    string[] segs = fn.Split('_');
                    if (segs.Length < 2)
                        return false;

                    string yyyymmdd = segs[1];

                    // Must be at least YYYYMMDD
                    if (yyyymmdd.Length < 8)
                        return false;

                    // Compare YYYYMM
                    return yyyymmdd.StartsWith(yearMonth);
                });
        }
        private IEnumerable<string> GetMonthlyArchivesForYear(string rollupPath)
        {
            string archiveDir = Config.ArchivePath.PathValue;

            // Extract YYYY from the rollup file name
            string name = Path.GetFileNameWithoutExtension(rollupPath);
            string[] parts = name.Split('_');
            string year = parts[^1]; // e.g., "2026"

            string periodicBase = AppName;

            return Directory.EnumerateFiles(archiveDir, $"{periodicBase}_*.zip")
                .Where(f =>
                {
                    string fn = Path.GetFileNameWithoutExtension(f);
                    string[] segs = fn.Split('_');
                    if (segs.Length < 2)
                        return false;

                    string yyyymm = segs[1];

                    // Must be at least YYYYMM
                    if (yyyymm.Length < 6)
                        return false;

                    return yyyymm.StartsWith(year);
                });
        }
        private void NormalizeDailyArchives()
        {
            foreach (string file in GetDailyArchives())
            {
                if (file.EndsWith(".log", StringComparison.OrdinalIgnoreCase))
                    CompressFile(file);
            }
        }
        private void PerformArchive(ArchiveType type, string archivePath, bool isDaily)
        {
            string domain = $"Archive{type}";

            if (isDaily)
            {
                // Move the active log to the archive
                File.Move(FullLogPath, archivePath);

                // Compress if enabled and extension is .zip
                if (Config.Options.Contains(LoggerOption.EnableCompression) && archivePath.EndsWith(".zip"))
                    CompressFile(archivePath);

                // Create a fresh empty active log
                using (File.Create(FullLogPath)) { }

                Info(domain, $"Created {type.ToString().ToLowerInvariant()} archive: {archivePath}");
            }
            else
            {
                NormalizeDailyArchives();
                // Monthly/Annual rollups operate on existing archives
                CompressPeriodicArchives(type, archivePath);

                Info(domain, $"Created {type.ToString().ToLowerInvariant()} rollup: {archivePath}");
            }

            // Apply retention rules
            CleanArchives();
        }
        private void RotateIfNeeded()
        {
            if (!File.Exists(FullLogPath))
                return;

            FileInfo fi = new(FullLogPath);

            if (fi.Length >= Config.MaxLogSizeBytes)
                Archive(ArchiveType.Daily);
        }

        #endregion
        #region Public Methods
        public void Archive(ArchiveType type)
        {
            // Ensure the active log is flushed
            Flush();

            // Build the archive file name
            string archiveName = BuildArchiveName(type);

            // Build the full archive path
            string archivePath = Path.Combine(GetArchiveDirectory(), archiveName);

            // Determine if this is a daily archive
            bool isDaily = (type == ArchiveType.Daily);

            // Perform the archive operation
            PerformArchive(type, archivePath, isDaily);
        }
        public long GetLogSize()
        {
            string path = FullLogPath;

            if (!File.Exists(path))
                return 0;

            return new FileInfo(path).Length;
        }
        public void Log(string domain, string pid, string facility, LogLevel level, string message)
        {
            Log(domain, pid, facility, level.ToString("G"), message);
        }
        public void Log(string domain, string pid, string facility, string level, string message)
        {
            string timestamp = TimeHelpers.LogCurrentTimeStamp;

            // Build the log entry
            string entry =
                $"{timestamp} {domain}:[ID {pid} {facility}.{level}]:{message}";
            // Append to the unified application log
            WriteLine(entry);
        }
        public string ReadLog()
        {
            string path = FullLogPath;

            if (!File.Exists(path))
                return string.Empty;

            return File.ReadAllText(path);
        }
        public void SetLogPath(string fullPath)
        {
            fullPath = Path.GetFullPath(fullPath);

            // Split into directory + filename
            string dir = Path.GetDirectoryName(fullPath)!;
            string file = Path.GetFileName(fullPath);

            Config.LogDirectory.PathValue = dir;
            LogFileName = file;

            // Ensure directory exists
            Directory.CreateDirectory(dir);

            // Ensure file exists
            if (!File.Exists(fullPath))
                using (File.Create(fullPath)) { }
        }
        public void Write(string text)
        {
            Directory.CreateDirectory(Config.LogDirectory.PathValue);
            RotateIfNeeded();
            File.AppendAllText(FullLogPath, text);
        }

        public void WriteLine(string text)
        {
            Directory.CreateDirectory(Config.LogDirectory.PathValue);
            RotateIfNeeded();
            File.AppendAllText(FullLogPath, text + Environment.NewLine);
        }

        #endregion
        #region Convenience Methods
        public void Trace(string domain, string message) =>
            Log(domain,
                Process.GetCurrentProcess().Id.ToString(),
                "local7",
                "TRACE",
                message);

        public void Debug(string domain, string message) =>
            Log(domain,
                Process.GetCurrentProcess().Id.ToString(),
                "local7",
                "DEBUG",
                message);

        public void Info(string domain, string message) =>
            Log(domain,
                Process.GetCurrentProcess().Id.ToString(),
                "local7",
                "INFO",
                message);

        public void Warning(string domain, string message) =>
            Log(domain,
                Process.GetCurrentProcess().Id.ToString(),
                "local7",
                "WARNING",
                message);

        public void Error(string domain, string message) =>
            Log(domain,
                Process.GetCurrentProcess().Id.ToString(),
                "local7",
                "ERROR",
                message);

        public void Critical(string domain, string message) =>
            Log(domain,
                Process.GetCurrentProcess().Id.ToString(),
                "local7",
                "CRITICAL",
                message);

        public void Audit(string domain, string message) =>
            Log(domain,
                Process.GetCurrentProcess().Id.ToString(),
                "local7",
                "AUDIT",
                message);        
        #endregion
        #region Exception Logging
        public void AuditException(string domain, string message, Exception ex) =>
            Audit(domain, FormatExceptionEntry(message, ex));
        public void Crash(string domain, Exception ex)
        {
            string prefix =
                $"CRASH DETECTED | ProcessID: {Process.GetCurrentProcess().Id} | " +
                $"WorkingDir: {Environment.CurrentDirectory}";

            Critical(domain, FormatExceptionEntry(prefix, ex));
        }
        public void CriticalException(string domain, string message, Exception ex) =>
            Critical(domain, FormatExceptionEntry(message, ex));

        public void DebugException(string domain, string message, Exception ex) =>
            Debug(domain, FormatExceptionEntry(message, ex));

        public void Error(string domain, string message, Exception ex = null)
        {
            if (ex == null)
            {
                Error(domain, message);
                return;
            }

            Error(domain, FormatExceptionEntry(message, ex));
        }
        public string FormatExceptionEntry(string message, Exception ex)
        {
            string stack = ex.StackTrace?.Replace(Environment.NewLine, " ") ?? "no stack trace";

            return $"{message} | Exception: {ex.GetType().Name}: {ex.Message} | StackTrace: {stack}";
        }
        public void TraceException(string domain, string message, Exception ex)
        {
            string stack = ex.StackTrace?.Replace(Environment.NewLine, " ") ?? "no stack trace";
            string combined =
                $"{message} | Exception: {ex.GetType().Name}: {ex.Message} | StackTrace: {stack}";

            Trace(domain, combined);
        }
        public void WarningException(string domain, string message, Exception ex) =>
            Warning(domain, FormatExceptionEntry(message, ex));



        #endregion
    }
}