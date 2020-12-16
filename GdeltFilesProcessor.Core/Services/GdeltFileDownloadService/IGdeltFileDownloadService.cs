using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GdeltFilesProcessor.Core.Services.GdeltFileDownloadService
{
    public interface IGdeltFileDownloadService
    {
        Task<Stream> Download(string downloadUrl);
    }
}
