/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Config/ConfigManager.cs
 * File: ConfigManager.cs
 * Created: 2026-03-31
 * Modified: 2026-04-02
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
#region System Libraries
using LiteDB;
using System;
using System.IO;
#endregion
#region Project Libraries
using Tools.Files;
#endregion

namespace Tools.Config
{
    public static class ConfigManager
    {
        /// <summary>
        /// Builds the deterministic DBLite configuration path:
        /// %APPDATA%\Linktech\<AppName>.dblite
        /// </summary>
        public static string GetConfigPath(string appName)
        {
            string basePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Linktech"
            );

            Directory.CreateDirectory(basePath);

            return Path.Combine(basePath, $"{appName}.dblite");
        }

        /// <summary>
        /// Loads the settings from the DBLite file.
        /// If the file or table does not exist, returns a new SettingsModel.
        ///</summary>
        public static SettingsModel Load(string appName)
        {
            string path = GetConfigPath(appName);

            using DBLite db = new(path);

            // Load the settings row (or create defaults)
            SettingsModel settings = db.Load<SettingsModel>("Settings");

            return settings ?? new SettingsModel();
        }

        /// <summary>
        /// Saves the settings to the DBLite file.
        /// Creates the file and schema if needed.
        /// </summary>
        public static void Save(string appName, SettingsModel settings)
        {
            string path = GetConfigPath(appName);

            using DBLite db = new(path);

            // Ensure schema exists
            DBLiteSchema.Ensure(db);

            // Set schema version on the model
            settings.Version = DBLiteSchema.SchemaVersion;

            // Save the typed model
            db.Save("Settings", settings);
        }
    }
}
