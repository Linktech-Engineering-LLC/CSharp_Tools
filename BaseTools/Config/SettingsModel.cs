/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Config/SettingsModel.cs
 * File: SettingsModel.cs
 * Version: 1.0.0
 * Created: 2026-03-31
 * Modified: 2026-05-11
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
        public List<PasswordMetadata> Passwords { get; set; } = [];
        public List<AppPath> Paths { get; set; } = [];
        public DatabaseConfig Database { get; set; } = new();
    }
}
