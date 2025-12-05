using PaperBuddy.Web.Common.Abstractions;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;
using UglyToad.PdfPig.Content;

namespace PaperBuddy.Web.Infrastructure.Services;

public class PdfMetadataExtractor : IPdfMetadataExtractor
{
    public async Task<PdfMetadata> ExtractMetadataAsync(byte[] pdfData)
    {
        return await Task.Run(() =>
        {
            using var ms = new MemoryStream(pdfData);
            using var document = PdfDocument.Open(ms);

            var info = document.Information;

            var title = info.Title ?? "Unknown Title";
            var authors = info.Author ?? "Unknown Author";
            var year = GetYear(info.CreationDate);

            return new PdfMetadata(title, authors, year);
        });
    }

    public async Task<List<string>> ExtractTextAsync(byte[] pdfData)
    {
        return await Task.Run(() =>
        {
            using var ms = new MemoryStream(pdfData);
            using var document = PdfDocument.Open(ms);

            return ReadAllText(document);
        });
    }

    public async Task<List<string>> ExtractParagraphsAsync(byte[] pdfData)
    {
        return await Task.Run(() =>
        {
            List<string> paragraphs = [];
            
            using var ms = new MemoryStream(pdfData);
            using var document = PdfDocument.Open(ms);
            
            for (var i = 0; i < document.NumberOfPages; i++)
            {
                var page = document.GetPage(i + 1);
                var words = page.GetWords();

                // Use default parameters
                // - within line angle is set to [-30, +30] degree (horizontal angle)
                // - between lines angle is set to [45, 135] degree (vertical angle)
                // - between line multiplier is 1.3
                var blocks = DocstrumBoundingBoxes.Instance.GetBlocks(words);

                foreach (var block in blocks)
                {
                    paragraphs.Add(block.Text);
                }
            }
                
            return paragraphs;
        });
    }
    
    private static int? GetYear(string? pdfDateString)
    {
        if (string.IsNullOrEmpty(pdfDateString) || !pdfDateString.StartsWith("D:"))
        {
            return null;
        }

        var dateStr = pdfDateString.Substring(2);
        if (dateStr.Length < 8)
        {
            return null;
        }

        var datePart = dateStr.Substring(0, 8);
        if (DateTime.TryParseExact(datePart, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var date))
        {
            return date.Year;
        }

        return null;
    }
    
    private List<string> ReadAllText(PdfDocument document)
    {
        return document.GetPages().Select(p => p.Text).ToList();
    }
}