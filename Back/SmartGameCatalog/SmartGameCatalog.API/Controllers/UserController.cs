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
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _repository;

        public UsersController(UserRepository repository)
        {
            _repository = repository;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _repository.GetUsersAsync();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            user.Id_User = Guid.NewGuid();
            user.Registration_Date = DateTime.UtcNow;
            await _repository.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id_User }, user);
        }
    }
}
