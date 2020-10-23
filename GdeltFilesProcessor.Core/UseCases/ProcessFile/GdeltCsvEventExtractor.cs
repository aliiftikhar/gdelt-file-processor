using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace GdeltFilesProcessor.Core.UseCases.ProcessFile
{
    public class GdeltCsvEventExtractor : IGdeltCsvEventExtractor
    {
        public GdeltEvent ConvertCsvLineToEvent(string csvLine, char csvSeperator)
        {
            var values = csvLine.Split(csvSeperator);

            var gdeltEvent = new GdeltEvent();

            if (values.Length == 1 && !string.IsNullOrWhiteSpace(values[0]))
                gdeltEvent.GlobalEventID = Convert.ToInt32(values[0]);

            return gdeltEvent;
        }
    }
}