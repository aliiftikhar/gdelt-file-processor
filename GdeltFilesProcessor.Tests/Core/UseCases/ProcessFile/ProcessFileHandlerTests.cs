using GdeltFilesProcessor.Core.Services.GdeltFileDownloadService;
using GdeltFilesProcessor.Core.Services.GdeltUnZipService;
using GdeltFilesProcessor.Core.Services.QueueService;
using GdeltFilesProcessor.Core.UseCases.ProcessFile;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GdeltFilesProcessor.Tests.Core.UseCases.ProcessFile
{
    public class ProcessFileHandlerTests
    {
        private IProcessFileHandler processFileHandler;
        private Mock<IGdeltFileDownloadService> fileDownloader;
        private Mock<IGdeltUnZipService> zipService;
        private Mock<ICsvToGdeltEventConverter> csvToGdeltConverter;
        private Mock<IQueueService> queueService;

        [SetUp]
        public void Setup()
        {
            this.fileDownloader = new Mock<IGdeltFileDownloadService>();
            this.zipService = new Mock<IGdeltUnZipService>();
            this.csvToGdeltConverter = new Mock<ICsvToGdeltEventConverter>();
            this.queueService = new Mock<IQueueService>();

            var stream = GenerateStreamFromString("some-string-in-a-stream");
            this.zipService.Setup(x => x.UnZip(It.IsAny<Stream>())).Returns(stream);

            this.csvToGdeltConverter.Setup(x => x.ConvertCsvLineToGdeltEvent(It.IsAny<string>(), It.IsAny<char>())).Returns(
                new GdeltEvent
                {
                    GlobalEventID = 10
                });

            this.processFileHandler = new ProcessFileHandler(fileDownloader.Object, zipService.Object, csvToGdeltConverter.Object, queueService.Object);
        }

        private static Stream GenerateStreamFromString(string stringInStream)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(stringInStream);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        [Test]
        public void Handle_should_throw_argument_exception_if_request_is_null()
        {
            Assert.ThrowsAsync<ArgumentNullException>(
                () => this.processFileHandler.Handle(null));
        }

        [Test]
        public void Handle_should_throw_argument_exception_if_downloadUrl_is_empty()
        {
            Assert.ThrowsAsync<ArgumentNullException>(
                () => this.processFileHandler.Handle(new ProcessFileRequest { DownloadUrl = "" }));
        }

        [Test]
        public async Task Handle_should_queue_correct_event()
        {
            await this.processFileHandler.Handle(
                new ProcessFileRequest
                {
                    DownloadUrl = "http://url"
                });

            this.queueService.Verify(x => x.Queue(It.Is<GdeltEvent>(p => p.GlobalEventID == 10)), Times.Once);
        }
    }
}
