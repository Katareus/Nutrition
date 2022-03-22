using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nutrition.Application.Interfaces.ServiceContracts;
using Nutrition.Application.Services.Services;
using System;
using System.Diagnostics;

namespace Nutrition
{
    class Program
    {
        private const string SourceFilePath = "sources/recetas.pdf";
        private const string TargetFilePath = "output/recetas.txt";

        static void Main(string[] args)
        {
            // Setting up our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IReaderService, ReaderService>()
                .AddSingleton<IFilterService, FilterService>()
                .AddSingleton<ITxtService, TxtService>()
                .BuildServiceProvider();

            //configure console logging
            serviceProvider
                .GetService<ILoggerFactory>();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //starting logic and timer
            Console.WriteLine("Converting...");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var readerService = serviceProvider.GetService<IReaderService>();
            var textString = readerService.Read(SourceFilePath);


            var filterService = serviceProvider.GetService<IFilterService>();
            var contentFiltered = filterService.GetContentFiltered(textString);

            var txtService = serviceProvider.GetService<ITxtService>();
            txtService.UpsertFile(TargetFilePath, contentFiltered);

            stopWatch.Stop();
            var ts = stopWatch.Elapsed;
            Console.WriteLine($"Converted in { ts.Seconds } seconds and { ts.Milliseconds } milliseconds.");
            Console.ReadKey();
        }
    }
}
