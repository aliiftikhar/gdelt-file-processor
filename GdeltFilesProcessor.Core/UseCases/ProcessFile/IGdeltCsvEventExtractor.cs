using System;
using System.Collections.Generic;
using System.Text;

namespace GdeltFilesProcessor.Core.UseCases.ProcessFile
{
    public interface IGdeltCsvEventExtractor
    {
        GdeltEvent ConvertCsvLineToEvent(string csvLine, char csvSeperator);
    }
}
