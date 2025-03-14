using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    public class AIAnalysisRepository
    {
        private readonly Database _database;

        public AIAnalysisRepository(Database database)
        {
            _database = database;
        }

        public async Task<IEnumerable<AIAnalysis>> GetAnalysesAsync()
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryAsync<AIAnalysis>("SELECT * FROM AI_Analysis");
        }

        public async Task<int> AddAnalysisAsync(AIAnalysis analysis)
        {
            using var connection = _database.CreateConnection();
            var query = "INSERT INTO AI_Analysis (id_analysis, id_user, id_videogame, ai_score, generated_at) VALUES (@Id_Analysis, @Id_User, @Id_VideoGame, @AI_Score, @Generated_At)";
            return await connection.ExecuteAsync(query, analysis);
        }
    }
}
