using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    /// <summary>
    /// Repositorio para la gestión de reseñas de videojuegos en la base de datos.
    /// </summary>
    public class ReviewRepository
    {
        private readonly Database _database;

        /// <summary>
        /// Constructor que inicializa la conexión con la base de datos.
        /// </summary>
        /// <param name="database">Instancia de la base de datos.</param>
        public ReviewRepository(Database database)
        {
            _database = database;
        }

        /// <summary>
        /// Obtiene la lista de todas las reseñas registradas en la base de datos.
        /// </summary>
        /// <returns>Lista de reseñas realizadas por los usuarios.</returns>
        public async Task<IEnumerable<Review>> GetReviewsAsync()
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryAsync<Review>("SELECT * FROM Review");
        }

        /// <summary>
        /// Registra una nueva reseña en la base de datos.
        /// </summary>
        /// <param name="review">Objeto que representa la reseña a registrar.</param>
        /// <returns>Número de filas afectadas.</returns>
        public async Task<int> AddReviewAsync(Review review)
        {
            using var connection = _database.CreateConnection();
            var query = "INSERT INTO Review (id_review, id_user, id_videogame, comment, rating, date) VALUES (@Id_Review, @Id_User, @Id_VideoGame, @Comment, @Rating, @Date)";
            return await connection.ExecuteAsync(query, review);
        }
    }
}