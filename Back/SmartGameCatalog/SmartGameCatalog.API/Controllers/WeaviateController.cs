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

        if (recommendations == null || !recommendations.Any())
        {
            Console.WriteLine($"⚠️ No se encontraron recomendaciones para el usuario {userId}.");
            return NotFound(new { message = "No se encontraron recomendaciones para este usuario." });
        }

        Console.WriteLine($"✅ Recomendaciones encontradas para el usuario {userId}: {System.Text.Json.JsonSerializer.Serialize(recommendations)}");
        return Ok(recommendations);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error en GetUserRecommendations: {ex.Message}");
        return StatusCode(500, new { error = ex.Message });
    }
}




    }
}
