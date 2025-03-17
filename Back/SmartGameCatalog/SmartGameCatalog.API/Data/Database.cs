using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace SmartGameCatalog.API.Data
{
    /// <summary>
    /// Clase que gestiona la conexión a la base de datos PostgreSQL.
    /// </summary>
    public class Database
    {
        // Cadena de conexión a la base de datos.
        private readonly string _connectionString;

        /// <summary>
        /// Constructor que inicializa la cadena de conexión a partir de la configuración.
        /// </summary>
        /// <param name="configuration">Interfaz de configuración para obtener la cadena de conexión.</param>
        /// <exception cref="ArgumentNullException">Lanza una excepción si la cadena de conexión es nula.</exception>
        public Database(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgreSQL")
                ?? throw new ArgumentNullException(nameof(_connectionString));
        }

        /// <summary>
        /// Crea y devuelve una nueva conexión a la base de datos PostgreSQL.
        /// </summary>
        /// <returns>Una instancia de IDbConnection.</returns>
        public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
    }
}