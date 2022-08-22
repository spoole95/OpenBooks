using Microsoft.EntityFrameworkCore;
using OpenBooks.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBooks.Repository
{
    public class BooksRepository : IBooksRepository
    {
        private readonly OpenBooksContext _context;

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

        public async Task<IEnumerable<Book>> Get()
        {
            return await _context.Book.ToListAsync();
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
