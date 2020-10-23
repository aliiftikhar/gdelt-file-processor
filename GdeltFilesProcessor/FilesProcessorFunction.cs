using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GdeltFilesProcessor.Core.UseCases.ProcessFile;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GdeltFilesProcessor
{
    public class FilesProcessorFunction
    {
        private readonly IProcessFileHandler processFileHandler;
        //private readonly ILogger log;

        public FilesProcessorFunction(IProcessFileHandler processFileHandler)//, ILogger log)
        {
            this.processFileHandler = processFileHandler;
            //this.log = log;
        }

        [FunctionName("FilesProcessorFunction")]
        public async Task Run([QueueTrigger("gdelt-files-for-download", Connection = "AzureWebJobsStorage")]string myQueueItem)
        {
            await ProcessMessage(myQueueItem);

            //this.log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }

        private async Task ProcessMessage(string message)
        {
            var request = JsonSerializer.Deserialize<ProcessFileRequest>(message);

            await this.processFileHandler.Handle(request);
        }
    }
}
