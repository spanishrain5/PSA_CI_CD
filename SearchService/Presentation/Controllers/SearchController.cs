using Microsoft.AspNetCore.Mvc;
using SearchService.Persistence.Models;
using SearchService.Persistence.Repositories;

namespace SearchService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {

        private readonly IBookRepository _bookRepository;

        public SearchController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest("Keyword is required.");

            var results = _bookRepository.Search(keyword);
            return Ok(results);
        }
    }
}
