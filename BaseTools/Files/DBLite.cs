/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Files/DBLite.cs
 * File: DBLite.cs
 * Version: 1.0.0
 * Created: 2026-03-31
 * Modified: 2026-05-03
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using LiteDB;
using System;
using Tools.Config;
using Tools.Enums;

namespace Tools.Files
{
    public sealed class DBLite : IDisposable
    {
        private readonly LiteDatabase _db;
        private readonly BsonMapper _mapper;
        private static readonly Dictionary<int, string> VersionHistory = new()
        {
            { 1, "Initial schema" },
            { 2, "PasswordMetadata structure introduced" },
            { 3, "AppPath structure introduced (LogPath, DataPath, TempPath)" },
            { 4, "Consolidated LogPath, DataPath, and TempPath into a list" },
            { 5, "Repaired Path Names" }
        };
        public static string DescribeVersion(int version)
        {
            return VersionHistory.TryGetValue(version, out var desc)
                ? desc
                : "Unknown version";
        }

        private static PathType DetectPathType(string path)
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
        public DBLite(string path)
        {
            _mapper = BsonMapper.Global;
            _db = new LiteDatabase(path, _mapper);
        }
        public void CreateTableIfMissing<T>(string name)
            where T : class, new()
        {
            _db.GetCollection<T>(name);
        }
        public T? Load<T>(string name)
            where T : class, new()
        {
            ILiteCollection<BsonDocument> col = _db.GetCollection(name);

            // Load raw BSON
            var doc = col.FindById(1);
            if (doc == null)
                return null;

            bool changed = false;

            // MIGRATIONS
            int version = doc["Version"].AsInt32;

            if (version < 2)
                changed |= MigratePasswordFields(doc);

            if (version < 3)
                changed |= MigrateAppPaths(doc);

            if (version < 4)
                changed |= MigratePathPropertiesToList(doc);

            // Update version if needed
            if (version < 4)
            {
                doc["Version"] = 4;
                changed = true;
            }
            if (version < 5)
            {
                changed |= RepairEmptyPathNames(doc);
                doc["Version"] = 5;
            }
            // Only write back if something changed
            if (changed)
                col.Upsert(doc);

            // Now safely map to T
            return BsonMapper.Global.ToObject<T>(doc);
        }
        public static bool MigrateConfig(BsonDocument doc)
        {
            bool changed = false;

            changed |= MigratePathPropertiesToList(doc);  // old v1→v2
            changed |= NormalizePathsList(doc);           // old v2→v3
            changed |= RepairEmptyPathNames(doc);         // NEW v3→v4

            return changed;
        }
        private static bool MigratePathPropertiesToList(BsonDocument doc)
        {
            if (doc.ContainsKey("Paths"))
                return false;

            bool changed = false;
            var list = new BsonArray();

            void Convert(string oldKey, string newName)
            {
                if (!doc.ContainsKey(oldKey))
                    return;

                var old = doc[oldKey].AsDocument;

                // Read old fields safely
                BsonValue typeVal = old.ContainsKey("Type") ? old["Type"] : new BsonValue((int)PathType.None);
                BsonValue pathVal = old.ContainsKey("Path") ? old["Path"] : new BsonValue("");

                list.Add(new BsonDocument
                {
                    ["PathName"] = newName,
                    ["PathType"] = typeVal.AsInt32,
                    ["PathValue"] = pathVal.AsString
                });

                doc.Remove(oldKey);
                changed = true;
            }

            Convert("LogPath", "LogPath");
            Convert("DataPath", "DataPath");
            Convert("TempPath", "TempPath");

            if (changed)
                doc["Paths"] = list;

            return changed;
        }

        private static bool MigrateAppPaths(BsonDocument doc)
        {
            bool changed = false;

            changed |= MigratePathField(doc, "LogPath");
            changed |= MigratePathField(doc, "DataPath");
            changed |= MigratePathField(doc, "TempPath");

            return changed;
        }

        private static bool MigratePathField(BsonDocument doc, string fieldName)
        {
            if (!doc.ContainsKey(fieldName))
                return false;

            var field = doc[fieldName];

            if (!field.IsString)
                return false;

            string oldValue = field.AsString;

            doc[fieldName] = new BsonDocument
            {
                ["PathName"] = fieldName,
                ["PathValue"] = oldValue,
                ["PathType"] = DetectPathType(oldValue).ToString()
            };

            return true;
        }

