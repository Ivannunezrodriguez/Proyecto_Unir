using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Model;

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
    public async Task<ActionResult<IEnumerable<Favorite>>> GetAll() => Ok(await _repository.GetAll());

    /// <summary>
    /// Obtiene un juego favorito por ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Favorite>> GetById(int id)
    {
        var favorite = await _repository.GetById(id);
        return favorite == null ? NotFound() : Ok(favorite);
    }

    /// <summary>
    /// Agrega un juego a favoritos.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Favorite>> Create(Favorite favorite)
    {
        await _repository.Create(favorite);
        return CreatedAtAction(nameof(GetById), new { id = favorite.FavoriteId }, favorite);
    }

    /// <summary>
    /// Actualiza un juego favorito existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Favorite favorite)
    {
        favorite.FavoriteId = id;
        await _repository.Update(favorite);
        return NoContent();
    }

    /// <summary>
    /// Elimina un juego de la lista de favoritos.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.Delete(id);
        return NoContent();
    }
}
