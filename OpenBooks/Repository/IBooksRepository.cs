using System.Collections.Generic;

namespace OpenBooks.Repository
{
    public interface IBooksRepository
    {
        int Create(Book book);
        IEnumerable<Book> Get();
        Book Get(int id);
        void Update(int id, Book book);
    }
}
