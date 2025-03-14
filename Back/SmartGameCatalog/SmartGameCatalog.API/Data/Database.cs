using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace SmartGameCatalog.API.Data
{
    public class Database
    {
        private readonly string _connectionString;

        public Database(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgreSQL")
                ?? throw new ArgumentNullException(nameof(_connectionString));
        }

        public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
    }
}
