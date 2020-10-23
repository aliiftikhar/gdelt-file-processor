using GdeltFilesProcessor.Core.Services.QueueService;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net.Cache;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GdeltFilesProcessor.Core.UseCases.ProcessFile
{
    public class ProcessFileHandler : IProcessFileHandler
    {
        IGdeltCsvEventExtractor gdeltCsvEventExtractor;
        IQueueService queueService;

        public ProcessFileHandler(IGdeltCsvEventExtractor gdeltCsvEventExtractor, IQueueService queueService)
        {
            this.gdeltCsvEventExtractor = gdeltCsvEventExtractor;
            this.queueService = queueService;
        }

        public async Task Handle(ProcessFileRequest request)
        {
            ValidateRequest(request);

            using Stream zippedStream = await Download(request.DownloadUrl);

            using Stream unzippedStream = UnZip(zippedStream);

            using StreamReader streamReader = new StreamReader(unzippedStream);

            while (!streamReader.EndOfStream)
            {
                var gdeltEvent = this.gdeltCsvEventExtractor.ConvertCsvLineToEvent(streamReader.ReadLine(), '\t');

                await this.queueService.Queue(gdeltEvent);
            }
        }

        private void ValidateRequest(ProcessFileRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.DownloadUrl))
                throw new ArgumentNullException(nameof(request.DownloadUrl));
        }

        private async Task<Stream> Download(string downloadUrl)
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

        private Stream UnZip(Stream stream)
        {
            Stream unzippedStream = null;

            ZipArchive archive = new ZipArchive(stream);

            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                if (entry.FullName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    unzippedStream = entry.Open();
                    break;
                }
            }

            return unzippedStream;
        }
    }
}
