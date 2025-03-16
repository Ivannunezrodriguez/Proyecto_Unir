using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Services;
using System;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        private readonly WeaviateService _weaviateService;

        public RecommendationsController(WeaviateService weaviateService)
        {
            _weaviateService = weaviateService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetRecommendations(Guid userId)
        {
  var recommendations = await _weaviateService.GetRecommendationsAsync(userId.ToString());

            return Ok(recommendations);
        }
   [HttpGet("recommendations")]
public async Task<IActionResult> GetRecommendations([FromQuery] string searchQuery)
{
    var recommendations = await _weaviateService.GetRecommendationsAsync(searchQuery);
    return Ok(recommendations);
}



    }
}
