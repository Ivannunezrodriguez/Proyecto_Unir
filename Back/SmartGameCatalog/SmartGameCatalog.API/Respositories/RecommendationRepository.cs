using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    public class RecommendationRepository
    {
        private readonly Database _database;

        public RecommendationRepository(Database database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Recommendation>> GetRecommendationsAsync()
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryAsync<Recommendation>("SELECT * FROM Recommendation");
        }

        public async Task<int> AddRecommendationAsync(Recommendation recommendation)
        {
            using var connection = _database.CreateConnection();
            var query = "INSERT INTO Recommendation (id_recommendation, id_user, id_videogame, reason, date) VALUES (@Id_Recommendation, @Id_User, @Id_VideoGame, @Reason, @Date)";
            return await connection.ExecuteAsync(query, recommendation);
        }
    }
}
