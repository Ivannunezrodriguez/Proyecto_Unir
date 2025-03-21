using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Model;

[Route("api/ratings")]
[ApiController]
public class RatingsController : ControllerBase
{
    private readonly RatingRepository _repository;

    public RatingsController(RatingRepository repository)
    {
        _repository = repository;
    }

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
        var rating = await _repository.GetById(id);
        return rating == null ? NotFound() : Ok(rating);
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
        rating.RatingId = id;
        await _repository.Update(rating);
        return Ok(new { message = "Estado actulizado correctamente" });
    }

    /// <summary>
    /// Elimina una calificación de la base de datos.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.Delete(id);
       return Ok(new { message = "Estado borrado correctamente" });
    }
}
