using System.Collections.Generic;
using System.Dynamic;

namespace CsvFormatter.DataImporters.Interfaces
{
    public interface IDataImporter
    {
        public List<ExpandoObject> Import();
    }
}
