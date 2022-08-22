using Microsoft.EntityFrameworkCore;
using OpenBooks.Repository;

namespace OpenBooks.Data
{
    public class OpenBooksContext : DbContext
    {
        public OpenBooksContext(DbContextOptions<OpenBooksContext> options)
            : base(options)
        {
#if DEBUG
            //For debugging, ensure we have a fresh db each time.
            //elsewhere, keep the same database
            Database.EnsureDeleted();
#endif
            Database.EnsureCreated();
        }

        public DbSet<Book> Book { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Basic data
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Author = "A. A. Milne",
                    Title = "Winnie-the-Pooh",
                    Price = 19.25m
                },
                new Book
                {
                    Id = 2,
                    Author = "Jane Austen",
                    Title = "Pride and Prejudice",
                    Price = 5.49m
                },
                new Book
                {
                    Id = 3,
                    Author = "William Shakespeare",
                    Title = "Romeo and Juliet",
                    Price = 6.95m
                }
            );

            //Data from bestsellers list 2009 - 2019 https://www.kaggle.com/datasets/sootersaalu/amazon-top-50-bestselling-books-2009-2019
            //modelBuilder.Entity<Book>().HasData(CsvRepository.GetDistinct(@"..\Data\bestsellers with categories.csv"));
        }
    }
}
