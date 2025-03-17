using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Models;
using SmartGameCatalog.API.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    // Define la ruta base de la API para este controlador como "api/Users".
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // Repositorio que maneja la lógica de acceso a datos para los usuarios.
        private readonly UserRepository _repository;

        // Constructor que inyecta el repositorio de usuarios.
        public UsersController(UserRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene la lista de todos los usuarios registrados.
        /// </summary>
        /// <returns>Una lista de objetos User.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _repository.GetUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Obtiene un usuario específico por su identificador único.
        /// </summary>
        /// <param name="id">El identificador único del usuario.</param>
        /// <returns>El objeto User si se encuentra, de lo contrario devuelve un error 404.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Crea un nuevo usuario y lo almacena en la base de datos.
        /// </summary>
        /// <param name="user">El objeto User a registrar.</param>
        /// <returns>Un resultado con el estado de la operación.</returns>
        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            // Genera un nuevo identificador único para el usuario.
            user.Id_User = Guid.NewGuid();
            // Establece la fecha de registro en UTC.
            user.Registration_Date = DateTime.UtcNow;
            // Agrega el usuario a la base de datos.
            await _repository.AddUserAsync(user);
            // Devuelve una respuesta 201 Created con la ubicación del nuevo recurso.
            return CreatedAtAction(nameof(GetUser), new { id = user.Id_User }, user);
        }
    }
}