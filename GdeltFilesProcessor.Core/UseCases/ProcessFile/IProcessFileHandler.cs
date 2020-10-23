using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GdeltFilesProcessor.Core.UseCases.ProcessFile
{
    public interface IProcessFileHandler
    {
        Task Handle(ProcessFileRequest request);
    }
}
