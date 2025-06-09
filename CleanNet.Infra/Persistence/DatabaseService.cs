using CleanNet.Application.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNet.Infra.Persistence
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string _connString;
        public DatabaseService(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("Default");
        }

        public async Task InsertCatFactAsync(string fact, int length)
        {
            const string sql = "INSERT INTO cat_facts (fact, length) VALUES (@fact, @length);";
            using var conn = new NpgsqlConnection(_connString);
            await conn.ExecuteAsync(sql, new { fact, length });
        }
    }
}
