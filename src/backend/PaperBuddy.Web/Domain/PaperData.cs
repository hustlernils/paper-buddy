using PaperBuddy.Web.Domain.Entities;

namespace PaperBuddy.Web.Domain;

public class PaperData : TrackedEntity
{
    public Guid PaperId { get; set; }
    public byte[] Data { get; set; } = Array.Empty<byte>();
}