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
    public class AIAnalysesController : ControllerBase
    {
        private readonly AIAnalysisRepository _repository;

        public AIAnalysesController(AIAnalysisRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AIAnalysis>>> GetAnalyses()
        {
            var analyses = await _repository.GetAnalysesAsync();
            return Ok(analyses);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAnalysis(AIAnalysis analysis)
        {
            analysis.Id_Analysis = Guid.NewGuid();
            analysis.Generated_At = DateTime.UtcNow;
            await _repository.AddAnalysisAsync(analysis);
            return CreatedAtAction(nameof(GetAnalyses), new { id = analysis.Id_Analysis }, analysis);
        }
    }
}
