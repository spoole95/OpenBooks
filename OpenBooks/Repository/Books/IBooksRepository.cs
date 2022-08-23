using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenBooks.Repository.Books
{
    public interface IBooksRepository
    {
        Task<int> Create(Book book);
        Task<IEnumerable<Book>> Get(BookSortType sortType);
        Task<Book> Get(int id);
        Task Update(Book book);
    }
}
