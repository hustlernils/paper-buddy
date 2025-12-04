namespace PaperBuddy.Web.Domain;

public class PaperData
{
    public Guid Id { get; set; }
    public Guid PaperId { get; set; }
    public byte[] Data { get; set; } = Array.Empty<byte>();
    public DateTime CreatedAt { get; set; }
}