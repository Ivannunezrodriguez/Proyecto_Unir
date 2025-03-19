using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Services;

namespace SmartGameCatalog.API.Controllers
{
    [Route("api/weaviate")]
    [ApiController]
    public class WeaviateController : ControllerBase
    {
        private readonly WeaviateService _weaviateService;

        public WeaviateController(WeaviateService weaviateService)
        {
            _weaviateService = weaviateService;
        }

        /// <summary>
        /// Obtiene recomendaciones de juegos para un usuario específico desde Weaviate.
        /// </summary>
        [HttpGet("recommendations/{userId}")]
        public async Task<IActionResult> GetUserRecommendations(int userId)
        {
            try
            {
                var recommendations = await _weaviateService.GetRecommendations(userId);
                return Ok(recommendations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener recomendaciones de Weaviate: {ex.Message}");
            }
        }
    }
}
