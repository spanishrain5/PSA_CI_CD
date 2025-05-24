using SearchService.Persistence.Models;
using SearchService.Persistence.Context;

namespace SearchService.Persistence.Repositories
{
    public interface IBookRepository
    {
        List<Book> Search(string keyword);
    }

    public class BookRepository : IBookRepository
    {
        private readonly BooksSearchDbContext _context;

        public BookRepository(BooksSearchDbContext context)
        {
            _context = context;
        }

        public List<Book> Search(string keyword)
        {
            return _context.Books
                .Where(b => b.Title.Contains(keyword))
                .ToList();
        }
    }

}
