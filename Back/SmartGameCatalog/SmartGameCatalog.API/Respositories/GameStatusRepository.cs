using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Model;

public class GameStatusRepository
{
    private readonly Database _database;

    public GameStatusRepository(Database database)
    {
        _database = database;
    }

    /// <summary>
    /// Obtiene todos los registros de estados de juegos.
    /// </summary>
    public async Task<IEnumerable<GameStatus>> GetAll()
    {
        using var connection = _database.CreateConnection();
        return await connection.QueryAsync<GameStatus>("SELECT * FROM GameStatus;");
    }

    /// <summary>
    /// Obtiene un estado de juego por su ID.
    /// </summary>
    public async Task<GameStatus?> GetById(int id)
    {
        using var connection = _database.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<GameStatus>(
            "SELECT * FROM GameStatus WHERE StatusId = @Id;", new { Id = id }
        );
    }

    /// <summary>
    /// Agrega un nuevo estado de juego a la base de datos.
    /// </summary>
 public async Task SetStatus(int userId, int gameId, string status)
{
    var query = @"
        INSERT INTO GameStatus (UserId, GameId, Status, UpdatedAt)
        VALUES (@UserId, @GameId, @Status, NOW())
        ON CONFLICT (UserId, GameId) DO UPDATE
        SET Status = EXCLUDED.Status, UpdatedAt = NOW();
    ";

    using var connection = _database.CreateConnection();
    await connection.ExecuteAsync(query, new { UserId = userId, GameId = gameId, Status = status });
}


    /// <summary>
    /// Actualiza la información de un estado de juego existente.
    /// </summary>
    public async Task Update(GameStatus gameStatus)
    {
        using var connection = _database.CreateConnection();
        await connection.ExecuteAsync(
            "UPDATE GameStatus SET UserId = @UserId, GameId = @GameId, Status = @Status, UpdatedAt = @UpdatedAt WHERE StatusId = @StatusId;",
            gameStatus
        );
    }

    /// <summary>
    /// Elimina un estado de juego de la base de datos.
    /// </summary>
    public async Task Delete(int id)
    {
        using var connection = _database.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM GameStatus WHERE StatusId = @Id;", new { Id = id });
    }
}
