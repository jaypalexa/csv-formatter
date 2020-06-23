using System;
using CsvFormatter.Enums;
using CsvFormatter.DataImporters.Interfaces;

namespace CsvFormatter.DataImporters
{
    public class DataImporterFactory
    {
        private readonly InputSource _inputSource;

        public DataImporterFactory(InputSource inputSource)
        {
            _inputSource = inputSource;
        }

        public IDataImporter Create()
        {
            return _inputSource switch
            {
                InputSource.CsvFile => new CsvFileDataImporter(),
                InputSource.Database => new DatabaseDataImporter(),
                _ => throw new NotSupportedException($"Output format '{_inputSource}' not supported")
            };
        }
    }
}
