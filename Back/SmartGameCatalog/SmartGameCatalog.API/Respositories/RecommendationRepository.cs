using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    /// <summary>
    /// Repositorio para la gestión de recomendaciones en la base de datos.
    /// </summary>
    public class RecommendationRepository
    {
        private readonly Database _database;

        public RecommendationRepository(Database database)
        {
            _database = database;
        }

        /// <summary>
        /// Obtiene la lista de todas las recomendaciones registradas en la base de datos.
        /// </summary>
        /// <returns>Lista de recomendaciones generadas para los usuarios.</returns>
        public async Task<IEnumerable<Recommendation>> GetRecommendationsAsync()
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryAsync<Recommendation>(
                "SELECT id, user_id, recommended_videogame_id, reason, created_at FROM recommendations"
            );
        }

        /// <summary>
        /// Registra una nueva recomendación en la base de datos.
        /// </summary>
        /// <param name="recommendation">Objeto que representa la recomendación a registrar.</param>
        /// <returns>Número de filas afectadas.</returns>
        public async Task<int> AddRecommendationAsync(Recommendation recommendation)
        {
            using var connection = _database.CreateConnection();
            var query = @"
                INSERT INTO recommendations (id, user_id, recommended_videogame_id, reason, created_at) 
                VALUES (@Id_Recommendation, @Id_User, @Id_Recommended_VideoGame, @Reason, @Created_At)";
            return await connection.ExecuteAsync(query, recommendation);
        }
    }
}
