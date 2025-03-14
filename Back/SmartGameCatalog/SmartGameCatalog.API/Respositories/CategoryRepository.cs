using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    public class CategoryRepository
    {
        private readonly Database _database;

        public CategoryRepository(Database database)
        {
            _database = database;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryAsync<Category>("SELECT * FROM Category");
        }

        public async Task<int> AddCategoryAsync(Category category)
        {
            using var connection = _database.CreateConnection();
            var query = "INSERT INTO Category (id_category, name) VALUES (@Id_Category, @Name)";
            return await connection.ExecuteAsync(query, category);
        }
    }
}
