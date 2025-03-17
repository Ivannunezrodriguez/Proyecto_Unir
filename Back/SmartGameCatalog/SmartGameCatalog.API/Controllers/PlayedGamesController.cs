using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Models;
using SmartGameCatalog.API.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    // Define la ruta base de la API para este controlador como "api/PlayedGames".
    [Route("api/[controller]")]
    [ApiController]
    public class PlayedGamesController : ControllerBase
    {
        // Repositorio que maneja la lógica de acceso a datos para los juegos jugados.
        private readonly PlayedGamesRepository _repository;

        // Constructor que inyecta el repositorio de juegos jugados.
        public PlayedGamesController(PlayedGamesRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene la lista de juegos jugados por un usuario específico.
        /// </summary>
        /// <param name="userId">El identificador único del usuario.</param>
        /// <returns>Una lista de juegos jugados.</returns>
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<PlayedGame>>> GetPlayedGames(Guid userId)
        {
            var playedGames = await _repository.GetPlayedGamesAsync(userId);
            return Ok(playedGames);
        }

        /// <summary>
        /// Marca un juego como jugado y lo almacena en la base de datos.
        /// </summary>
        /// <param name="playedGame">El objeto PlayedGame a registrar.</param>
        /// <returns>Un resultado con el estado de la operación.</returns>
        [HttpPost]
        public async Task<ActionResult> MarkAsPlayed(PlayedGame playedGame)
        {
            // Genera un nuevo identificador único para el juego jugado.
            playedGame.Id_Played_Game = Guid.NewGuid();
            // Establece la fecha y hora en que se jugó el juego en UTC.
            playedGame.Played_At = DateTime.UtcNow;
            // Agrega el juego jugado a la base de datos.
            await _repository.AddPlayedGameAsync(playedGame);
            // Devuelve una respuesta 201 Created con la ubicación del nuevo recurso.
            return CreatedAtAction(nameof(GetPlayedGames), new { id = playedGame.Id_Played_Game }, playedGame);
        }
    }
}
