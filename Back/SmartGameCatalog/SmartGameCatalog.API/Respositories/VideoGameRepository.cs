using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    /// <summary>
    /// Repositorio para la gestión de videojuegos en la base de datos.
    /// </summary>
    public class VideoGameRepository
    {
        private readonly Database _database;

        public VideoGameRepository(Database database)
        {
            _database = database;
        }

        /// <summary>
        /// Obtiene la lista de todos los videojuegos almacenados en la base de datos.
        /// </summary>
        /// <returns>Lista de videojuegos.</returns>
        public async Task<IEnumerable<VideoGame>> GetVideoGamesAsync()
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryAsync<VideoGame>(
                "SELECT id AS Id_VideoGame, title, description, release_date, cover_url, developer, platform, rating, category_id FROM videogames"
            );
        }

        /// <summary>
        /// Agrega un nuevo videojuego a la base de datos.
        /// </summary>
        /// <param name="videoGame">Objeto que representa el videojuego a registrar.</param>
        /// <returns>Número de filas afectadas.</returns>
        public async Task<int> AddVideoGameAsync(VideoGame videoGame)
        {
            using var connection = _database.CreateConnection();
            var query = @"
                INSERT INTO videogames (id, title, description, release_date, cover_url, developer, platform, rating, category_id) 
                VALUES (@Id_VideoGame, @Title, @Description, @Release_Date, @CoverUrl, @Developer, @Platform, @Rating, @Category_id)";
            return await connection.ExecuteAsync(query, videoGame);
        }
    }
}
