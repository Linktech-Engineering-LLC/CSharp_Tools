/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Logging/LoggerConfig.cs
 * File: LoggerConfig.cs
 * Version: 1.0.1
 * Created: 2025-12-29
 * Modified: 2026-06-05
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */

namespace Tools.Logging
{
    public class LoggerConfig
    {
        #region Public Properties
        public string AppName { get; set; }
        public string ArchivePath { get; set; }
        public ArchiveType ArchiveType { get; set; }
        public string BannerText { get; set; }
        public string LogDirectory { get; set; }
        public string LogFileName { get; set; }
        public LogFormat LogFormat { get; set; }
        public long MaxLogSizeBytes { get; set; } = 100 * 1024 * 1024;
        public LogLevel MinimumLevel { get; set; }
        public string Module { get; set; }
        public List<LoggerOption> Options { get; set; } = new();
        public int RetentionDays { get; set; }
        public RotationType RotationType { get; set; }
        public string RotatePath { get; set; }

        #endregion
        #region Constructors/Destructors
        public LoggerConfig()
        {
            SetDefaults();
        }
        #endregion
        #region Public Methods
        public string FullLogPath => Path.Combine(LogDirectory, LogFileName);
        public void SetDefaults()
        {
            LogDirectory = Path.Combine(
                new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)
                    .Root.FullName,
                "Logs"
            );
            ArchivePath = Path.Combine(LogDirectory, "Archives");
            BannerText = "";
            RetentionDays = 30;
            ArchiveType = ArchiveType.Daily;
            LogFormat = LogFormat.PlainText;
            MinimumLevel = LogLevel.Info;

            Options.Clear();
            Options.Add(LoggerOption.AutoRotateLogs);
            Options.Add(LoggerOption.EnableCompression);

            RotationType = RotationType.SizeBased; // dormant for now
        }
        #endregion

    }
}