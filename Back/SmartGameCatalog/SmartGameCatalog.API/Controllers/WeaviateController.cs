using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace SmartGameCatalog.API.Controllers
{
    /// <summary>
    /// Controlador para gestionar la comunicación con la base de datos vectorial Weaviate.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WeaviateController : ControllerBase
    {
        private readonly WeaviateService _weaviateService;

        /// <summary>
        /// Constructor que inyecta el servicio de Weaviate.
        /// </summary>
        /// <param name="weaviateService">Servicio de Weaviate.</param>
        public WeaviateController(WeaviateService weaviateService)
        {
            _weaviateService = weaviateService;
        }

        /// <summary>
        /// Agrega un nuevo videojuego a la base de datos Weaviate.
        /// </summary>
        /// <param name="request">Datos del videojuego.</param>
        /// <returns>Resultado de la operación.</returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddGame([FromBody] GameRequest request)
        {
            // Validación de campos obligatorios
            if (string.IsNullOrWhiteSpace(request.Title) ||
                string.IsNullOrWhiteSpace(request.Description) ||
                string.IsNullOrWhiteSpace(request.ReleaseDate) ||
                string.IsNullOrWhiteSpace(request.CoverUrl) ||
                string.IsNullOrWhiteSpace(request.Developer) ||
                string.IsNullOrWhiteSpace(request.Platform))
            {
                return BadRequest("All fields are required.");
            }

            var success = await _weaviateService.AddGameToWeaviateAsync(
                request.Title,
                request.Description,
                request.ReleaseDate,
                request.CoverUrl,
                request.Developer,
                request.Platform,
                request.Rating,
                request.CategoryId
            );

            return success ? Ok("Game added to Weaviate.") : BadRequest("Failed to add game.");
        }
        /// <summary>
        /// Verifica si la base de datos Weaviate está accesible.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> CheckWeaviate()
        {
            var isConnected = await _weaviateService.CheckWeaviateConnectionAsync();
            return isConnected ? Ok("Weaviate is running.") : StatusCode(500, "Weaviate is not reachable.");
        }

    }

    /// <summary>
    /// Representa la estructura de los datos requeridos para agregar un juego a Weaviate.
    /// </summary>
    public class GameRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ReleaseDate { get; set; }
        public string? CoverUrl { get; set; }
        public string? Developer { get; set; }
        public string? Platform { get; set; }
        public float Rating { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
