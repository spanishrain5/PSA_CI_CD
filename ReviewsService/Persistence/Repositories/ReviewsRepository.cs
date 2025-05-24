using ReviewsService.Persistence.Models;
using ReviewsService.Persistence.Context;

namespace ReviewsService.Persistence.Repositories
{
    public interface IReviewsRepository
    {
        List<Review> GetReviewsForBook(int bookId);
        Task<Review> CreateReview(Review review);
    }

    public class ReviewsRepository : IReviewsRepository
    {
        private readonly BooksReviewsDbContext _context;

        public ReviewsRepository(BooksReviewsDbContext context)
        {
            _context = context;
        }

        public List<Review> GetReviewsForBook(int bookId)
        {
            return _context.Reviews.Where(r => r.BookId == bookId).ToList();
        }

        public async Task<Review> CreateReview(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return review;
        }
    }
}
