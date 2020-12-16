using GdeltFilesProcessor.Core.Services.GdeltFileDownloadService;
using GdeltFilesProcessor.Core.Services.GdeltUnZipService;
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
        IGdeltFileDownloadService fileDownloader;
        IGdeltUnZipService zipService;
        ICsvToGdeltEventConverter csvToGdeltEventConverter;
        IQueueService queueService;

        public ProcessFileHandler(IGdeltFileDownloadService fileDownloader, IGdeltUnZipService zipService, ICsvToGdeltEventConverter gdeltCsvEventExtractor, IQueueService queueService)
        {
            this.fileDownloader = fileDownloader;
            this.zipService = zipService;
            this.csvToGdeltEventConverter = gdeltCsvEventExtractor;
            this.queueService = queueService;
        }

        public async Task Handle(ProcessFileRequest request)
        {
            ValidateRequest(request);

            using Stream zippedStream = await this.fileDownloader.Download(request.DownloadUrl);

            using Stream unzippedStream = this.zipService.UnZip(zippedStream);

            using StreamReader streamReader = new StreamReader(unzippedStream);

            while (!streamReader.EndOfStream)
            {
                var gdeltEvent = this.csvToGdeltEventConverter.ConvertCsvLineToGdeltEvent(streamReader.ReadLine(), '\t');

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
    }
}
