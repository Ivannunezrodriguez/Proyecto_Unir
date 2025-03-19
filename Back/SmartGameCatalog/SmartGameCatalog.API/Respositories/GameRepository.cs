using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Model;

public class GameRepository
{
    private readonly Database _database;

    public GameRepository(Database database)
    {
        _database = database;
    }

    /// <summary>
    /// Obtiene todos los juegos registrados.
    /// </summary>
    public async Task<IEnumerable<Game>> GetAll()
    {
        using var connection = _database.CreateConnection();
        return await connection.QueryAsync<Game>("SELECT * FROM Games;");
    }

    /// <summary>
    /// Obtiene un juego por su ID.
    /// </summary>
    public async Task<Game?> GetById(int id)
    {
        using var connection = _database.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Game>(
            "SELECT * FROM Games WHERE GameId = @Id;", new { Id = id }
        );
    }

    /// <summary>
    /// Agrega un nuevo juego a la base de datos.
    /// </summary>
    public async Task Create(Game game)
    {
        using var connection = _database.CreateConnection();
        await connection.ExecuteAsync(
            "INSERT INTO Games (IgdbId) VALUES (@IgdbId);",
            game
        );
    }

    /// <summary>
    /// Actualiza la información de un juego existente.
    /// </summary>
    public async Task Update(Game game)
    {
        using var connection = _database.CreateConnection();
        await connection.ExecuteAsync(
            "UPDATE Games SET IgdbId = @IgdbId WHERE GameId = @GameId;",
            game
        );
    }

    /// <summary>
    /// Elimina un juego de la base de datos.
    /// </summary>
    public async Task Delete(int id)
    {
        using var connection = _database.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Games WHERE GameId = @Id;", new { Id = id });
    }
}
