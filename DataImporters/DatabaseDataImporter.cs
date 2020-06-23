using System;
using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient; // import NuGet package
using System.Dynamic;
using CsvFormatter.DataImporters.Interfaces;

namespace CsvFormatter.DataImporters
{
    public class DatabaseDataImporter : IDataImporter
    {
        public List<ExpandoObject> Import()
        {
            /* TODO:  read from database into DataTable here...something like this...
                var records = new List<ExpandoObject>();
                var dataTable = new DataTable();
                using (var dataAdapter = new SqlDataAdapter("SELECT * FROM {{some-table}}", "{{connection-string}}"))
                {
                    dataAdapter.Fill(dataTable);
                    records = CsvParserHelper.Parse(dataTable);
                }
                return records;
            */

            throw new NotSupportedException("Database import not yet supported");
        }
    }
}
