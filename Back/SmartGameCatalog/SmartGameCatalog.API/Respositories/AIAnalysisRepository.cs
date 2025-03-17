using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    /// <summary>
    /// Repositorio para la gestión de análisis de IA en la base de datos.
    /// </summary>
    public class AIAnalysisRepository
    {
        private readonly Database _database;

        public AIAnalysisRepository(Database database)
        {
            _database = database;
        }

        /// <summary>
        /// Obtiene la lista de todos los análisis de IA almacenados en la base de datos.
        /// </summary>
        /// <returns>Lista de análisis de IA.</returns>
        public async Task<IEnumerable<AIAnalysis>> GetAnalysesAsync()
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryAsync<AIAnalysis>(
                "SELECT id, user_id, videogame_id, ai_score, created_at FROM ai_analysis"
            );
        }

        /// <summary>
        /// Registra un nuevo análisis de IA en la base de datos.
        /// </summary>
        /// <param name="analysis">Objeto que representa el análisis a registrar.</param>
        /// <returns>Número de filas afectadas.</returns>
        public async Task<int> AddAnalysisAsync(AIAnalysis analysis)
        {
            using var connection = _database.CreateConnection();
            var query = @"
                INSERT INTO ai_analysis (id, user_id, videogame_id, ai_score, created_at) 
                VALUES (@Id_Analysis, @Id_User, @Id_VideoGame, @AI_Score, @Created_At)";
            return await connection.ExecuteAsync(query, analysis);
        }
    }
}
