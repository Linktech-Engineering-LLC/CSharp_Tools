/*
 * Linktech Engineering Tools Suite
 * (c) 2026 Leon McClatchey
 * (c) 2026 Linktech Engineering, LLC
 * Licensed under the MIT License.
 */

/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Config/ConfigManager.cs
 * File: ConfigManager.cs
 * Version: 1.0.1
 * Created: 2026-03-31
 * Modified: 2026-05-18
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

            SettingsModel settings = db.Load<SettingsModel>("Settings") ?? new SettingsModel();

            return settings;
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

            // Load existing document (raw)
            BsonDocument existing = db.Load<BsonDocument>("Settings") ?? new BsonDocument();

            // Convert UI model to a document
            BsonDocument updated = db.ToDocument(settings);

            // Merge UI fields into existing document
            foreach (KeyValuePair<string, BsonValue> kv in updated)
            {
                existing[kv.Key] = kv.Value;
            }

            // Save merged document
            db.Save("Settings", existing);
        }
    }
}
