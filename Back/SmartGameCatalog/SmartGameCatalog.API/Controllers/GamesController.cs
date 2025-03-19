using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Model;

namespace SmartGameCatalog.API.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GameRepository _repository;

        public GamesController(GameRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene la lista de todos los juegos registrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetAll()
        {
            try
            {
                var games = await _repository.GetAll();
                return Ok(games);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene un juego por ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetById(int id)
        {
            try
            {
                var game = await _repository.GetById(id);
                if (game == null)
                {
                    return NotFound($"Juego con ID {id} no encontrado.");
                }
                return Ok(game);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Agrega un nuevo juego a la base de datos.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Game>> Create(Game game)
        {
            try
            {
                if (game == null)
                {
                    return BadRequest("Los datos del juego no pueden ser nulos.");
                }

                if (game.IgdbId <= 0)
                {
                    return BadRequest("El ID de IGDB debe ser un número válido.");
                }

                await _repository.Create(game);
                return CreatedAtAction(nameof(GetById), new { id = game.GameId }, game);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el juego: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza la información de un juego existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Game game)
        {
            try
            {
                if (game == null)
                {
                    return BadRequest("Los datos del juego no pueden ser nulos.");
                }

                if (id != game.GameId)
                {
                    return BadRequest("El ID en la URL no coincide con el ID del juego.");
                }

                if (game.IgdbId <= 0)
                {
                    return BadRequest("El ID de IGDB debe ser un número válido.");
                }

                await _repository.Update(game);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el juego: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un juego de la base de datos.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingGame = await _repository.GetById(id);
                if (existingGame == null)
                {
                    return NotFound($"Juego con ID {id} no encontrado.");
                }

                await _repository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el juego: {ex.Message}");
            }
        }
    }
}
