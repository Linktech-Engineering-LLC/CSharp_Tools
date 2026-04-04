/*
 * Project: DbTools
 * Program: DbTools.dll
 * Path: Tools/DbTools/DbConnectionTester.cs
 * File: DbConnectionTester.cs
 * Created: 2026-04-01
 * Modified: 2026-04-02
 * Author: Leon McClatchey
 * Company: Linktech Engineering, LLC
 * Description:
 */
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Microsoft.Data.SqlClient;
using Npgsql;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Tools.Config;

namespace DbTools
{
    public static class DbConnectionTester
    {
        public static async Task<bool> TestConnectionAsync(DatabaseSettings s)
        {
            return s.Engine switch
            {
                DatabaseEngine.MySQL or DatabaseEngine.MariaDB =>
                    await TestMySqlAsync(s),

                DatabaseEngine.PostgreSql =>
                    await TestPostgresAsync(s),

                DatabaseEngine.SQLServer =>
                    await TestSqlServerAsync(s),

                DatabaseEngine.SQLite =>
                    TestSqlite(s),

                _ => false
            };
        }

        private static async Task<bool> TestMySqlAsync(DatabaseSettings s)
        {
            string connStr =
                $"Server={s.Host};Port={s.Port};User ID={s.User};Password={s.Password};Database={s.Schema};";

            try
            {
                using var conn = new MySqlConnection(connStr);
                await conn.OpenAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static async Task<bool> TestPostgresAsync(DatabaseSettings s)
        {
            string connStr =
                $"Host={s.Host};Port={s.Port};Username={s.User};Password={s.Password};Database={s.Schema};";

            try
            {
                using var conn = new NpgsqlConnection(connStr);
                await conn.OpenAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static async Task<bool> TestSqlServerAsync(DatabaseSettings s)
        {
            string connStr =
                $"Server={s.Host},{s.Port};User ID={s.User};Password={s.Password};Database={s.Schema};";

            try
            {
                using var conn = new SqlConnection(connStr);
                await conn.OpenAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool TestSqlite(DatabaseSettings s)
        {
            try
            {
                using var conn = new SqliteConnection($"Data Source={s.Schema}");
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
