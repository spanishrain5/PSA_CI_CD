using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PSA_1uzd.Persistence.Models;
using PSA_1uzd.Presentation.Controllers;

namespace PSA_1uzd.Business.Services
{
    public interface IBookSearchService
    {
        Task<List<BookResult>> Search(string query);
    }

    public class BookSearchService : IBookSearchService
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _booksClient;
        private readonly HttpClient _reviewsClient;
        private readonly SpotifyService _spotifyService;

        public BookSearchService(IHttpClientFactory factory, ILogger<HomeController> logger, SpotifyService spotifyService)
        {
            _logger = logger;

            _booksClient = factory.CreateClient("SearchService");
            _reviewsClient = factory.CreateClient("ReviewService");
            _spotifyService = spotifyService;
        }

        [HttpGet]
        public async Task<List<BookResult>> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<BookResult>();
            }

            var response = await _booksClient.GetAsync($"api/search?keyword={Uri.EscapeDataString(query)}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var results = JsonConvert.DeserializeObject<List<BookResult>>(json) ?? new();

                foreach (var book in results)
                {
                    var reviewsResponse = await _reviewsClient.GetAsync($"api/reviews/book/{book.ID}");
                    book.Reviews = reviewsResponse.IsSuccessStatusCode
                        ? await reviewsResponse.Content.ReadFromJsonAsync<List<Review>>() ?? new()
                        : new List<Review>();
                }

                return results;
            }

            return new List<BookResult>();
        }
    }
}
