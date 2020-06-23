using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using CsvHelper;
using CsvFormatter.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using CsvFormatter.OutputBuilders;

namespace CsvFormatter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //TODO: FUTURE:  support input from database as well as from CSV file
                var inputSource = InputSource.CsvFile;

                var records = inputSource switch
                {
                    InputSource.CsvFile => ImportFromCsvFile(),
                    InputSource.Database => ImportFromDatabase(),
                    _ => throw new NotSupportedException($"Input source '{inputSource}' not supported")
                };

                //TODO: put this prompt in a loop in case input is not valid
                Console.WriteLine(string.Empty);
                Console.WriteLine("Enter 1 to format output as JSON");
                Console.WriteLine("Enter 2 to format output as XML");
                var outputFormatAsString = Console.ReadLine();

                //TODO: sanitize/validate input to make sure it is valid, is within enum range, etc.
                var outputFormat = GetOutputFormat(outputFormatAsString);

                var outputBuilderFactory = new OutputBuilderFactory(outputFormat);
                var outputBuilder = outputBuilderFactory.Create();
                var outputAsString = outputBuilder.Build(records);

                Console.WriteLine(string.Empty);
                Console.WriteLine("Enter output file folder:");
                var outputFilePath = Console.ReadLine();
                var fullOutputPathAndFileName = Path.Combine(outputFilePath, $"records.{DateTime.Now:yyyyMMddHHmmss}.{outputFormat.ToString().ToLower()}");

                //TODO: add guard rails for output path location, permissions, overwrite, etc.
                File.WriteAllText(fullOutputPathAndFileName, outputAsString);

                Console.WriteLine(string.Empty);
                Console.WriteLine($"Output file written to: {fullOutputPathAndFileName}");
            }
            catch (Exception ex)
            {
                //TODO: nicer exception handling for the user
                Console.WriteLine($"ERROR: {ex}");
            }
        }

        private static OutputFormat GetOutputFormat(string outputFormatAsString)
        {
            if (Enum.TryParse(outputFormatAsString, true, out OutputFormat outputFormat))
            {
                if (Enum.IsDefined(typeof(OutputFormat), outputFormat))
                {
                    return outputFormat;
                }
                else
                {
                    throw new InvalidOperationException($"{outputFormatAsString} is not an underlying value of the {nameof(OutputFormat)} enumeration.");
                }
            }
            else
            {
                throw new InvalidOperationException($"{outputFormatAsString} is not a member of the {nameof(OutputFormat)} enumeration.");
            }
        }

        private static List<ExpandoObject> ImportFromCsvFile()
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

        private static List<ExpandoObject> ImportFromDatabase()
        {
            throw new NotImplementedException("TODO:  ImportFromDatabase");
        }

    }
}
