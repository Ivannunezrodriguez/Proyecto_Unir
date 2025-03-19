using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Model;

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
    public async Task<ActionResult<IEnumerable<GameStatus>>> GetAll() => Ok(await _repository.GetAll());

    /// <summary>
    /// Obtiene un estado de juego por ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<GameStatus>> GetById(int id)
    {
        var gameStatus = await _repository.GetById(id);
        return gameStatus == null ? NotFound() : Ok(gameStatus);
    }

    /// <summary>
    /// Agrega un nuevo estado de juego a la base de datos.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<GameStatus>> Create(GameStatus gameStatus)
    {
        await _repository.Create(gameStatus);
        return CreatedAtAction(nameof(GetById), new { id = gameStatus.StatusId }, gameStatus);
    }

    /// <summary>
    /// Actualiza la información de un estado de juego existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, GameStatus gameStatus)
    {
        gameStatus.StatusId = id;
        await _repository.Update(gameStatus);
        return NoContent();
    }

    /// <summary>
    /// Elimina un estado de juego de la base de datos.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.Delete(id);
        return NoContent();
    }
}
