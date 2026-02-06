using System.Data;
using Dapper;
using PaperBuddy.MessageBus.Abstractions;
using PaperBuddy.Web.Infrastructure.Services;

namespace PaperBuddy.Web.Features.UploadPaper;

public class CreatePaperEmbeddingsHandler(IDbConnection connection, ChunkingService chunking) : IConsumer<SummarizePaperRequest>
{
    private readonly IDbConnection _dbConnection = connection;
    private readonly ChunkingService _chunkingService = chunking;

    public async Task ConsumeAsync(SummarizePaperRequest message, CancellationToken cancellationToken)
    {
        _dbConnection.Open();

        string sql = "SELECT text_content FROM paper_data WHERE paper_id = @PaperId";
        var fullText = await _dbConnection.QueryFirstAsync<string>(sql, new { PaperId = message.PaperId });

        var chunks = _chunkingService.GetChunks(fullText);
        
        _dbConnection.Close();
        
        // remove this once fully implemented :)
        return Task.CompletedTask;
    }
}