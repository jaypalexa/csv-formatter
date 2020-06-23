using System;
using System.IO;
using CsvFormatter.DataImporters;
using CsvFormatter.Enums;
using CsvFormatter.OutputBuilders;

namespace CsvFormatter
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: converting back to CSV from other XML or JSON -- get data into a standard format (e.g., DataTable) and manipulate it "generically"
            //TODO: converting between XML and JSON -- get data into a standard format (e.g., DataTable) and manipulate it "generically"
            //TODO: input from a different source, e.g. a database -- stubbed out in DatabaseDataImporter.cs

            try
            {
                //TODO: FUTURE: support input from database as well as from CSV file
                var inputSource = InputSource.CsvFile;
                var dataImporterFactory = new DataImporterFactory(inputSource);
                var dataImporter = dataImporterFactory.Create();
                var records = dataImporter.Import();

                //TODO: put prompts in a loop in case input is not valid

                Console.WriteLine(string.Empty);
                Console.WriteLine("Enter 1 to format output as JSON");
                Console.WriteLine("Enter 2 to format output as XML");
                var outputFormatAsString = Console.ReadLine();

                var outputFormat = CsvParserHelper.GetOutputFormat(outputFormatAsString);
                var outputBuilderFactory = new OutputBuilderFactory(outputFormat);
                var outputBuilder = outputBuilderFactory.Create();
                var outputAsString = outputBuilder.Build(records);

                //TODO: more options for user to specify output file location/name, default to input folder, etc.
                Console.WriteLine(string.Empty);
                Console.WriteLine("Enter output file folder:");
                var outputFilePath = Console.ReadLine();

                //TODO: add guard rails for output path location, permissions, overwrite, etc.
                var fullOutputPathAndFileName = Path.Combine(outputFilePath, $"records.{DateTime.Now:yyyyMMddHHmmss}.{outputFormat.ToString().ToLower()}");
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
    }
}
