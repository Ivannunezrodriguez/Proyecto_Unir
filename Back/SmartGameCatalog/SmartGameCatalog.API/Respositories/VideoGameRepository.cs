using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    public class VideoGameRepository
    {
        private readonly Database _database;

        public VideoGameRepository(Database database)
        {
            _database = database;
        }

        public async Task<IEnumerable<VideoGame>> GetVideoGamesAsync()
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryAsync<VideoGame>("SELECT * FROM VideoGame");
        }

        public async Task<int> AddVideoGameAsync(VideoGame videoGame)
        {
            using var connection = _database.CreateConnection();
            var query = "INSERT INTO VideoGame (id_videogame, title, description, release_date, developer, platform, rating, id_category) VALUES (@Id_VideoGame, @Title, @Description, @Release_Date, @Developer, @Platform, @Rating, @Id_Category)";
            return await connection.ExecuteAsync(query, videoGame);
        }
    }
}
