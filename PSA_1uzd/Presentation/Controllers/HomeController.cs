using Microsoft.AspNetCore.Mvc;
using PSA_1uzd.Business.Services;
using PSA_1uzd.Persistence.Models;

namespace PSA_1uzd.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookSearchService _bookSearchService;
        private readonly IReviewService _reviewService;
        private readonly SpotifyService _spotifyService;

        public HomeController(IBookSearchService bookSearchService, IReviewService reviewService, SpotifyService spotifyService)
        {
            _bookSearchService = bookSearchService;
            _reviewService = reviewService;
            _spotifyService = spotifyService;
        }

        public async Task<IActionResult> Index(string keyword)
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return View("Index");
            }

            var results = await _bookSearchService.Search(query);

            if (!results.Any())
            {
                ViewBag.Error = "No results or an error occurred.";
                return View("Index");
            }

            return View("SearchResults", results);
        }

        public IActionResult AddReview(int bookId, string keyword)
        {
            var review = new Review
            {
                BookId = bookId,
                Keyword = keyword
            };
            return View(review);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitReview(Review review)
        {
            var success = await _reviewService.SubmitReview(review);

            if (success)
                return RedirectToAction("Search", "Home", new { query = review.Keyword });

            ModelState.AddModelError(string.Empty, "An error occurred while submitting the review.");
            return View("AddReview", review);
        }

        public async Task<IActionResult> FindPlaylists(string genre)
        {

            var playlists = await _spotifyService.SearchPlaylistsAsync(genre);

            if (playlists == null || !playlists.Any())
            {
                ViewBag.NoPlaylistsFound = true;
            }
            else
            {
                ViewBag.NoPlaylistsFound = false;
            }

            ViewBag.Genre = genre;

            return View(playlists);
        }


    }
}
