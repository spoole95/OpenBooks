using System.Collections.Generic;
using System.Linq;

namespace OpenBooks.Repository
{
    public class BooksRepository : CsvRepository<Book>, IBooksRepository
    {
        /// <summary>
        /// Loads books from data set on https://www.kaggle.com/datasets/sootersaalu/amazon-top-50-bestselling-books-2009-2019
        /// Contains multiple rows for books where sold for multiple years with differing prices
        /// </summary>
        public BooksRepository() : base(@"..\Data\bestsellers with categories.csv")
        {
            counter = 0;
        }

        public int Create(Book book)
        {
            throw new System.NotImplementedException();
        }

        public new IEnumerable<Book> Get()
        {
            return base.Get();
        }

        public Book Get(int id)
        {
            var books = Get();
            return books.Single(x => x.Id == id);
        }

        public void Update(int id, Book book)
        {
            throw new System.NotImplementedException();
        }


        private int counter;
        protected override Book ParseRow(List<string> csvRow)
        {
            counter++;
            return new Book(counter, csvRow[0], csvRow[1], decimal.Parse(csvRow[4]));
        }
    }
}
