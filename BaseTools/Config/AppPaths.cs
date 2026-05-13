/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Config/AppPaths.cs
 * File: AppPaths.cs
 * Version: 1.0.1
 * Created: 2026-05-01
 * Modified: 2026-05-13
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Config
{
    public enum PathType
    {
        None,
        LocalDrive,
        UNC,
        DeviceLiteral,
        DeviceDOS,
        Relative,
        Registry,
        VirtualDevice
    }
    public enum PathLocation
    {
        LogPath,
        DataPath,
        TempPath,
        DbPath,
        Custom
    }
    public class AppPath
    {
        public PathLocation PathName { get; set; }
        public PathType PathType { get; set; } = PathType.LocalDrive;
        public string PathValue { get; set; } = string.Empty;
    }
}
