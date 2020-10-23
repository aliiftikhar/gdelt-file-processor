using GdeltFilesProcessor.Core.Services.QueueService;
using GdeltFilesProcessor.Core.UseCases.ProcessFile;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GdeltFilesProcessor.Tests.Core.UseCases.ProcessFile
{
    public class ProcessFileHandlerTests
    {
        private IProcessFileHandler processFileHandler;
        private Mock<IGdeltCsvEventExtractor> gdeltCsvEventExtractor;
        private Mock<IQueueService> queueService;

        [SetUp]
        public void Setup()
        {
            this.gdeltCsvEventExtractor = new Mock<IGdeltCsvEventExtractor>();
            this.queueService = new Mock<IQueueService>();

            this.gdeltCsvEventExtractor.Setup(x => x.ConvertCsvLineToEvent(It.IsAny<string>(), It.IsAny<char>())).Returns(
                new GdeltEvent {
                    GlobalEventID = 10
                });

            this.processFileHandler = new ProcessFileHandler(gdeltCsvEventExtractor.Object, queueService.Object);
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

        //[Test]
        //public async Task Handle_should_queue_correct_event()
        //{
        //    await this.processFileHandler.Handle(
        //        new ProcessFileRequest
        //        {
        //            DownloadUrl = "http://data.gdeltproject.org/events/20201014.export.CSV.zip"
        //        });

        //    this.gdeltCsvEventExtractor.Verify(x=>x.ConvertCsvLineToEvent(It.Is<string>, '\t')).
        //}
    }
}
