/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Files/DBLite.cs
 * File: DBLite.cs
 * Version: 1.0.0
 * Created: 2026-03-31
 * Modified: 2026-04-22
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using LiteDB;
using System;
using Tools.Enums;

namespace Tools.Files
{
    public sealed class DBLite : IDisposable
    {
        private readonly LiteDatabase _db;
        public DBLite(string path)
        {
            _db = new LiteDatabase(path);
        }
        public void CreateTableIfMissing<T>(string name)
            where T : class, new()
        {
            _db.GetCollection<T>(name);
        }

        public T? Load<T>(string name)
            where T : class, new()
        {
            var col = _db.GetCollection(name);

            // Load raw BSON
            var doc = col.FindById(1);
            if (doc == null)
                return null;

            // MIGRATION: fix old string password fields before mapping to T
            MigratePasswordFields(doc);

            // Now safely map to T
            return BsonMapper.Global.ToObject<T>(doc);
        }
        private void MigratePasswordFields(BsonDocument doc)
        {
            MigrateField(doc, "AppPassword");
            MigrateField(doc, "ConfigPassword");
            MigrateField(doc, "DbPassword");
        }

        private void MigrateField(BsonDocument doc, string fieldName)
        {
            if (!doc.ContainsKey(fieldName))
                return;

            var field = doc[fieldName];

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
        }
        public void Rebuild()
        {
            _db.Rebuild();
        }

        public void Save<T>(string name, T data)
            where T : class, new()
        {
            var col = _db.GetCollection<T>(name);

            // Ensure deterministic ID = 1
            var idProp = typeof(T).GetProperty("Id");
            if (idProp != null)
                idProp.SetValue(data, 1);

            col.Upsert(data);   // v6 API
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
