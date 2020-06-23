using System.Collections.Generic;
using System.Dynamic;

namespace CsvFormatter.OutputBuilders.Interfaces
{
    public interface IOutputBuilder
    {
        public string Build(List<ExpandoObject> records);
    }
}
