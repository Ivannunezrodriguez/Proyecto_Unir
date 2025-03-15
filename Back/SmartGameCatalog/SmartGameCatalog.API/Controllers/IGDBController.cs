using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Services;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IGDBController : ControllerBase
    {
        private readonly IGDBService _igdbService;

        public IGDBController(IGDBService igdbService)
        {
            _igdbService = igdbService;
        }

        [HttpGet("search/{query}")]
        public async Task<IActionResult> SearchGames(string query)
        {
            var result = await _igdbService.SearchGamesAsync(query);
            return Ok(result);
        }
    }
}
