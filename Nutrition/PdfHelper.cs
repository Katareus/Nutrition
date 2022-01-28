using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.util;

namespace Nutrition
{
    public class PdfHelper
    {
        private string _filePath;

        public PdfHelper(string filePath)
        {
            _filePath = filePath;
        }

        public String Read()
        {
            var alltextFromPage = string.Empty;

            if (File.Exists(_filePath))
            {
                var pdfReader = new PdfReader(_filePath);

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

        /// <summary>
        /// Lê uma tabela de um pdf
        /// </summary>
        /// <param name="pdf">Caminho do PDF</param>
        /// <param name="origemXPag1">Inicio da leitura no eixo X para a primeira página</param>
        /// <param name="origemYPag1">Inicio da leitura no eixo Y para a primeira página</param>
        /// <param name="linhasPag1">Quantidade de linhas da primeira página</param>
        /// <param name="origemXOutrasPag">Inicio da leitura no eixo X para as demais páginas</param>
        /// <param name="origemYOutrasPag">Inicio da leitura no eixo Y para as demais páginas</param>
        /// <param name="linhasOutrasPag">Quantidade de linhas das demais páginas</param>
        /// <param name="alturaLinha">Altrura da linha</param>
        /// <param name="colunas">Nome e largura das colunas</param>
        /// <returns></returns>
        private static List<Dictionary<string, string>> LerTabelaPDF(string pdf, float origemXPag1, float origemYPag1, int linhasPag1, float origemXOutrasPag, float origemYOutrasPag, int linhasOutrasPag, float alturaLinha, Dictionary<string, float> colunas)
        {
            // Primeira página
            float origemX = origemXPag1;
            float origemY = origemYPag1;
            int quantidadeLinhas = linhasPag1;

            var resultado = new List<Dictionary<string, string>>();
            using (PdfReader leitor = new PdfReader(pdf))
            {
                var texto = string.Empty;
                for (int i = 1; i <= leitor.NumberOfPages; i++)
                {
                    if (i > 1)
                    {
                        origemX = origemXOutrasPag;
                        origemY = origemYOutrasPag;
                        quantidadeLinhas = linhasOutrasPag;
                    }
                    for (int l = 0; l < quantidadeLinhas; l++)
                    {
                        var dados = new Dictionary<string, string>();
                        int c = 0;
                        float deslocamentoX = 0;
                        foreach (var coluna in colunas)
                        {
                            RectangleJ rect = new RectangleJ(origemX + deslocamentoX, origemY + (l * alturaLinha), coluna.Value, alturaLinha);
                            RenderFilter filter = new RegionTextRenderFilter(rect);
                            ITextExtractionStrategy strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filter);
                            texto = PdfTextExtractor.GetTextFromPage(leitor, i, strategy);

                            dados.Add(coluna.Key, texto);
                            c++;
                            deslocamentoX += coluna.Value;
                        }
                        if (dados != null)
                            resultado.Add(dados);
                    }
                }
            }
            return resultado;
        }
    }
}
