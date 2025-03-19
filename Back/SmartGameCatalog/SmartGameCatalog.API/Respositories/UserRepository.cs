using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Model;

public class UserRepository
{
    private readonly Database _database;

    public UserRepository(Database database)
    {
        _database = database;
    }

    /// <summary>
    /// Obtiene todos los usuarios registrados.
    /// </summary>
    public async Task<IEnumerable<User>> GetAll()
    {
        using var connection = _database.CreateConnection();
        return await connection.QueryAsync<User>("SELECT * FROM Users;");
    }

    /// <summary>
    /// Obtiene un usuario por su ID.
    /// </summary>
    public async Task<User?> GetById(int id)
    {
        using var connection = _database.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE UserId = @Id;", new { Id = id }
        );
    }

    /// <summary>
    /// Agrega un nuevo usuario a la base de datos.
    /// </summary>
    public async Task Create(User user)
    {
        using var connection = _database.CreateConnection();
        await connection.ExecuteAsync(
            "INSERT INTO Users (Username, Email, Password, Role, CreatedAt) VALUES (@Username, @Email, @Password, @Role, @CreatedAt);",
            user
        );
    }

    /// <summary>
    /// Actualiza la información de un usuario existente.
    /// </summary>
    public async Task Update(User user)
    {
        using var connection = _database.CreateConnection();
        await connection.ExecuteAsync(
            "UPDATE Users SET Username = @Username, Email = @Email, Password = @Password, Role = @Role WHERE UserId = @UserId;",
            user
        );
    }

    /// <summary>
    /// Elimina un usuario de la base de datos.
    /// </summary>
    public async Task Delete(int id)
    {
        using var connection = _database.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Users WHERE UserId = @Id;", new { Id = id });
    }
}
