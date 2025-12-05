using PaperBuddy.Web.Domain.Entities;

namespace PaperBuddy.Web.Domain;

public class Note : TrackedEntity
{
    public Guid UserId { get; set; }
    public ParentType ParentType { get; set; }
    public Guid ParentId { get; set; }
    public string Content { get; set; } = string.Empty;
}