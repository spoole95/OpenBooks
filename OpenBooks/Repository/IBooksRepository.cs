using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenBooks.Repository
{
    public interface IBooksRepository
    {
        Task<int> Create(Book book);
        Task<IEnumerable<Book>> Get();
        Task<Book> Get(int id);
        Task Update(Book book);
    }
}
