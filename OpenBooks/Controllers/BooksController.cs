using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenBooks.Repository.Books;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenBooks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {

        private readonly ILogger<BooksController> _logger;
        private readonly IBooksRepository _repository;

        public BooksController(ILogger<BooksController> logger, IBooksRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost]
        public async Task<int> Create(Book book)
        {
            return await _repository.Create(book);
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> Get(string sortBy = "title")
        {
            var sortType = Enum.Parse<BookSortType>(sortBy.ToLower());

            return await _repository.Get(sortType);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Book> GetById(int id)
        {
            return await _repository.Get(id);
        }

        [HttpPut]
        public async Task Update(int id, Book book)
        {
            if (id != book.Id)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.Update(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        throw new NullReferenceException("Book not found");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        private bool BookExists(int id)
        {
            return _repository.Get(id) != null;
        }
    }
}
