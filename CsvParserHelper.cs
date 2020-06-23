using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;

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
                    Console.WriteLine($"Header: {col.ColumnName}; Value: {row[col.ColumnName]}");
                    DoAddProperty(record, col.ColumnName, Convert.ToString(row[col.ColumnName]));
                }
                records.Add(record);
            }

            return records;
        }

        private static void DoAddProperty(dynamic outputRecord, string propertyName, string propertyValue)
        {
            //TODO: null checking, weird underscore position checking, etc.

            if (propertyName.Contains("_"))
            {
                var parentPropertyName = propertyName.Split('_')[0];
                var childPropertyName = propertyName.Split('_')[1];
                if (!((IDictionary<string, object>)outputRecord).ContainsKey(parentPropertyName))
                {
                    ((IDictionary<string, object>)outputRecord)[parentPropertyName] = new ExpandoObject();
                }
                DoAddProperty(((IDictionary<string, object>)outputRecord)[parentPropertyName], childPropertyName, propertyValue);
            }
            else
            {
                ((IDictionary<string, object>)outputRecord)[propertyName] = propertyValue;
            }
        }
    }
}
