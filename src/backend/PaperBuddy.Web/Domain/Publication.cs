using PaperBuddy.Web.Domain.Entities;

namespace PaperBuddy.Web.Domain;

public abstract class Publication : TrackedEntity
{
    public string Title { get; set; } = string.Empty;
    public string Authors { get; set; } = string.Empty;
    public int? Year { get; set; }
    public string Doi { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public Guid UploadedBy { get; set; }
}