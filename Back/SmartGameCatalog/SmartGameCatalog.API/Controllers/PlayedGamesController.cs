using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Models;
using SmartGameCatalog.API.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayedGamesController : ControllerBase
    {
        private readonly PlayedGamesRepository _repository;

        public PlayedGamesController(PlayedGamesRepository repository)
        {
            _repository = repository;
        }

        // GET: api/playedgames/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<PlayedGame>>> GetPlayedGames(Guid userId)
        {
            var playedGames = await _repository.GetPlayedGamesAsync(userId);
            return Ok(playedGames);
        }

        // POST: api/playedgames
        [HttpPost]
        public async Task<ActionResult> MarkAsPlayed(PlayedGame playedGame)
        {
            playedGame.Id_Played_Game = Guid.NewGuid();
            playedGame.Played_At = DateTime.UtcNow;
            await _repository.AddPlayedGameAsync(playedGame);
            return CreatedAtAction(nameof(GetPlayedGames), new { id = playedGame.Id_Played_Game }, playedGame);
        }
    }
}
