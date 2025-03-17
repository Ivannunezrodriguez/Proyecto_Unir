using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Models;
using SmartGameCatalog.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    // Define la ruta base de la API para este controlador como "api/VideoGames".
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGamesController : ControllerBase
    {
        // Repositorio que maneja la lógica de acceso a datos para los videojuegos.
        private readonly VideoGameRepository _repository;

        // Constructor que inyecta el repositorio de videojuegos.
        public VideoGamesController(VideoGameRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene la lista de todos los videojuegos disponibles.
        /// </summary>
        /// <returns>Una lista de objetos VideoGame.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoGame>>> GetVideoGames()
        {
            var videoGames = await _repository.GetVideoGamesAsync();
            return Ok(videoGames);
        }

        /// <summary>
        /// Crea un nuevo videojuego y lo almacena en la base de datos.
        /// </summary>
        /// <param name="videoGame">El objeto VideoGame a registrar.</param>
        /// <returns>Un resultado con el estado de la operación.</returns>
        [HttpPost]
        public async Task<ActionResult> CreateVideoGame(VideoGame videoGame)
        {
            // Genera un nuevo identificador único para el videojuego.
            videoGame.Id_VideoGame = Guid.NewGuid();
            // Agrega el videojuego a la base de datos.
            await _repository.AddVideoGameAsync(videoGame);
            // Devuelve una respuesta 201 Created con la ubicación del nuevo recurso.
            return CreatedAtAction(nameof(GetVideoGames), new { id = videoGame.Id_VideoGame }, videoGame);
        }
    }
}
