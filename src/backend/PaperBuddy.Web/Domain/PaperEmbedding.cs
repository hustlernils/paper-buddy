using PaperBuddy.Web.Domain.Entities;

namespace PaperBuddy.Web.Domain;

public class PaperEmbedding : TrackedEntity
{
    public Guid PaperId { get; set; }
    public float[] Embedding { get; set; } = Array.Empty<float>();
    public string ChunkId { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}