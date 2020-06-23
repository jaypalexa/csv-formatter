using System.Collections.Generic;
using System.Dynamic;
using System.Xml.Linq;
using CsvFormatter.OutputBuilders.Interfaces;
using Newtonsoft.Json;

namespace CsvFormatter.OutputBuilders
{
    public class XmlOutputBuilder : IOutputBuilder
    {
        public string Build(List<ExpandoObject> records)
        {
            var outputJsonForXml = JsonConvert.SerializeObject(records);
            XNode node = JsonConvert.DeserializeXNode($"{{\"Record\":{outputJsonForXml}}}", "Records");
            return node.ToString();
        }
    }
}
