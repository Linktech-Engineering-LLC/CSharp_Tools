/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Config/LoggerConfig.cs
 * File: LoggerConfig.cs
 * Version: 1.0.4
 * Created: 2026-06-05
 * Modified: 2026-08-21
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
#region Public Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Logging;
#endregion
#region Project Libraries
using Tools.Logging;
#endregion
namespace Tools.Config
{
    public class LoggerConfig
    {
        public AppPath LogDirectory { get; set; }
        public AppPath RotatePath { get; set; }
        public AppPath ArchivePath { get; set; }

        public LogLevel MinimumLevel { get; set; }
        public RotationType RotationType { get; set; }
        public ArchiveType ArchiveType { get; set; }
        public int RetentionDays { get; set; }
        public long MaxLogSizeBytes { get; set; }
        public List<LoggerOption> Options { get; set; } = new();
    }
}
