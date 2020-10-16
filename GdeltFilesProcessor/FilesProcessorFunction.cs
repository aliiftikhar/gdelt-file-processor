using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GdeltFilesProcessor
{
    public static class FilesProcessorFunction
    {
        [FunctionName("FilesProcessorFunction")]
        public static void Run([QueueTrigger("gdelt-files-for-download", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
