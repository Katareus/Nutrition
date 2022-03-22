using System;
using System.Diagnostics;

namespace Nutrition
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Converting...");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            var pdfHelper = new PdfHelper("sources/recetas.pdf");
            var textString = pdfHelper.Read();

            var convertService = new ConvertService();

            convertService.RecetasConverter(textString);

            stopWatch.Stop();
            var ts = stopWatch.Elapsed;
            Console.WriteLine($"Converted in { ts.Seconds } seconds and { ts.Milliseconds } milliseconds.");
            Console.ReadKey();
        }
    }
}
