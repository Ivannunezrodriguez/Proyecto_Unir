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
    public class RecommendationsController : ControllerBase
    {
        private readonly RecommendationRepository _repository;

        public RecommendationsController(RecommendationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recommendation>>> GetRecommendations()
        {
            var recommendations = await _repository.GetRecommendationsAsync();
            return Ok(recommendations);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRecommendation(Recommendation recommendation)
        {
            recommendation.Id_Recommendation = Guid.NewGuid();
            recommendation.Date = DateTime.UtcNow;
            await _repository.AddRecommendationAsync(recommendation);
            return CreatedAtAction(nameof(GetRecommendations), new { id = recommendation.Id_Recommendation }, recommendation);
        }
    }
}
