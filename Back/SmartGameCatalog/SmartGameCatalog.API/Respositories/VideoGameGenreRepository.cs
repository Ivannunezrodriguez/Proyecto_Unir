using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    /// <summary>
    /// Repositorio para la gestión de géneros de videojuegos.
    /// </summary>
    public class VideoGameGenreRepository
    {
        private readonly Database _database;

        public VideoGameGenreRepository(Database database)
        {
            _database = database;
        }

        /// <summary>
        /// Obtiene los géneros de un videojuego específico.
        /// </summary>
        public async Task<IEnumerable<string>> GetGenresByVideoGameIdAsync(Guid videoGameId)
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryAsync<string>(
                "SELECT genre FROM videogame_genres WHERE videogame_id = @VideoGameId",
                new { VideoGameId = videoGameId }
            );
        }

        /// <summary>
        /// Agrega un género a un videojuego.
        /// </summary>
        public async Task<int> AddGenreToVideoGameAsync(VideoGameGenre videoGameGenre)
        {
            using var connection = _database.CreateConnection();
            return await connection.ExecuteAsync(
                "INSERT INTO videogame_genres (videogame_id, genre) VALUES (@Id_VideoGame, @Genre)",
                videoGameGenre
            );
        }

        /// <summary>
        /// Elimina un género de un videojuego.
        /// </summary>
        public async Task<int> RemoveGenreFromVideoGameAsync(Guid videoGameId, string genre)
        {
            using var connection = _database.CreateConnection();
            return await connection.ExecuteAsync(
                "DELETE FROM videogame_genres WHERE videogame_id = @VideoGameId AND genre = @Genre",
                new { VideoGameId = videoGameId, Genre = genre }
            );
        }
    }
}
