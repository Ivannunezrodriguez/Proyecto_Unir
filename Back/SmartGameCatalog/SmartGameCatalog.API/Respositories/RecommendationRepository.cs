using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Model;

public class RecommendationRepository
{
    private readonly Database _database;

    public RecommendationRepository(Database database)
    {
        _database = database;
    }

    /// <summary>
    /// Obtiene todas las recomendaciones registradas.
    /// </summary>
    public async Task<IEnumerable<Recommendation>> GetAll()
    {
        using var connection = _database.CreateConnection();
        return await connection.QueryAsync<Recommendation>("SELECT * FROM Recommendations;");
    }

    /// <summary>
    /// Obtiene una recomendación por su ID.
    /// </summary>
    public async Task<Recommendation?> GetById(int id)
    {
        using var connection = _database.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Recommendation>(
            "SELECT * FROM Recommendations WHERE RecommendationId = @Id;", new { Id = id }
        );
    }

    /// <summary>
    /// Agrega una nueva recomendación a la base de datos.
    /// </summary>
    public async Task Create(Recommendation recommendation)
    {
        using var connection = _database.CreateConnection();
        await connection.ExecuteAsync(
            "INSERT INTO Recommendations (UserId, GameId, Reason, CreatedAt) VALUES (@UserId, @GameId, @Reason, @CreatedAt);",
            recommendation
        );
    }

    /// <summary>
    /// Actualiza la información de una recomendación existente.
    /// </summary>
    public async Task Update(Recommendation recommendation)
    {
        using var connection = _database.CreateConnection();
        await connection.ExecuteAsync(
            "UPDATE Recommendations SET UserId = @UserId, GameId = @GameId, Reason = @Reason, CreatedAt = @CreatedAt WHERE RecommendationId = @RecommendationId;",
            recommendation
        );
    }

    /// <summary>
    /// Elimina una recomendación de la base de datos.
    /// </summary>
    public async Task Delete(int id)
    {
        using var connection = _database.CreateConnection();
        await connection.ExecuteAsync("DELETE FROM Recommendations WHERE RecommendationId = @Id;", new { Id = id });
    }
}
