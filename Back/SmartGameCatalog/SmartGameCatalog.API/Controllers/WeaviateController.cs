using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaviateController : ControllerBase
    {
        private readonly WeaviateService _weaviateService;

        public WeaviateController(WeaviateService weaviateService)
        {
            _weaviateService = weaviateService;
        }

       [HttpPost("add")]
public async Task<IActionResult> AddGame([FromBody] GameRequest request)
{
    // Validar que los valores requeridos no sean nulos o vacíos
    if (string.IsNullOrWhiteSpace(request.Title) || 
        string.IsNullOrWhiteSpace(request.Description) || 
        request.Genres == null || request.Genres.Count == 0 || 
        request.Platforms == null || request.Platforms.Count == 0 || 
        string.IsNullOrWhiteSpace(request.ReleaseDate) || 
        string.IsNullOrWhiteSpace(request.CoverUrl))
    {
        return BadRequest("All fields are required.");
    }

    var success = await _weaviateService.AddGameToWeaviateAsync(
        request.Title,
        request.Description,
        request.Genres,
        request.Platforms,
        request.ReleaseDate,
        request.CoverUrl
    );

    return success ? Ok("Game added to Weaviate.") : BadRequest("Failed to add game.");
}

    }

public class GameRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<string>? Genres { get; set; }
    public List<string>? Platforms { get; set; }
    public string? ReleaseDate { get; set; }
    public string? CoverUrl { get; set; }
}

}
