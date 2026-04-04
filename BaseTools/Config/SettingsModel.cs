/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Config/SettingsModel.cs
 * File: SettingsModel.cs
 * Created: 2026-03-31
 * Modified: 2026-04-04
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using LiteDB;

using Tools.Enums;

namespace Tools.Config
{
    public class SettingsModel
    {
        [BsonId]
        public int Id { get; set; } = 1;
        public int Version { get; set; } = 1;
        public PasswordStyles PasswordStyle { get; set; } = PasswordStyles.Vault;
        public string AppPassword { get; set; } = string.Empty;
        public string ConfigPassword { get; set; } = string.Empty;
        public string DbPassword { get; set; } = string.Empty;
        public string VaultKey { get; set; } = string.Empty;

        public string LogPath { get; set; } = string.Empty;
        public string DataPath { get; set; } = string.Empty;
        public string TempPath { get; set; } = string.Empty;

        public string DbHost { get; set; } = "localhost";
        public int DbPort { get; set; } = 3306;
        public string DbName { get; set; } = string.Empty;
        public string DbUser { get; set; } = string.Empty;
        public string DbInstance { get; set; } = string.Empty;
    }
}
