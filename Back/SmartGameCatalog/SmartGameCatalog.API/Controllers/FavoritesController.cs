using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Model;

namespace SmartGameCatalog.API.Controllers
{
    [Route("api/favorites")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly FavoriteRepository _repository;

        public FavoritesController(FavoriteRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene la lista de todos los juegos favoritos.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favorite>>> GetAll()
        {
            try
            {
                var favorites = await _repository.GetAll();
                return Ok(favorites);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene un juego favorito por ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Favorite>> GetById(int id)
        {
            try
            {
                var favorite = await _repository.GetById(id);
                if (favorite == null)
                {
                    return NotFound($"Favorito con ID {id} no encontrado.");
                }
                return Ok(favorite);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Agrega un juego a favoritos.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Favorite>> Create(Favorite favorite)
        {
            try
            {
                if (favorite == null)
                {
                    return BadRequest("Los datos del favorito no pueden ser nulos.");
                }

                if (favorite.UserId <= 0 || favorite.GameId <= 0)
                {
                    return BadRequest("El UserId y el GameId deben ser números válidos.");
                }

                // Verificar si ya existe el favorito para este usuario y juego
                var existingFavorite = (await _repository.GetAll()).FirstOrDefault(f =>
                    f.UserId == favorite.UserId && f.GameId == favorite.GameId);

                if (existingFavorite != null)
                {
                    return Conflict("Este juego ya está en la lista de favoritos del usuario.");
                }

                await _repository.Create(favorite);
                return CreatedAtAction(nameof(GetById), new { id = favorite.FavoriteId }, favorite);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar a favoritos: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza un juego favorito existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Favorite favorite)
        {
            try
            {
                if (favorite == null)
                {
                    return BadRequest("Los datos del favorito no pueden ser nulos.");
                }

                if (id != favorite.FavoriteId)
                {
                    return BadRequest("El ID en la URL no coincide con el ID del favorito.");
                }

                await _repository.Update(favorite);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el favorito: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un juego de la lista de favoritos.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingFavorite = await _repository.GetById(id);
                if (existingFavorite == null)
                {
                    return NotFound($"Favorito con ID {id} no encontrado.");
                }

                await _repository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el favorito: {ex.Message}");
            }
        }
    }
}
