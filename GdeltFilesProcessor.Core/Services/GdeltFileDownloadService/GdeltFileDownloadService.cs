using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GdeltFilesProcessor.Core.Services.GdeltFileDownloadService
{
    public class GdeltFileDownloadService : IGdeltFileDownloadService
    {
        public async Task<Stream> Download(string downloadUrl)
        {
            Stream memoryStream = new MemoryStream();

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(downloadUrl))
                {
                    using var stream = await response.Content.ReadAsStreamAsync();
                    await stream.CopyToAsync(memoryStream);
                    return memoryStream;
                }
            }
        }
    }
}