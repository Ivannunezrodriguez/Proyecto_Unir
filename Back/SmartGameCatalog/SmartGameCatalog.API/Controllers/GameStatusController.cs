using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Model;

namespace SmartGameCatalog.API.Controllers
{
    [Route("api/gamestatus")]
    [ApiController]
    public class GameStatusController : ControllerBase
    {
        private readonly GameStatusRepository _repository;

        public GameStatusController(GameStatusRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene la lista de todos los estados de juego registrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameStatus>>> GetAll()
        {
            try
            {
                var gameStatuses = await _repository.GetAll();
                return Ok(gameStatuses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene un estado de juego por ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<GameStatus>> GetById(int id)
        {
            try
            {
                var gameStatus = await _repository.GetById(id);
                if (gameStatus == null)
                {
                    return NotFound($"Estado de juego con ID {id} no encontrado.");
                }
                return Ok(gameStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Agrega un nuevo estado de juego a la base de datos.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<GameStatus>> Create(GameStatus gameStatus)
        {
            try
            {
                if (gameStatus == null)
                {
                    return BadRequest("Los datos del estado del juego no pueden ser nulos.");
                }

                if (gameStatus.Status != "Jugado" && gameStatus.Status != "Deseado")
                {
                    return BadRequest("El estado debe ser 'Jugado' o 'Deseado'.");
                }

                await _repository.Create(gameStatus);
                return CreatedAtAction(nameof(GetById), new { id = gameStatus.StatusId }, gameStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el estado del juego: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza la información de un estado de juego existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GameStatus gameStatus)
        {
            try
            {
                if (gameStatus == null)
                {
                    return BadRequest("Los datos del estado del juego no pueden ser nulos.");
                }

                if (id != gameStatus.StatusId)
                {
                    return BadRequest("El ID en la URL no coincide con el ID del estado de juego.");
                }

                if (gameStatus.Status != "Jugado" && gameStatus.Status != "Deseado")
                {
                    return BadRequest("El estado debe ser 'Jugado' o 'Deseado'.");
                }

                await _repository.Update(gameStatus);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el estado del juego: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un estado de juego de la base de datos.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingGameStatus = await _repository.GetById(id);
                if (existingGameStatus == null)
                {
                    return NotFound($"Estado de juego con ID {id} no encontrado.");
                }

                await _repository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el estado del juego: {ex.Message}");
            }
        }
    }
}
