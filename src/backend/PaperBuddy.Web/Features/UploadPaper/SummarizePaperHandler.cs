using System.Data;
using Dapper;
using PaperBuddy.MessageBus.Abstractions;
using PaperBuddy.Web.Common.Abstractions;

namespace PaperBuddy.Web.Features.UploadPaper;

public class SummarizePaperHandler(ILogger<SummarizePaperHandler> logger, IDbConnection dbConnection, ISummarizationService summarizationService) : IConsumer<SummarizePaperRequest>
{
    private readonly IDbConnection _dbConnection = dbConnection;
    private readonly ISummarizationService _summarizationService = summarizationService;
    public async Task ConsumeAsync(SummarizePaperRequest message, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling paper with id: {message.PaperId}");
        
        _dbConnection.Open();
        
        string sql = "SELECT text_content FROM paper_data WHERE paper_id = @PaperId";

        var fullText = await _dbConnection.QueryFirstAsync<string>(sql, new { PaperId = message.PaperId });

        if (string.IsNullOrEmpty(fullText))
        {
            return;
        }
        
        string summary = await _summarizationService.SummarizeAsync(fullText);
        
        await _dbConnection.ExecuteAsync("UPDATE papers SET summary = @Summary WHERE id = @PaperId", new { PaperId = message.PaperId, Summary = summary });
        
        _dbConnection.Close();
    }
}