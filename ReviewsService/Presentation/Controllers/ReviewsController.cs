using Microsoft.AspNetCore.Mvc;
using ReviewsService.Persistence.Models;
using ReviewsService.Persistence.Repositories;

namespace ReviewsService.Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsRepository _reviewsRepository;

        public ReviewsController(IReviewsRepository reviewsRepository)
        {
            _reviewsRepository = reviewsRepository;
        }

        [HttpGet("book/{bookId}")]
        public IActionResult GetReviewsForBook(int bookId)
        {
            var reviews = _reviewsRepository.GetReviewsForBook(bookId);
            return Ok(reviews);
        }

        // POST: api/reviews
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] Review review)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _reviewsRepository.CreateReview(review);
            return CreatedAtAction(nameof(GetReviewsForBook), new { bookId = review.BookId }, review);
        }
    }
}
