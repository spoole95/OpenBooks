using NUnit.Framework;
using OpenBooks.Repository;
using System.Linq;

namespace OpenBooks.Tests
{
    public class CsvRepositoryTests
    {
        [Test]
        public void LoadAll_should_return_all()
        {
            //Act
            var result = CsvRepository.Get(@"../../../../Data/bestsellers with categories.csv");

            //Assert
            Assert.That(result.Count(), Is.GreaterThan(0));



        }
    }
}