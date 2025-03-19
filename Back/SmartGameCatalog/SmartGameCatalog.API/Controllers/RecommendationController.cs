using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Model;

namespace SmartGameCatalog.API.Controllers
{
    [Route("api/recommendations")]
    [ApiController]
    public class RecommendationsController : ControllerBase
    {
        private readonly RecommendationRepository _repository;

        public RecommendationsController(RecommendationRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene la lista de todas las recomendaciones registradas.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recommendation>>> GetAll()
        {
            try
            {
                var recommendations = await _repository.GetAll();
                return Ok(recommendations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene una recomendación por ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Recommendation>> GetById(int id)
        {
            try
            {
                var recommendation = await _repository.GetById(id);
                if (recommendation == null)
                {
                    return NotFound($"Recomendación con ID {id} no encontrada.");
                }
                return Ok(recommendation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Agrega una nueva recomendación a la base de datos.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Recommendation>> Create(Recommendation recommendation)
        {
            try
            {
                if (recommendation == null)
                {
                    return BadRequest("Los datos de la recomendación no pueden ser nulos.");
                }

                await _repository.Create(recommendation);
                return CreatedAtAction(nameof(GetById), new { id = recommendation.RecommendationId }, recommendation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la recomendación: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza la información de una recomendación existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Recommendation recommendation)
        {
            try
            {
                if (recommendation == null)
                {
                    return BadRequest("Los datos de la recomendación no pueden ser nulos.");
                }

                if (id != recommendation.RecommendationId)
                {
                    return BadRequest("El ID en la URL no coincide con el ID de la recomendación.");
                }

                await _repository.Update(recommendation);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la recomendación: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina una recomendación de la base de datos.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingRecommendation = await _repository.GetById(id);
                if (existingRecommendation == null)
                {
                    return NotFound($"Recomendación con ID {id} no encontrada.");
                }

                await _repository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la recomendación: {ex.Message}");
            }
        }
    }
}
