using PSA_1uzd.Persistence.Models;

namespace PSA_1uzd.Business.Services
{
    public interface IReviewService
    {
        Task<bool> SubmitReview(Review review);
    }

    public class ReviewService : IReviewService
    {
        private readonly HttpClient _reviewsClient;

        public ReviewService(IHttpClientFactory factory)
        {
            _reviewsClient = factory.CreateClient("ReviewService");
        }

        public async Task<bool> SubmitReview(Review review)
        {
            var response = await _reviewsClient.PostAsJsonAsync("api/reviews", review);
            return response.IsSuccessStatusCode;
        }
    }
}
