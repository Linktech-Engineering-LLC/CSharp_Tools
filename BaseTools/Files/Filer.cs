/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Files/Filer.cs
 * File: Filer.cs
 * Version: 1.0.0
 * Created: 2025-12-30
 * Modified: 2026-05-03
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */

#region System Libraries
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using System.Formats.Asn1;
using System.Reflection.PortableExecutable;
using System.Diagnostics.CodeAnalysis;
#endregion
#region Project Libraries
using Tools.Utilities;
using Tools.Logging;
using Tools.Config;
#endregion

namespace Tools.Files
{
    /// <summary>
    /// Filer provides centralized file utilities:
    /// - Filters for common file types
    /// - Paths dictionary for log and config resolution
    /// - Flags for file operations (append, create, recycle, etc.)
    /// - Audit-friendly log path resolution
    /// </summary>
    public class Filer
    {
        #region Public Enums/Structs
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct SHQUERYRBINFO
        {
            public int cbSize;
            public long i64Size;
            public long i64NumItems;
        }
        public enum RecycleFlag : int
        {
            SHERB_NOCONFIRMATION = 0x00000001,
            SHERB_NOPROGRESSUI = 0x00000002,
            SHERB_NOSOUND = 0x00000004
        }
        #endregion
        #region Private Variables
        #endregion
        #region Private Static Helper Methods
        #endregion
        #region Public Constants
        public const string LOG_FILTER = "Log Files (*.log) | *.log";
        public const string TEXT_FILTER = "Text Files (*.txt) | *.txt";
        public const string CSV_FILTER = "CSV Files (*.csv) | *.csv";
        public const string INI_FILTER = "INI Files (*.ini) | *.ini";
        public const string EXE_FILTER = "Executable Files (*.exe;*.com) | *.exe;*.com";
        #endregion
        #region Constructors/Destructors
        public Filer()
        {
        }
        #endregion
        #region Public Properties
        #endregion
        #region Public Methods
        public static PathType DetectPathType(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return PathType.None;

            if (path.StartsWith(@"\\?\"))
                return PathType.DeviceLiteral;

            if (path.StartsWith(@"\\."))
                return PathType.DeviceDOS;

            if (path.StartsWith(@"\\"))
                return PathType.UNC;

            if (path.Contains(':'))
                return PathType.LocalDrive;

            return PathType.Relative;
        }
        #endregion
    }
}