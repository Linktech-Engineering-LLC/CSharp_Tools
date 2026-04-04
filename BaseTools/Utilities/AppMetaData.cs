/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Utilities/AppMetaData.cs
 * File: AppMetaData.cs
 * Created: 2025-12-30
 * Modified: 2026-01-31
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */






using System.Reflection;

namespace Tools.Utilities
{
    public static class AppMetaData
    {
        public static string Product =>
            Assembly.GetEntryAssembly()?
                .GetCustomAttribute<AssemblyProductAttribute>()?.Product
            ?? "Unknown Product";

        public static string Company =>
            Assembly.GetEntryAssembly()?
                .GetCustomAttribute<AssemblyCompanyAttribute>()?.Company
            ?? "Unknown Company";

        public static string Version =>
            Assembly.GetEntryAssembly()?.GetName().Version?.ToString()
            ?? "Unknown Version";

        public static string AssemblyName =>
            Assembly.GetEntryAssembly()?.GetName().Name
            ?? "Unknown Assembly";
    }
}