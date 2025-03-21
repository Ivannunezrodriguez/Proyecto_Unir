using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Model;

[Route("api/recommendations")]
[ApiController]
public class RecommendationsController : ControllerBase
{
    private readonly RecommendationRepository _repository;

    public RecommendationsController(RecommendationRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Obtiene la lista de todas las recomendaciones registradas.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Recommendation>>> GetAll() => Ok(await _repository.GetAll());

    /// <summary>
    /// Obtiene una recomendación por ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Recommendation>> GetById(int id)
    {
        var recommendation = await _repository.GetById(id);
        return recommendation == null ? NotFound() : Ok(recommendation);
    }

    /// <summary>
    /// Agrega una nueva recomendación a la base de datos.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<Recommendation>> Create(Recommendation recommendation)
    {
        await _repository.Create(recommendation);
        return CreatedAtAction(nameof(GetById), new { id = recommendation.RecommendationId }, recommendation);
    }

    /// <summary>
    /// Actualiza la información de una recomendación existente.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Recommendation recommendation)
    {
        recommendation.RecommendationId = id;
        await _repository.Update(recommendation);
   return Ok(new { message = "Estado actulizado correctamente" });
    }

    /// <summary>
    /// Elimina una recomendación de la base de datos.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repository.Delete(id);
   return Ok(new { message = "Estado borrado correctamente" });
    }
}
