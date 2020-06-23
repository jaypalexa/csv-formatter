using System;
using CsvFormatter.Enums;
using CsvFormatter.OutputBuilders.Interfaces;

namespace CsvFormatter.OutputBuilders
{
    public class OutputBuilderFactory
    {
        private readonly OutputFormat _outputFormat;

        public OutputBuilderFactory(OutputFormat outputFormat)
        {
            _outputFormat = outputFormat;
        }

        public IOutputBuilder Create()
        {
            return _outputFormat switch
            {
                OutputFormat.Json => new JsonOutputBuilder(),
                OutputFormat.Xml => new XmlOutputBuilder(),
                _ => throw new NotSupportedException($"Output format '{_outputFormat}' not supported")
            };
        }
    }
}
