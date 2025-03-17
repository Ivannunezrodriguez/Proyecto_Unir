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
    public class VideoGamePlatformsController : ControllerBase
    {
        private readonly VideoGamePlatformRepository _platformRepository;

        public VideoGamePlatformsController(VideoGamePlatformRepository platformRepository)
        {
            _platformRepository = platformRepository;
        }

        [HttpGet("{videoGameId}")]
        public async Task<ActionResult<IEnumerable<string>>> GetPlatforms(Guid videoGameId)
        {
            var platforms = await _platformRepository.GetPlatformsByVideoGameIdAsync(videoGameId);
            return Ok(platforms);
        }

        [HttpPost]
        public async Task<IActionResult> AddPlatform([FromBody] VideoGamePlatform platform)
        {
            await _platformRepository.AddPlatformToVideoGameAsync(platform);
            return Ok("Platform added successfully.");
        }

        [HttpDelete("{videoGameId}/{platform}")]
        public async Task<IActionResult> RemovePlatform(Guid videoGameId, string platform)
        {
            await _platformRepository.RemovePlatformFromVideoGameAsync(videoGameId, platform);
            return Ok("Platform removed successfully.");
        }
    }
}
