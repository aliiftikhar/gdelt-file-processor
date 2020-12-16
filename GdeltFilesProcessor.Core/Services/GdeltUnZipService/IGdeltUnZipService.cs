using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GdeltFilesProcessor.Core.Services.GdeltUnZipService
{
    public interface IGdeltUnZipService
    {
        Stream UnZip(Stream zipStream);
    }
}
