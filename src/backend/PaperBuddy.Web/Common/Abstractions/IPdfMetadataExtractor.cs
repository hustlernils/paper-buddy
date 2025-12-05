namespace PaperBuddy.Web.Common.Abstractions;

public interface IPdfMetadataExtractor
{
    Task<PdfMetadata> ExtractMetadataAsync(byte[] pdfData);
    Task<string> ExtractTextAsync(byte[] pdfData);
}

public record PdfMetadata(string Title, string Authors, int? Year);