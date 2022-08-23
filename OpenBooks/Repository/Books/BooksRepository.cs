using Microsoft.EntityFrameworkCore;
using OpenBooks.Data;
using OpenBooks.Repository.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenBooks.Repository.Books
{
    public class BooksRepository : IBooksRepository
    {
        private readonly OpenBooksContext _context;

        private readonly IDictionary<BookSortType, IOrderBy> SortFunction =
            new Dictionary<BookSortType, IOrderBy>()
            {
                { BookSortType.title, new OrderBy<Book, string>(x => x.Title)},
                { BookSortType.author, new OrderBy<Book, string>(x => x.Author) },
                { BookSortType.price, new OrderBy<Book, decimal>(x => x.Price) }
            };

        public BooksRepository(OpenBooksContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Book book)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task<IEnumerable<Book>> Get(BookSortType sortType)
        {
            return await _context.Book.OrderBy(SortFunction[sortType]).ToListAsync();
        }

        public async Task<Book> Get(int id)
        {
            return await _context.Book.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Update(Book book)
        {
            _context.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}
