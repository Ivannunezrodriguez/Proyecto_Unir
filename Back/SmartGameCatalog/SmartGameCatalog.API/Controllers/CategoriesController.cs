using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Models;
using SmartGameCatalog.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryRepository _repository;

        public CategoriesController(CategoryRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _repository.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(Category category)
        {
            category.Id_Category = Guid.NewGuid();
            await _repository.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategories), new { id = category.Id_Category }, category);
        }
    }
}
