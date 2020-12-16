using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace GdeltFilesProcessor.Core.UseCases.ProcessFile
{
    public class CsvToGdeltEventConverter : ICsvToGdeltEventConverter
    {
        public GdeltEvent ConvertCsvLineToGdeltEvent(string csvLine, char csvSeperator)
        {
            var values = csvLine.Split(csvSeperator);

            var gdeltEvent = new GdeltEvent();

            if (values.Length > 0 && !string.IsNullOrWhiteSpace(values[0]))
                gdeltEvent.GlobalEventID = Convert.ToInt32(values[0]);

            return gdeltEvent;
        }
    }
}