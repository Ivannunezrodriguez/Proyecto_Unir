using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Repositories;
using SmartGameCatalog.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameGenresController : ControllerBase
    {
        private readonly VideoGameGenreRepository _genreRepository;

        public VideoGameGenresController(VideoGameGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet("{videoGameId}")]
        public async Task<ActionResult<IEnumerable<string>>> GetGenres(Guid videoGameId)
        {
            var genres = await _genreRepository.GetGenresByVideoGameIdAsync(videoGameId);
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody] VideoGameGenre genre)
        {
            await _genreRepository.AddGenreToVideoGameAsync(genre);
            return Ok("Genre added successfully.");
        }

        [HttpDelete("{videoGameId}/{genre}")]
        public async Task<IActionResult> RemoveGenre(Guid videoGameId, string genre)
        {
            await _genreRepository.RemoveGenreFromVideoGameAsync(videoGameId, genre);
            return Ok("Genre removed successfully.");
        }
    }
}
