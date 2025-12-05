using PaperBuddy.Web.Common.Abstractions;
using UglyToad.PdfPig;

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

    public Task<string> ExtractTextAsync(byte[] pdfData)
    {
        throw new NotImplementedException();
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
}