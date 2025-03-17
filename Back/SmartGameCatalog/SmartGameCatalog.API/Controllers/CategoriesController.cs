using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Models;
using SmartGameCatalog.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    // Define la ruta base de la API para este controlador como "api/Categories".
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        // Repositorio que maneja la lógica de acceso a datos para las categorías.
        private readonly CategoryRepository _repository;

        // Constructor que inyecta el repositorio de categorías.
        public CategoriesController(CategoryRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene la lista de todas las categorías disponibles.
        /// </summary>
        /// <returns>Una lista de objetos Category.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _repository.GetCategoriesAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Crea una nueva categoría y la almacena en la base de datos.
        /// </summary>
        /// <param name="category">El objeto Category a crear.</param>
        /// <returns>Un resultado con el estado de la operación.</returns>
        [HttpPost]
        public async Task<ActionResult> CreateCategory(Category category)
        {
            // Genera un nuevo identificador único para la categoría.
            category.Id_Category = Guid.NewGuid();
            // Agrega la categoría a la base de datos.
            await _repository.AddCategoryAsync(category);
            // Devuelve una respuesta 201 Created con la ubicación del nuevo recurso.
            return CreatedAtAction(nameof(GetCategories), new { id = category.Id_Category }, category);
        }
    }
}