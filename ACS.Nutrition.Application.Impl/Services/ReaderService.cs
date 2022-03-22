using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using ACS.Nutrition.Application.Contracts.Services;
using System.IO;
using System.Text;

namespace ACS.Nutrition.Application.Impl.Services
{
    public class ReaderService: IReaderService
    {
        public string Read(string filePath)
        {
            var alltextFromPage = string.Empty;

            if (File.Exists(filePath))
            {
                var pdfReader = new PdfReader(filePath);

                for (int i = 0; i < pdfReader.NumberOfPages; i++)
                {
                    // Converting text from pdf to string
                    var locationTextExtractionStrategy = new LocationTextExtractionStrategy();
                    var textFromPage = PdfTextExtractor.GetTextFromPage(pdfReader, i + 1, locationTextExtractionStrategy);
                    alltextFromPage += Encoding.UTF8.GetString(Encoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(textFromPage)));
                }
            }

            return alltextFromPage;
        }
    }
}
