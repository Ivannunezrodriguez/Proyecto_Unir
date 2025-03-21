using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Model;
using SmartGameCatalog.API.Controllers;

public class UserRepository
{
    private readonly Database _database;

    public UserRepository(Database database)
    {
        _database = database;
    }
    /// <summary>
    /// comprueba que el mail y user no esta repetido en la base de datos
    /// </summary>
    public async Task<bool> EmailOrUsernameExists(string email, string username)
    {
        using var connection = _database.CreateConnection();
        var query = "SELECT COUNT(1) FROM Users WHERE Email = @Email OR Username = @Username";
        var count = await connection.ExecuteScalarAsync<int>(query, new { Email = email, Username = username });
        return count > 0;
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

        if (await EmailOrUsernameExists(user.Email, user.Username))
        {
            throw new Exception("El email o el nombre de usuario ya están en uso. Intente con otro.");
        }

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
