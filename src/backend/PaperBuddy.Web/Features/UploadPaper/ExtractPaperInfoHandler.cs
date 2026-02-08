using System.Data;
using Dapper;
using PaperBuddy.MessageBus.Abstractions;
using PaperBuddy.Web.Common.Abstractions;
using UglyToad.PdfPig;

namespace PaperBuddy.Web.Features.UploadPaper;

public class ExtractPaperInfoHandler(IDbConnection connection, ILogger<ExtractPaperInfoHandler> logger, IMessageBus messageBus, IPdfMetadataExtractor metadataExtractor) : IConsumer<ExtractPaperInfoRequest>
{
    private readonly IDbConnection _dbConnection = connection;
    private readonly ILogger<ExtractPaperInfoHandler> _logger = logger;
    private readonly IMessageBus _messageBus = messageBus;
    private readonly IPdfMetadataExtractor _pdfMetadataExtractor = metadataExtractor;

    public async Task ConsumeAsync(ExtractPaperInfoRequest message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Extracting info for paper {PaperId}", message.PaperId);

        _dbConnection.Open();

        var paperData = await _dbConnection.QuerySingleOrDefaultAsync<byte[]>(
            "SELECT data FROM paper_data WHERE paper_id = @PaperId",
            new { PaperId = message.PaperId });

        if (paperData == null)
        {
            _logger.LogWarning("No paper data found for {PaperId}", message.PaperId);
            return;
        }

        var pdfData = await _pdfMetadataExtractor.ExtractMetadataAsync(paperData);
        var fullText = await GetFullTextAsync(paperData);
        
        await _dbConnection.ExecuteAsync(
            @"UPDATE papers SET title = @Title, authors = @Authors, year = @Year, keywords = @Keywords WHERE id = @Id",
            new
            {
                Id = message.PaperId,
                Title = pdfData.Title,
                Authors = pdfData.Authors,
                Year = pdfData.Year,
                Keywords = pdfData.Keywords,
            });

        await _dbConnection.ExecuteAsync("UPDATE paper_data SET text_content = @TextContent WHERE paper_id = @Id",
            new { Id = message.PaperId, TextContent = fullText });
        
        _dbConnection.Close();
        
        _logger.LogInformation("Extracted and updated info for paper {PaperId}", message.PaperId);
        
        await _messageBus.PublishAsync(new SummarizePaperRequest(message.PaperId), cancellationToken);
        await _messageBus.PublishAsync(new CreatePaperEmbeddingsRequest(message.PaperId), cancellationToken);
    }

    private async Task<string> GetFullTextAsync(byte[] paperData)
    {
        var textParagraphs = await _pdfMetadataExtractor.ExtractParagraphsAsync(paperData);
        return string.Join(" ", textParagraphs);
    }
    
    private static int? GetYear(string? pdfDateString)
    {
        if (string.IsNullOrEmpty(pdfDateString) || !pdfDateString.StartsWith("D:"))
        {
            return null;
        }

        var dateStr = pdfDateString.Substring(2); // Remove "D:"
        if (dateStr.Length < 8)
        {
            return null;
        }

        var datePart = dateStr.Substring(0, 8); // YYYYMMDD
        if (DateTime.TryParseExact(datePart, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var date))
        {
            return date.Year;
        }

        return null;
    }
}