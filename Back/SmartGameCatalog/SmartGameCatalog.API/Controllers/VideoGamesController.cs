using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Models;
using SmartGameCatalog.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGamesController : ControllerBase
    {
        private readonly VideoGameRepository _repository;

        public VideoGamesController(VideoGameRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoGame>>> GetVideoGames()
        {
            var videoGames = await _repository.GetVideoGamesAsync();
            return Ok(videoGames);
        }

        [HttpPost]
        public async Task<ActionResult> CreateVideoGame(VideoGame videoGame)
        {
            videoGame.Id_VideoGame = Guid.NewGuid();
            await _repository.AddVideoGameAsync(videoGame);
            return CreatedAtAction(nameof(GetVideoGames), new { id = videoGame.Id_VideoGame }, videoGame);
        }
    }
}
