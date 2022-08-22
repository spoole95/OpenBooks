using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpenBooks.Repository
{
    public static class CsvRepository
    {
        /// <summary>
        /// Gets all books from the repository https://www.kaggle.com/datasets/sootersaalu/amazon-top-50-bestselling-books-2009-2019
        /// As this dataset contains duplicates books irrelivant to our use, create a distinct list with the average price over the years given.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Book> GetDistinct(string filePath)
        {
            var result = Get(filePath);

            var distinctResult = result.GroupBy(x => x.Title).Select(x => new Book
            {
                Title = x.First().Title,
                Author = x.First().Author,
                Price = x.Average(y => y.Price),
            }).ToList();

            var counter = 0;
            foreach (Book book in distinctResult)
            {
                counter++;
                book.Id = counter;
            }

            return distinctResult;
        }


        /// <summary>
        /// Gets all books from the repository https://www.kaggle.com/datasets/sootersaalu/amazon-top-50-bestselling-books-2009-2019
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Book> Get(string filePath)
        {
            TextFieldParser fileParser = new TextFieldParser(File.OpenRead(filePath));
            if (fileParser == null || fileParser.EndOfData)
            {
                throw new FileNotFoundException($"Could not find file {filePath}");
            }
            fileParser.HasFieldsEnclosedInQuotes = true;
            fileParser.SetDelimiters(",");

            var text = fileParser.ReadToEnd(); //Don't inspect this variable until after regex replace for large files. It will crash
            fileParser.Close();

            TextFieldParser textParser = new TextFieldParser(new StringReader(text));
            textParser.HasFieldsEnclosedInQuotes = true;
            textParser.SetDelimiters(",");

            //To track where we are within the CSV
            int rowIndex = 0;

            while (!textParser.EndOfData)
            {
                List<string> csvRow = textParser.ReadFields().ToList();

                //Ignore the first row (row = 0).  Also ignore any rows that are completely blank
                if (HeaderOrBlank(rowIndex, csvRow))
                {
                    rowIndex++;
                    continue;
                }

                yield return ParseRow(csvRow);
            }
        }

        private static bool HeaderOrBlank(int rowIndex, List<string> csvRow)
        {
            return rowIndex == 0 || csvRow == null || (csvRow.Count == 1 && string.IsNullOrWhiteSpace(csvRow[0]));
        }


        private static int idCounter = 0;
        private static Book ParseRow(List<string> csvRow)
        {
            idCounter++;
            return new Book
            {
                Id = idCounter,
                Title = csvRow[0],
                Author = csvRow[1],
                Price = decimal.Parse(csvRow[4])
            };
        }
    }
}
