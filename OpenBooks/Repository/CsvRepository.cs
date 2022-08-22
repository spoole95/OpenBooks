using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace OpenBooks.Repository
{
    /// <summary>
    /// Base class of json file based repository
    /// </summary>
    public abstract class CsvRepository<T>
    {
        private readonly string filePath;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath">Path to the repository file</param>
        public CsvRepository(string filePath)
        {
            this.filePath = filePath;
        }


        /// <summary>
        /// Gets all T from the repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected IEnumerable<T> Get()
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

        protected abstract T ParseRow(List<string> csvRow);

        private static bool HeaderOrBlank(int rowIndex, List<string> csvRow)
        {
            return rowIndex == 0 || csvRow == null || (csvRow.Count == 1 && string.IsNullOrWhiteSpace(csvRow[0]));
        }

        protected void UpdateList(IEnumerable<T> list)
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(list, new JsonSerializerOptions
            {
                WriteIndented = true, //Write indented otherwise humans stand no chance of reading again.
                IgnoreNullValues = true, //Null values not needed to be stored
            }));
        }
    }
}
