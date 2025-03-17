using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    /// <summary>
    /// Repositorio para la gestión de plataformas de videojuegos.
    /// </summary>
    public class VideoGamePlatformRepository
    {
        private readonly Database _database;

        public VideoGamePlatformRepository(Database database)
        {
            _database = database;
        }

        /// <summary>
        /// Obtiene las plataformas de un videojuego específico.
        /// </summary>
        public async Task<IEnumerable<string>> GetPlatformsByVideoGameIdAsync(Guid videoGameId)
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryAsync<string>(
                "SELECT platform FROM videogame_platforms WHERE videogame_id = @VideoGameId",
                new { VideoGameId = videoGameId }
            );
        }

        /// <summary>
        /// Agrega una plataforma a un videojuego.
        /// </summary>
        public async Task<int> AddPlatformToVideoGameAsync(VideoGamePlatform videoGamePlatform)
        {
            using var connection = _database.CreateConnection();
            return await connection.ExecuteAsync(
                "INSERT INTO videogame_platforms (videogame_id, platform) VALUES (@Id_VideoGame, @Platform)",
                videoGamePlatform
            );
        }

        /// <summary>
        /// Elimina una plataforma de un videojuego.
        /// </summary>
        public async Task<int> RemovePlatformFromVideoGameAsync(Guid videoGameId, string platform)
        {
            using var connection = _database.CreateConnection();
            return await connection.ExecuteAsync(
                "DELETE FROM videogame_platforms WHERE videogame_id = @VideoGameId AND platform = @Platform",
                new { VideoGameId = videoGameId, Platform = platform }
            );
        }
    }
}
