using System;
using System.Collections.Generic;
using System.Text;

namespace GdeltFilesProcessor.Core.UseCases.ProcessFile
{
    public interface ICsvToGdeltEventConverter
    {
        GdeltEvent ConvertCsvLineToGdeltEvent(string csvLine, char csvSeperator);
    }
}
