using Microsoft.Extensions.Logging;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Core.DataAccess.SqlLite
{
    public class DbContext
    {
        protected readonly IConfiguration _configuration;
        public DbContext(ILogger<DbContext> logger, IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
        {
            return new SqliteConnection(_configuration.GetConnectionString("WebServerDatabase"));
        }
        public async Task Init() 
        {
            // create database tables if they don't exist
            using var connection = CreateConnection();
            await _initUsersTable();

            async Task _initUsersTable()
            {
                var sql = @"CREATE TABLE IF NOT EXISTS 
                            Users (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                            Username TEXT NOT NULL,
                            Email TEXT NOT NULL,
                            Phone TEXT,
                            SkillSets TEXT,
                            Hobby TEXT);";
                await connection.ExecuteAsync(sql);
            }
        }
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql) 
        {
            using var conn = CreateConnection();
            return (IEnumerable<T>)(await conn.QueryAsync<T>(sql));
        }
        public async Task ExecuteAsync(string sql, object para = null)
        {
            using var conn = CreateConnection();
            await conn.ExecuteAsync(sql, para);
        }
    }
}
