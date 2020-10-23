using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GdeltFilesProcessor.Core.Services.QueueService
{
    public interface IQueueService
    {
        Task Queue<T>(T message);
    }
}
