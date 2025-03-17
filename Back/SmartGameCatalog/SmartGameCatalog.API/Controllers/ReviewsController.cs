using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Models;
using SmartGameCatalog.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    // Define la ruta base de la API para este controlador como "api/Reviews".
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        // Repositorio que maneja la lógica de acceso a datos para las reseñas.
        private readonly ReviewRepository _repository;

        // Constructor que inyecta el repositorio de reseñas.
        public ReviewsController(ReviewRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene la lista de todas las reseñas registradas.
        /// </summary>
        /// <returns>Una lista de objetos Review.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            var reviews = await _repository.GetReviewsAsync();
            return Ok(reviews);
        }

        /// <summary>
        /// Crea una nueva reseña y la almacena en la base de datos.
        /// </summary>
        /// <param name="review">El objeto Review a registrar.</param>
        /// <returns>Un resultado con el estado de la operación.</returns>
        [HttpPost]
        public async Task<ActionResult> CreateReview(Review review)
        {
            // Genera un nuevo identificador único para la reseña.
            review.Id_Review = Guid.NewGuid();
            // Establece la fecha y hora de la reseña en UTC.
            review.Date = DateTime.UtcNow;
            // Agrega la reseña a la base de datos.
            await _repository.AddReviewAsync(review);
            // Devuelve una respuesta 201 Created con la ubicación del nuevo recurso.
            return CreatedAtAction(nameof(GetReviews), new { id = review.Id_Review }, review);
        }
    }
}