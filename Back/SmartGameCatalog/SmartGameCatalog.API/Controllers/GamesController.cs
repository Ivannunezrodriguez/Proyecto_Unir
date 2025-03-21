using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Model;


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
    public async Task<ActionResult<IEnumerable<Game>>> GetAll() => Ok(await _repository.GetAll());

    /// <summary>
    /// Obtiene un juego por ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Game>> GetById(int id)
    {
        var game = await _repository.GetById(id);
        return game == null ? NotFound() : Ok(game);
    }

    /// <summary>
    /// Agrega un nuevo juego a la base de datos.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Game>> Create(Game game)
    {
        await _repository.Create(game);
        return CreatedAtAction(nameof(GetById), new { id = game.GameId }, game);
    }

    /// <summary>
    /// Actualiza la información de un juego existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Game game)
    {
        game.GameId = id;
        await _repository.Update(game);
     return Ok(new { message = "Estado actualizado correctamente" });
    }

    /// <summary>
    /// Elimina un juego de la base de datos.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.Delete(id);
    return Ok(new { message = "Estado borrado correctamente" });
    }
}
