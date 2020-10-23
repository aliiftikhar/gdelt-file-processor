using GdeltFilesProcessor.Core.UseCases.ProcessFile;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(GdeltFilesProcessor.Startup))]

namespace GdeltFilesProcessor
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IGdeltCsvEventExtractor, GdeltCsvEventExtractor>();
            builder.Services.AddTransient<IProcessFileHandler, ProcessFileHandler>();
        }
    }
}
