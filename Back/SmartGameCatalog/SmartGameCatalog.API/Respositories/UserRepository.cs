using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    /// <summary>
    /// Repositorio para la gestión de usuarios en la base de datos.
    /// </summary>
    public class UserRepository
    {
        private readonly Database _database;

        /// <summary>
        /// Constructor que inicializa la conexión con la base de datos.
        /// </summary>
        /// <param name="database">Instancia de la base de datos.</param>
        public UserRepository(Database database)
        {
            _database = database;
        }

        /// <summary>
        /// Obtiene la lista de todos los usuarios registrados en la base de datos.
        /// </summary>
        /// <returns>Lista de usuarios.</returns>
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryAsync<User>("SELECT * FROM Users");
        }

        /// <summary>
        /// Obtiene un usuario específico por su identificador único.
        /// </summary>
        /// <param name="id">Identificador único del usuario.</param>
        /// <returns>El objeto User si se encuentra, de lo contrario devuelve null.</returns>
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE id_user = @Id", new { Id = id });
        }

        /// <summary>
        /// Agrega un nuevo usuario a la base de datos.
        /// </summary>
        /// <param name="user">Objeto que representa al usuario a registrar.</param>
        /// <returns>Número de filas afectadas.</returns>
        public async Task<int> AddUserAsync(User user)
        {
            using var connection = _database.CreateConnection();
            var query = "INSERT INTO Users (id_user, name, email, password, registration_date) VALUES (@Id_User, @Name, @Email, @Password, @Registration_Date)";
            return await connection.ExecuteAsync(query, user);
        }
    }
}
