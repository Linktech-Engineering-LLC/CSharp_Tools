/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Config/DBLiteSchema.cs
 * File: DBLiteSchema.cs
 * Version: 1.0.0
 * Created: 2026-03-31
 * Modified: 2026-04-30
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using LiteDB;
using System;
using Tools.Files;

namespace Tools.Config
{
    public static class DBLiteSchema
    {
        private const int _SchemaVersion = 1;
        public static int SchemaVersion => _SchemaVersion;
        public static readonly HashSet<string> IgnoredFields = new()
        {
            "Id",   // C# property
            "id",   // LiteDB internal field
            "_id"   // LiteDB internal primary key
        };

        /// <summary>
        /// Ensures the DBLite file contains the required tables and schema.
        /// Creates tables if missing and performs migrations if needed.
        /// </summary>
        public static void Ensure(DBLite db)
        {
            // Ensure schema version table exists
            db.CreateTableIfMissing<SchemaInfo>("SchemaInfo");

            SchemaInfo info = db.Load<SchemaInfo>("SchemaInfo") ?? new SchemaInfo();

            if (info.Version == 0)
            {
                // Fresh database — create all required tables
                CreateInitialSchema(db);

                info.Version = SchemaVersion;
                db.Save("SchemaInfo", info);
                return;
            }

            // Future: handle migrations
            if (info.Version < SchemaVersion)
            {
                Migrate(db, info.Version);
                info.Version = SchemaVersion;
                db.Save("SchemaInfo", info);
            }
        }

        /// <summary>
        /// Creates the initial schema for a new DBLite configuration file.
        /// </summary>
        private static void CreateInitialSchema(DBLite db)
        {
            db.CreateTableIfMissing<SettingsModel>("Settings");
        }

        /// <summary>
        /// Handles schema migrations between versions.
        /// </summary>
        private static void Migrate(DBLite db, int fromVersion)
        {
            // Example migration pattern:
            //
            // if (fromVersion < 2)
            // {
            //     db.AddColumn<SettingsModel>("Settings", "NewField", defaultValue: "");
            // }
            //
            // if (fromVersion < 3)
            // {
            //     db.CreateTableIfMissing<NewTable>("NewTable");
            // }

            // No migrations yet — schema version 1 is the baseline
        }

        /// <summary>
        /// Internal schema version tracking.
        /// </summary>
        private class SchemaInfo
        {
            [BsonId]
            public int Id { get; set; } = 1;

            public int Version { get; set; }
        }
    }
}
