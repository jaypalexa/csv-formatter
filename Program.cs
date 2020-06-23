using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using CsvHelper;

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
            var csvInputFileName = Console.ReadLine();
            //TODO: sanitize/validate file name, validate that it is a CSV file, etc.

            Console.WriteLine($"You entered: {csvInputFileName}");

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

            using (var reader = new StreamReader(csvInputFileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                //var records = new List<Foo>();
                csv.Read();
                csv.ReadHeader();
                var headers = csv.Context.HeaderRecord;

                while (csv.Read())
                {
                    for (int i = 0; i < headers.Length; i++)
                    {
                        Console.WriteLine($"Header: {headers[i]}; Value: {csv.GetField(i)}");
                    }
                    //var record = new Foo
                    //{
                    //    Id = csv.GetField<int>("Id"),
                    //    Name = csv.GetField("Name")
                    //};
                    //records.Add(record);
                }
            }

            //dynamic dynObject = new ExpandoObject();

            //// name,address_line1,address_line2,description,reason,another_thing1,another_thing2
            //var headers = "name,address_line1,address_line2,description,reason,another_thing1,another_thing2";
            //var headerArray = headers.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            //foreach (var header in headerArray)
            //{
            //    Console.WriteLine(header);
            //    DoAddProperty(dynObject, header);
            //}


        }

        static void DoAddProperty(dynamic dynObject, string propertyName)
        {
            //TODO:  null checking here

            if (propertyName.Contains("_"))
            {
            }
            else
            {

            }
        }
    }
}
