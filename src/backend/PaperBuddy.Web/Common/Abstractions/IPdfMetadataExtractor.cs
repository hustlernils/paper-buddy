namespace PaperBuddy.Web.Common.Abstractions;

public interface IPdfMetadataExtractor
{
    Task<PdfMetadata> ExtractMetadataAsync(byte[] pdfData);
    Task<List<string>> ExtractTextAsync(byte[] pdfData);
    Task<List<string>> ExtractParagraphsAsync(byte[] pdfData);
}

public record PdfMetadata(string Title, string Authors, int? Year);