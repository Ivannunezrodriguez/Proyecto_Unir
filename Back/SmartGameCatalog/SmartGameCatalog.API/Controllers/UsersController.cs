using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Model;

namespace SmartGameCatalog.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _repository;

        public UsersController(UserRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene la lista de todos los usuarios registrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            try
            {
                var users = await _repository.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene un usuario por ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            try
            {
                var user = await _repository.GetById(id);
                if (user == null)
                {
                    return NotFound($"Usuario con ID {id} no encontrado.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        /// <summary>
        /// Registra un nuevo usuario en la base de datos.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<User>> Create(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Los datos del usuario no pueden ser nulos.");
                }

                await _repository.Create(user);
                return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el usuario: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza la información de un usuario existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("Los datos del usuario no pueden ser nulos.");
                }

                if (id != user.UserId)
                {
                    return BadRequest("El ID proporcionado en la URL no coincide con el ID del usuario.");
                }

                await _repository.Update(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el usuario: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un usuario de la base de datos.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingUser = await _repository.GetById(id);
                if (existingUser == null)
                {
                    return NotFound($"Usuario con ID {id} no encontrado.");
                }

                await _repository.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el usuario: {ex.Message}");
            }
        }
    }
}
