using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using CsvHelper;
using Newtonsoft.Json;

namespace CsvFormatter
{
    enum OutputFormatType
    {
        Json = 1,
        Xml = 2
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter CSV input file name:");
            var fullInputPathAndFileName = Console.ReadLine();
            //TODO: sanitize/validate file name, validate that it is a CSV file, etc.

            Console.WriteLine($"You entered: {fullInputPathAndFileName}");
            var inputPath = Path.GetDirectoryName(fullInputPathAndFileName);
            var inputFileName = Path.GetFileName(fullInputPathAndFileName);

            //TODO: put this prompt in a loop in case input is not valid
            Console.WriteLine("Enter 1 to format output as JSON");
            Console.WriteLine("Enter 2 to format output as XML");
            Console.WriteLine("Enter 0 to quit");
            var outputFormatAsString = Console.ReadLine();
            //TODO: sanitize/validate input to make sure it is 0 or 1 or 2 only, is within enum range, etc.

            if (outputFormatAsString.Equals("0")) return;

            //TODO: refactor this into its own method
            if (Enum.TryParse(outputFormatAsString, true, out OutputFormatType outputFormatType))
            {
                if (Enum.IsDefined(typeof(OutputFormatType), outputFormatType))
                {
                    Console.WriteLine($"Converted '{outputFormatAsString}' to {outputFormatType}.");
                }
                else
                {
                    Console.WriteLine($"{outputFormatAsString} is not an underlying value of the {nameof(OutputFormatType)} enumeration.");
                    return;
                }
            }
            else
            {
                Console.WriteLine($"{outputFormatAsString} is not a member of the {nameof(OutputFormatType)} enumeration.");
                return;
            }

            var outputRecords = new List<ExpandoObject>();

            using (var reader = new StreamReader(fullInputPathAndFileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                //TODO: safety checking around header values, file length, mismatched columns, etc.
                csv.Read();
                csv.ReadHeader();
                var headers = csv.Context.HeaderRecord;

                while (csv.Read())
                {
                    dynamic outputRecord = new ExpandoObject();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        Console.WriteLine($"Header: {headers[i]}; Value: {csv.GetField(i)}");
                        DoAddProperty(outputRecord, headers[i], csv.GetField(i));
                    }
                    outputRecords.Add(outputRecord);
                }
            }

            var outputJson = JsonConvert.SerializeObject(outputRecords);
            var fullOutputPathAndFileName = Path.Combine(inputPath, $"{inputFileName}.{DateTime.Now:yyyyMMddHHmmss}.{outputFormatType.ToString().ToLower()}");
            File.WriteAllText(fullOutputPathAndFileName, outputJson);
            Console.WriteLine($"Output file written to: {fullOutputPathAndFileName}");
        }

        static void DoAddProperty(dynamic outputRecord, string propertyName, string propertyValue)
        {
            //TODO: null checking here

            if (propertyName.Contains("_"))
            {
            }
            else
            {
                ((IDictionary<string, object>)outputRecord)[propertyName] = propertyValue;
            }
        }
    }
}
