/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Config/SettingsModel.cs
 * File: SettingsModel.cs
 * Version: 1.0.0
 * Created: 2026-03-31
 * Modified: 2026-05-03
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
        public PasswordMetadata AppPassword { get; set; } = new PasswordMetadata();
        public PasswordMetadata ConfigPassword { get; set; } = new PasswordMetadata();
        public PasswordMetadata DbPassword { get; set; } = new PasswordMetadata();

        public List<AppPath> Paths { get; set; } = new List<AppPath>();

        public string DbHost { get; set; } = "localhost";
        public int DbPort { get; set; } = 3306;
        public string DbName { get; set; } = string.Empty;
        public string DbUser { get; set; } = string.Empty;
        public string DbInstance { get; set; } = string.Empty;
    }
}
