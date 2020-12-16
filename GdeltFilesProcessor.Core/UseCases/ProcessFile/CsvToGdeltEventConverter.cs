using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace GdeltFilesProcessor.Core.UseCases.ProcessFile
{
    public class CsvToGdeltEventConverter : ICsvToGdeltEventConverter
    {
        //GDELT column reference: http://data.gdeltproject.org/documentation/GDELT-Event_Codebook-V2.0.pdf

        public GdeltEvent ConvertCsvLineToGdeltEvent(string csvLine, char csvSeperator)
        {
            var values = csvLine.Split(csvSeperator);

            var gdeltEvent = new GdeltEvent();

            if (values.Length > 0 && !string.IsNullOrWhiteSpace(values[0]))
                gdeltEvent.GlobalEventID = Convert.ToInt32(values[0]);

            if (values.Length > 1 && !string.IsNullOrWhiteSpace(values[1]))
                gdeltEvent.Day = Convert.ToInt32(values[1]);

            return gdeltEvent;
        }
    }
}