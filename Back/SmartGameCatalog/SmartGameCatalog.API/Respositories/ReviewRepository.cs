using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    public class ReviewRepository
    {
        private readonly Database _database;

        public ReviewRepository(Database database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Review>> GetReviewsAsync()
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryAsync<Review>("SELECT * FROM Review");
        }

        public async Task<int> AddReviewAsync(Review review)
        {
            using var connection = _database.CreateConnection();
            var query = "INSERT INTO Review (id_review, id_user, id_videogame, comment, rating, date) VALUES (@Id_Review, @Id_User, @Id_VideoGame, @Comment, @Rating, @Date)";
            return await connection.ExecuteAsync(query, review);
        }
    }
}
