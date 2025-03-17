using Dapper;
using SmartGameCatalog.API.Data;
using SmartGameCatalog.API.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Repositories
{
    /// <summary>
    /// Repositorio para la gestión de los juegos jugados por los usuarios en la base de datos.
    /// </summary>
    public class PlayedGamesRepository
    {
        private readonly Database _database;

        /// <summary>
        /// Constructor que inicializa la conexión con la base de datos.
        /// </summary>
        /// <param name="database">Instancia de la base de datos.</param>
        public PlayedGamesRepository(Database database)
        {
            _database = database;
        }

        /// <summary>
        /// Obtiene la lista de juegos jugados por un usuario específico.
        /// </summary>
        /// <param name="userId">Identificador único del usuario.</param>
        /// <returns>Lista de juegos jugados por el usuario.</returns>
        public async Task<IEnumerable<PlayedGame>> GetPlayedGamesAsync(Guid userId)
        {
            using var connection = _database.CreateConnection();
            return await connection.QueryAsync<PlayedGame>(
                "SELECT * FROM PlayedGames WHERE id_user = @UserId",
                new { UserId = userId }
            );
        }

        /// <summary>
        /// Registra un nuevo juego jugado en la base de datos.
        /// </summary>
        /// <param name="playedGame">Objeto que representa el juego jugado.</param>
        /// <returns>Número de filas afectadas.</returns>
        public async Task<int> AddPlayedGameAsync(PlayedGame playedGame)
        {
            using var connection = _database.CreateConnection();
            var query = "INSERT INTO PlayedGames (id_played_game, id_user, id_videogame, played_at) VALUES (@Id_Played_Game, @Id_User, @Id_VideoGame, @Played_At)";
            return await connection.ExecuteAsync(query, playedGame);
        }
    }
}