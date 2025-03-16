using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    public class PlayedGamesRepository
    {
        private readonly Database _database;

        public PlayedGamesRepository(Database database)
        {
            _database = database;
        }

        public async Task<IEnumerable<PlayedGame>> GetPlayedGamesAsync(Guid userId)
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryAsync<PlayedGame>(
                "SELECT * FROM PlayedGames WHERE id_user = @UserId",
                new { UserId = userId }
            );
        }

        public async Task<int> AddPlayedGameAsync(PlayedGame playedGame)
        {
            using var connection = _database.CreateConnection();
            var query = "INSERT INTO PlayedGames (id_played_game, id_user, id_videogame, played_at) VALUES (@Id_Played_Game, @Id_User, @Id_VideoGame, @Played_At)";
            return await connection.ExecuteAsync(query, playedGame);
        }
    }
}
