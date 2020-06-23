using System;

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

        }
    }
}
