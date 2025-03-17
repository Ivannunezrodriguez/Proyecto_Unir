using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Models;
using SmartGameCatalog.API.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    // Define la ruta base de la API para este controlador como "api/AIAnalyses".
    [Route("api/[controller]")]
    [ApiController]
    public class AIAnalysesController : ControllerBase
    {
        // Repositorio que maneja la lógica de acceso a datos para los análisis de IA.
        private readonly AIAnalysisRepository _repository;

        // Constructor que inyecta el repositorio de análisis de IA.
        public AIAnalysesController(AIAnalysisRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene la lista de todos los análisis de IA disponibles.
        /// </summary>
        /// <returns>Una lista de objetos AIAnalysis.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AIAnalysis>>> GetAnalyses()
        {
            var analyses = await _repository.GetAnalysesAsync();
            return Ok(analyses);
        }

        /// <summary>
        /// Crea un nuevo análisis de IA y lo almacena en la base de datos.
        /// </summary>
        /// <param name="analysis">El objeto AIAnalysis a crear.</param>
        /// <returns>Un resultado con el estado de la operación.</returns>
        [HttpPost]
        public async Task<ActionResult> CreateAnalysis(AIAnalysis analysis)
        {
            // Genera un nuevo identificador único para el análisis.
            analysis.Id_Analysis = Guid.NewGuid();
                  // Agrega el análisis a la base de datos.
            await _repository.AddAnalysisAsync(analysis);
            // Devuelve una respuesta 201 Created con la ubicación del nuevo recurso.
            return CreatedAtAction(nameof(GetAnalyses), new { id = analysis.Id_Analysis }, analysis);
        }
    }
}
