using CleanNet.Application.Interfaces;
using CleanNet.Infra.Common;
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
        private readonly IAppLogger<DatabaseService> _appLogger;

        public DatabaseService(IConfiguration configuration, IAppLogger<DatabaseService> appLogger)
        {
            _connString = configuration.GetConnectionString("Default");
            _appLogger = appLogger;
        }

        public async Task InsertCatFactAsync(string fact, int length)
        {
            using var activity = TracingHelper.StartActivity("Insert CatFact to DB");

            const string sql = "INSERT INTO cat_facts (fact, length) VALUES (@fact, @length);";
            await using var conn = new NpgsqlConnection(_connString);

            try
            {
                activity?.SetTag("db.statement", sql);
                activity?.SetTag("db.system", "postgresql");

                await conn.ExecuteAsync(sql, new { fact, length });
                _appLogger.LogInformation("Berhasil insert cat_fact: {Fact}", fact);
            }
            catch (Exception ex)
            {
                _appLogger.LogError(ex, "Gagal insert ke tabel cat_facts dengan fact: {Fact}", fact);
                activity.SetError(ex);
                throw;
            }
        }
    }
}