        private bool MigratePasswordFields(BsonDocument doc)
        {
            bool changed = false;

            changed |= MigrateField(doc, "AppPassword");
            changed |= MigrateField(doc, "ConfigPassword");
            changed |= MigrateField(doc, "DbPassword");

            return changed;
        }

        private bool MigrateField(BsonDocument doc, string fieldName)
        {
            if (!doc.ContainsKey(fieldName))
                return false;

            var field = doc[fieldName];

            if (!field.IsString)
                return false;
            // Old format: string
            if (field.IsString)
            {
                string oldValue = field.AsString;

                // Convert to new PasswordMetadata BSON structure
                doc[fieldName] = new BsonDocument
                {
                    ["Location"] = PasswordLocation.Database.ToString(),
                    ["Representation"] = PasswordRepresentation.Plaintext.ToString(),
                    ["Password"] = oldValue
                };
            }
            return true;
        }
        private static bool NormalizePathsList(BsonDocument doc)
        {
            if (!doc.ContainsKey("Paths"))
                return false;

            bool changed = false;
            var list = doc["Paths"].AsArray;

            foreach (var item in list)
            {
                if (item.IsDocument == false)
                    continue;

                var d = item.AsDocument;

                // Fix Name → PathName
                if (d.ContainsKey("Name"))
                {
                    d["PathName"] = d["Name"].AsString;
                    d.Remove("Name");
                    changed = true;
                }

                // Fix Type → PathType
                if (d.ContainsKey("Type"))
                {
                    d["PathType"] = d["Type"].AsInt32;
                    d.Remove("Type");
                    changed = true;
                }

                // Fix Path → PathValue
                if (d.ContainsKey("Path"))
                {
                    d["PathValue"] = d["Path"].AsString;
                    d.Remove("Path");
                    changed = true;
                }

                // Ensure required fields exist
                if (!d.ContainsKey("PathName"))
                {
                    d["PathName"] = "Unknown";
                    changed = true;
                }

                if (!d.ContainsKey("PathType"))
                {
                    d["PathType"] = (int)PathType.None;
                    changed = true;
                }

                if (!d.ContainsKey("PathValue"))
                {
                    d["PathValue"] = "";
                    changed = true;
                }
            }

            return changed;
        }

        public void Rebuild()
        {
            _db.Rebuild();
        }
        private static bool RepairEmptyPathNames(BsonDocument doc)
        {
            if (!doc.ContainsKey("Paths"))
                return false;

            bool changed = false;

            BsonArray list = doc["Paths"].AsArray;
            var names = Enum.GetNames(typeof(PathLocation)); // ["LogPath","DataPath","TempPath","DbPath","Custom"]

            for (int i = 0; i < list.Count; i++)
            {
                BsonValue item = list[i];
                if (!item.IsDocument)
                    continue;

                BsonDocument d = item.AsDocument;
                changed |= d.Remove("Name");
                changed |= d.Remove("Type");
                changed |= d.Remove("Path");

                // If PathName is missing or empty, repair using enum index
                if (!d.ContainsKey("PathName") || string.IsNullOrWhiteSpace(d["PathName"].AsString))
                {
                    if (i < names.Length)
                        d["PathName"] = names[i];
                    else
                        d["PathName"] = "Custom"; // fallback if list is longer than enum

                    changed = true;
                }

                // Ensure PathType exists
                if (!d.ContainsKey("PathType"))
                {
                    d["PathType"] = (int)PathType.None;
                    changed = true;
                }

                // Ensure PathValue exists
                if (!d.ContainsKey("PathValue"))
                {
                    d["PathValue"] = string.Empty;
                    changed = true;
                }
            }

            return changed;
        }

        public void Save<T>(string name, T data)
            where T : class, new()
        {
            ILiteCollection<T> col = _db.GetCollection<T>(name);

            // Ensure deterministic ID = 1
            var idProp = typeof(T).GetProperty("Id");
            if (idProp != null)
                idProp.SetValue(data, 1);

            col.Upsert(data);   // v6 API
        }
        public BsonDocument ToDocument<T>(T obj)
        {
            return _mapper.ToDocument(obj);
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
