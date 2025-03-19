using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using SmartGameCatalog.API.Model;

namespace SmartGameCatalog.API.Controllers
{
[Route("api/ratings")]
[ApiController]
public class RatingsController : ControllerBase
{
    private readonly RatingRepository _repository;
    public RatingsController(RatingRepository repository) { _repository = repository; }

    /// <summary>
    /// Obtiene la lista de todas las calificaciones registradas.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Rating>>> GetAll() => Ok(await _repository.GetAll());

    /// <summary>
    /// Obtiene una calificación por ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Rating>> GetById(int id)
    {
        var entity = await _repository.GetById(id);
        return entity == null ? NotFound() : Ok(entity);
    }

    /// <summary>
    /// Agrega una nueva calificación a la base de datos.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Rating>> Create(Rating rating)
    {
        await _repository.Create(rating);
        return CreatedAtAction(nameof(GetById), new { id = rating.RatingId }, rating);
    }

    /// <summary>
    /// Actualiza la información de una calificación existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Rating rating)
    {
        await _repository.Update(rating);
        return NoContent();
    }

    /// <summary>
    /// Elimina una calificación de la base de datos.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.Delete(id);
        return NoContent();
    }
}
}