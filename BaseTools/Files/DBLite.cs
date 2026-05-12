/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Files/DBLite.cs
 * File: DBLite.cs
 * Version: 1.0.0
 * Created: 2026-03-31
 * Modified: 2026-05-11
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
            { 5, "Repaired Path Names" },
            { 6, "Single DB Connection with legacy fields to Modern, multi-connection, engine based structure" },
            { 7, "Removed old Database Fields and Converted Password Properties to a list" }
        };
        public static string DescribeVersion(int version)
        {
            return VersionHistory.TryGetValue(version, out var desc)
                ? desc
                : "Unknown version";
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
            ILiteCollection<BsonDocument> col = _db.GetCollection<BsonDocument>(name);

            BsonDocument doc = col.FindById(1);
            if (doc == null)
            {
                return null;
            }

            int version = doc["Version"].AsInt32;

            // Enforce final schema
            if (version != 7)
                throw new InvalidOperationException($"Unsupported config version: {version}");

            // Optional: keep DB clean
            if (PurgeNullFields(doc))
            {
                col.Upsert(doc);
            }
            BsonValue raw = doc["Database"];

            return BsonMapper.Global.ToObject<T>(doc);
        }

        private static bool PurgeNullFields(BsonDocument doc)
        {
            bool changed = false;

            var keys = doc.Keys.ToList();
            foreach (var key in keys)
            {
                var value = doc[key];

                // Remove null fields
                if (value == null || value.IsNull)
                {
                    doc.Remove(key);
                    changed = true;
                    continue;
                }

                // Recurse into nested documents
                if (value.IsDocument)
                {
                    changed |= PurgeNullFields(value.AsDocument);
                }

                // Recurse into arrays
                if (value.IsArray)
                {
                    foreach (var item in value.AsArray)
                    {
                        if (item.IsDocument)
                            changed |= PurgeNullFields(item.AsDocument);
                    }
                }
            }

            return changed;
        }
        public void Rebuild()
        {
            _db.Rebuild();
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
