using System;

namespace Nutrition
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var pdfHelper = new PdfHelper("sources/recetas.pdf");
            var textString = pdfHelper.Read();

            var convertService = new ConvertService();

            convertService.RecetasConverter(textString);

            Console.ReadKey();
        }
    }
}
