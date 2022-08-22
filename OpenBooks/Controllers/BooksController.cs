using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenBooks.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public int Create(Book book)
        {
            //TODO - Validator?

            return _repository.Create(book);
        }

        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _repository.Get();
        }

        [HttpGet]
        [Route("{id}")]
        public Book GetById(int id)
        {
            return _repository.Get(id);
        }

        [HttpPut]
        public void Update(int id, Book book)
        {
            _repository.Update(id, book);
        }
    }
}
