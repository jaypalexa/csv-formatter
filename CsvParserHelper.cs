using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using CsvFormatter.Enums;

namespace CsvFormatter
{
    public class CsvParserHelper
    {
        public static List<ExpandoObject> Parse(DataTable dataTable)
        {
            var records = new List<ExpandoObject>();

            foreach (DataRow row in dataTable.Rows)
            {
                dynamic record = new ExpandoObject();
                foreach (DataColumn col in dataTable.Columns)
                {
                    // Console.WriteLine($"Header: {col.ColumnName}; Value: {row[col.ColumnName]}");
                    DoAddProperty(record, col.ColumnName, Convert.ToString(row[col.ColumnName]));
                }
                records.Add(record);
            }

            return records;
        }

        public static OutputFormat GetOutputFormat(string outputFormatAsString)
        {
            //TODO: sanitize/validate input to make sure it is valid, is within enum range, etc.
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

        private static void DoAddProperty(dynamic outputRecord, string propertyName, string propertyValue)
        {
            //TODO: null checking, weird underscore position checking, etc.

            var record = (IDictionary<string, object>)outputRecord;

            if (propertyName.Contains("_"))
            {
                var parentPropertyName = propertyName.Split('_')[0];
                var childPropertyName = propertyName.Split('_')[1];
                if (!record.ContainsKey(parentPropertyName))
                {
                    record[parentPropertyName] = new ExpandoObject();
                }
                DoAddProperty(record[parentPropertyName], childPropertyName, propertyValue);
            }
            else
            {
                record[propertyName] = propertyValue;
            }
        }
    }
}
