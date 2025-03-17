using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Services;
using System;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    // Define la ruta base de la API para este controlador como "api/Recommendations".
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        // Servicio que maneja la lógica de generación de recomendaciones con Weaviate.
        private readonly WeaviateService _weaviateService;

        // Constructor que inyecta el servicio de recomendaciones.
        public RecommendationsController(WeaviateService weaviateService)
        {
            _weaviateService = weaviateService;
        }

        /// <summary>
        /// Obtiene recomendaciones de juegos personalizadas para un usuario específico.
        /// </summary>
        /// <param name="userId">El identificador único del usuario.</param>
        /// <returns>Una lista de recomendaciones de juegos.</returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetRecommendations(Guid userId)
        {
            var recommendations = await _weaviateService.GetRecommendationsAsync(userId.ToString());
            return Ok(recommendations);
        }

        /// <summary>
        /// Obtiene recomendaciones de juegos basadas en una consulta de búsqueda.
        /// </summary>
        /// <param name="searchQuery">El término de búsqueda para generar recomendaciones.</param>
        /// <returns>Una lista de juegos recomendados que coinciden con la consulta.</returns>
        [HttpGet("recommendations")]
        public async Task<IActionResult> GetRecommendations([FromQuery] string searchQuery)
        {
            var recommendations = await _weaviateService.GetRecommendationsAsync(searchQuery);
            return Ok(recommendations);
        }
    }
}
