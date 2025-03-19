using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace SmartGameCatalog.API.Data
{
    /// <summary>
    /// Clase que gestiona la conexión a la base de datos PostgreSQL de forma manual.
    /// </summary>
    public class Database : IDisposable
    {
        // 🔹 Cadena de conexión a la base de datos
        private readonly string _connectionString;
        private IDbConnection? _connection;

        /// <summary>
        /// Constructor que inicializa la cadena de conexión a partir de la configuración.
        /// </summary>
        /// <param name="configuration">Interfaz de configuración para obtener la cadena de conexión.</param>
        /// <exception cref="ArgumentNullException">Lanza una excepción si la cadena de conexión es nula.</exception>
        public Database(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(_connectionString));
        }

        /// <summary>
        /// Crea y devuelve una nueva conexión a la base de datos PostgreSQL.
        /// Maneja excepciones para evitar fallos críticos en la aplicación.
        /// </summary>
        /// <returns>Una instancia de IDbConnection para consultas directas.</returns>
        public IDbConnection CreateConnection()
        {
            try
            {
                _connection = new NpgsqlConnection(_connectionString);
                return _connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error al crear la conexión a la base de datos: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Abre una conexión a la base de datos de forma segura.
        /// </summary>
        public void OpenConnection()
        {
            if (_connection == null)
            {
                _connection = CreateConnection();
            }

            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        /// <summary>
        /// Cierra y libera la conexión de la base de datos.
        /// </summary>
        public void Dispose()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }
    }
}
