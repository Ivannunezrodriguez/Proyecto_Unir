using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Model;

public class RatingRepository
{
    private readonly Database _database;

    public RatingRepository(Database database)
    {
        _database = database;
    }

    /// <summary>
    /// Obtiene todas las calificaciones registradas.
    /// </summary>
    public async Task<IEnumerable<Rating>> GetAll()
    {
        using var connection = _database.CreateConnection();
        return await connection.QueryAsync<Rating>("SELECT * FROM Ratings;");
    }

    /// <summary>
    /// Obtiene una calificación por su ID.
    /// </summary>
    public async Task<Rating?> GetById(int id)
    {
        using var connection = _database.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Rating>(
            "SELECT * FROM Ratings WHERE RatingId = @Id;", new { Id = id }
        );
    }

    /// <summary>
    /// Agrega una nueva calificación a la base de datos.
    /// </summary>
    public async Task Create(Rating rating)
    {
        using var connection = _database.CreateConnection();
        await connection.ExecuteAsync(
            "INSERT INTO Ratings (UserId, GameId, Score, Review, CreatedAt) VALUES (@UserId, @GameId, @Score, @Review, @CreatedAt);",
            rating
        );
    }

    /// <summary>
    /// Actualiza la información de una calificación existente.
    /// </summary>
    public async Task Update(Rating rating)
    {
        using var connection = _database.CreateConnection();
        await connection.ExecuteAsync(
            "UPDATE Ratings SET UserId = @UserId, GameId = @GameId, Score = @Score, Review = @Review WHERE RatingId = @RatingId;",
            rating
        );
    }

    /// <summary>
    /// Elimina una calificación de la base de datos.
    /// </summary>
    public async Task Delete(int id)
    {
        using var connection = _database.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Ratings WHERE RatingId = @Id;", new { Id = id });
    }
}
