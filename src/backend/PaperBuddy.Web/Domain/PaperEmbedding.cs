using PaperBuddy.Web.Domain.Entities;
using PaperBuddy.Web.Infrastructure.Services;

namespace PaperBuddy.Web.Domain;

public class PaperEmbedding : TrackedEntity
{
    public Guid PaperId { get; set; }
    public float[] Embedding { get; set; } = Array.Empty<float>();
    public string ChunkId { get; private set; } = string.Empty;
    public string Text { get; set; } = string.Empty;

    public static PaperEmbedding FromTextChunk(TextChunk textChunk, Guid paperId, float[] embedding)
        => new PaperEmbedding
        {
            PaperId = paperId,
            Embedding = embedding,
            Text = textChunk.Text,
            ChunkId = $"{paperId}_{textChunk.ChunkIndex}"
        };
}