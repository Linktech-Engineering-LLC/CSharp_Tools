/*
 * Project: BaseTools
 * Program: BaseTools.dll
 * Path: Tools/BaseTools/Files/DBLite.cs
 * File: DBLite.cs
 * Created: 2026-03-31
 * Modified: 2026-04-02
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using System;
using LiteDB;

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
            var col = _db.GetCollection<T>(name);
            return col.FindById(1);
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
