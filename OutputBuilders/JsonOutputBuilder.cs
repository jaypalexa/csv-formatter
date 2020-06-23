using System.Collections.Generic;
using System.Dynamic;
using CsvFormatter.OutputBuilders.Interfaces;
using Newtonsoft.Json;

namespace CsvFormatter.OutputBuilders
{
    public class JsonOutputBuilder : IOutputBuilder
    {
        public string Build(List<ExpandoObject> records)
        {
            return JsonConvert.SerializeObject(records);
        }
    }
}
