using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    /// <summary>
    /// Repositorio para la gestión de categorías en la base de datos.
    /// </summary>
    public class CategoryRepository
    {
        private readonly Database _database;

        /// <summary>
        /// Constructor que inicializa la conexión con la base de datos.
        /// </summary>
        /// <param name="database">Instancia de la base de datos.</param>
        public CategoryRepository(Database database)
        {
            _database = database;
        }

        /// <summary>
        /// Obtiene todas las categorías almacenadas en la base de datos.
        /// </summary>
        /// <returns>Lista de categorías.</returns>
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryAsync<Category>("SELECT * FROM Category");
        }

        /// <summary>
        /// Agrega una nueva categoría a la base de datos.
        /// </summary>
        /// <param name="category">Objeto de categoría a insertar.</param>
        /// <returns>Número de filas afectadas.</returns>
        public async Task<int> AddCategoryAsync(Category category)
        {
            using var connection = _database.CreateConnection();
            var query = "INSERT INTO Category (id_category, name) VALUES (@Id_Category, @Name)";
            return await connection.ExecuteAsync(query, category);
        }
    }
}