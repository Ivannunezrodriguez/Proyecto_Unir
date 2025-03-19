using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Model;

public class FavoriteRepository
{
    private readonly Database _database;

    public FavoriteRepository(Database database)
    {
        _database = database;
    }

    /// <summary>
    /// Obtiene todos los juegos favoritos.
    /// </summary>
    public async Task<IEnumerable<Favorite>> GetAll()
    {
        using var connection = _database.CreateConnection();
        return await connection.QueryAsync<Favorite>("SELECT * FROM Favorites;");
    }

    /// <summary>
    /// Obtiene un juego favorito por su ID.
    /// </summary>
    public async Task<Favorite?> GetById(int id)
    {
        using var connection = _database.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Favorite>(
            "SELECT * FROM Favorites WHERE FavoriteId = @Id;", new { Id = id }
        );
    }

    /// <summary>
    /// Agrega un juego a favoritos.
    /// </summary>
    public async Task Create(Favorite favorite)
    {
        using var connection = _database.CreateConnection();
        await connection.ExecuteAsync(
            "INSERT INTO Favorites (UserId, GameId, AddedAt) VALUES (@UserId, @GameId, @AddedAt);",
            favorite
        );
    }

    /// <summary>
    /// Actualiza un juego favorito existente.
    /// </summary>
    public async Task Update(Favorite favorite)
    {
        using var connection = _database.CreateConnection();
        await connection.ExecuteAsync(
            "UPDATE Favorites SET UserId = @UserId, GameId = @GameId, AddedAt = @AddedAt WHERE FavoriteId = @FavoriteId;",
            favorite
        );
    }

    /// <summary>
    /// Elimina un juego de la lista de favoritos.
    /// </summary>
    public async Task Delete(int id)
    {
        using var connection = _database.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Favorites WHERE FavoriteId = @Id;", new { Id = id });
    }
}
