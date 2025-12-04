using System.Data;
using Dapper;
using PaperBuddy.MessageBus.Abstractions;
using UglyToad.PdfPig;

namespace PaperBuddy.Web.Features.UploadPaper;

public class ExtractPaperInfoHandler(IDbConnection connection, ILogger<ExtractPaperInfoHandler> logger, IMessageBus messageBus) : IConsumer<ExtractPaperInfoRequest>
{
    private readonly IDbConnection _dbConnection = connection;
    private readonly ILogger<ExtractPaperInfoHandler> _logger = logger;
    private readonly IMessageBus _messageBus = messageBus;

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

        using var ms = new MemoryStream(paperData);
        using var document = PdfDocument.Open(ms);

        var info = document.Information;

        var title = info.Title ?? "Unknown Title";
        var authors = info.Author ?? "Unknown Author";
        var year = GetYear(info.CreationDate);

        await _dbConnection.ExecuteAsync(
            @"UPDATE papers SET title = @Title, authors = @Authors, year = @Year WHERE id = @Id",
            new
            {
                Id = message.PaperId,
                Title = title,
                Authors = authors,
                Year = year
            });

        _logger.LogInformation("Extracted and updated info for paper {PaperId}", message.PaperId);
        
        await _messageBus.PublishAsync(new SummarizePaperRequest(message.PaperId), cancellationToken);
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