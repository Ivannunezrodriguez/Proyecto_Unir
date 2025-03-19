using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Model;

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
    public async Task<ActionResult<IEnumerable<User>>> GetAll() => Ok(await _repository.GetAll());

    /// <summary>
    /// Obtiene un usuario por ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(int id)
    {
        var user = await _repository.GetById(id);
        return user == null ? NotFound() : Ok(user);
    }

    /// <summary>
    /// Registra un nuevo usuario en la base de datos.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<User>> Create(User user)
    {
        await _repository.Create(user);
        return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
    }

    /// <summary>
    /// Actualiza la información de un usuario existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, User user)
    {
        user.UserId = id;
        await _repository.Update(user);
        return NoContent();
    }

    /// <summary>
    /// Elimina un usuario de la base de datos.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.Delete(id);
        return NoContent();
    }
}
