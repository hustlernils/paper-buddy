namespace PaperBuddy.Web.Domain;

public class PaperEmbedding
{
    public Guid Id { get; set; }
    public Guid PaperId { get; set; }
    public float[] Embedding { get; set; } = Array.Empty<float>();
    public string ChunkId { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}