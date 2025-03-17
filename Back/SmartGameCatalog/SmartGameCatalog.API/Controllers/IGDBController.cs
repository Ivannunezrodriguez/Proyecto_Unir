using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Services;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    // Define la ruta base de la API para este controlador como "api/IGDB".
    [Route("api/[controller]")]
    [ApiController]
    public class IGDBController : ControllerBase
    {
        // Servicio que maneja la comunicación con la API de IGDB.
        private readonly IGDBService _igdbService;

        // Constructor que inyecta el servicio de IGDB.
        public IGDBController(IGDBService igdbService)
        {
            _igdbService = igdbService;
        }

        /// <summary>
        /// Busca videojuegos en la base de datos de IGDB según el término de búsqueda proporcionado.
        /// </summary>
        /// <param name="query">El término de búsqueda.</param>
        /// <returns>Una lista de videojuegos que coinciden con la consulta.</returns>
        [HttpGet("search/{query}")]
        public async Task<IActionResult> SearchGames(string query)
        {
            var result = await _igdbService.SearchGamesAsync(query);
            return Ok(result);
        }
    }
}
