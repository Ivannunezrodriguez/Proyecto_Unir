using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    public class UserRepository
    {
        private readonly Database _database;

        public UserRepository(Database database)
        {
            _database = database;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryAsync<User>("SELECT * FROM Users");
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE id_user = @Id", new { Id = id });
        }

        public async Task<int> AddUserAsync(User user)
        {
            using var connection = _database.CreateConnection();
            var query = "INSERT INTO Users (id_user, name, email, password, registration_date) VALUES (@Id_User, @Name, @Email, @Password, @Registration_Date)";
            return await connection.ExecuteAsync(query, user);
        }
    }
}
