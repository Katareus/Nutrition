using Nutrition.Application.Interfaces.ServiceContracts;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Nutrition.Application.Services.Services
{
    public class FilterService : IFilterService
    {
        public string GetContentFiltered(string textContent)
        {
            textContent = DeletingTitles(textContent);

            var textLines = textContent.Split("\n").Select(l => l.TrimStart().TrimEnd()).ToList();

            textLines.RemoveAll(x => RemovingTitles(x));

            textLines = textLines.Select(x => AvoidingSauceTitles(x)).ToList();

            textLines = textLines.Select(l => l.TrimStart().TrimEnd()).ToList();

            var content = string.Join("\n", textLines).Replace(". ", "\n").Replace(", ", "\n").Replace("g.", "g").Replace(" g", "g");

            return content;
        }

        private string AvoidingSauceTitles(string x)
        {
            return x.Contains(":") ? x.Split(":")[1] : x; ;
        }

        private string DeletingTitles(string x)
        {
            return x.Replace("Nombre del alimento:", "")
                .Replace("Composición:", "");
        }

        private bool RemovingTitles(string x)
        {
            x = x.TrimStart().TrimEnd();

            return x.StartsWith("FICHA")
                || x.StartsWith("RECETAS")
                || x.StartsWith("Nº")
                || string.IsNullOrEmpty(x);
        }
    }
}
