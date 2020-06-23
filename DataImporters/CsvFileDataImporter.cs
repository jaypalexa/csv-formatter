using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.IO;
using CsvFormatter.DataImporters.Interfaces;
using CsvHelper;

namespace CsvFormatter.DataImporters
{
    public class CsvFileDataImporter : IDataImporter
    {
        public List<ExpandoObject> Import()
        {
            var records = new List<ExpandoObject>();

            Console.WriteLine("Enter CSV input file name:");
            var fullInputPathAndFileName = Console.ReadLine();
            //TODO: sanitize/validate file name, validate that it is a CSV file, etc.

            using (var streamReader = new StreamReader(fullInputPathAndFileName))
            using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            using (var csvDataReader = new CsvDataReader(csvReader))
            {
                var dataTable = new DataTable();
                dataTable.Load(csvDataReader);
                records = CsvParserHelper.Parse(dataTable);
            }

            return records;
        }
    }
}
