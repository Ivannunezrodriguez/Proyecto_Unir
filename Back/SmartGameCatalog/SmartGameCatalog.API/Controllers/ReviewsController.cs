using Microsoft.AspNetCore.Mvc;
using SmartGameCatalog.API.Models;
using SmartGameCatalog.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGameCatalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewRepository _repository;

        public ReviewsController(ReviewRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            var reviews = await _repository.GetReviewsAsync();
            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReview(Review review)
        {
            review.Id_Review = Guid.NewGuid();
            review.Date = DateTime.UtcNow;
            await _repository.AddReviewAsync(review);
            return CreatedAtAction(nameof(GetReviews), new { id = review.Id_Review }, review);
        }
    }
}
