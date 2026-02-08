using System.Data;
using Dapper;
using PaperBuddy.MessageBus.Abstractions;
using PaperBuddy.Web.Common.Abstractions;
using PaperBuddy.Web.Domain;
using PaperBuddy.Web.Infrastructure.Services;

namespace PaperBuddy.Web.Features.UploadPaper.Embeddings;

public class CreatePaperEmbeddingsHandler(IDbConnection connection, ChunkingService chunking, IEmbeddingService embeddingService) : IConsumer<CreatePaperEmbeddingsRequest>
{
    private readonly IDbConnection _dbConnection = connection;
    private readonly ChunkingService _chunkingService = chunking;
    private readonly IEmbeddingService _embeddingService = embeddingService;
    
    public async Task ConsumeAsync(CreatePaperEmbeddingsRequest message, CancellationToken cancellationToken)
    {
        _dbConnection.Open();

        var fullText = await QueryPaperTextContent(message.PaperId);
        
        var chunks = _chunkingService.GetChunks(fullText);

        var paperEmbeddings = await CreatePaperEmbeddings(chunks, message.PaperId);
        
        await InsertPaperEmbeddings(paperEmbeddings);
        
        _dbConnection.Close();
    }

    private async Task<string> QueryPaperTextContent(Guid paperId)
    {
        string sql = "SELECT text_content FROM paper_data WHERE paper_id = @PaperId";
        var fullText = await _dbConnection.QueryFirstAsync<string>(sql, new { paperId });
        
        return fullText;
    }
    
    private async Task InsertPaperEmbeddings(List<PaperEmbedding> paperEmbeddings)
    {
        string insertSql =
            "INSERT INTO paper_embeddings (id, paper_id, embedding, chunk_id, content, created_at) VALUES (@Id, @PaperId, @Embedding, @ChunkId, @Content, @CreatedAt)";
        
        await _dbConnection.ExecuteAsync(insertSql, paperEmbeddings);
    }
    
    private async Task<List<PaperEmbedding>> CreatePaperEmbeddings(TextChunk[] chunks, Guid paperId)
    {        
        var paperEmbeddings = new List<PaperEmbedding>();
        
        foreach (var chunk in chunks)
        {
            float[] embedding = await _embeddingService.GetEmbeddingAsync(chunk.Text);

            var paperEmbedding = PaperEmbedding.FromTextChunk(chunk, paperId, embedding);
            
            paperEmbeddings.Add(paperEmbedding);
        }
        
        return paperEmbeddings;
    }
}