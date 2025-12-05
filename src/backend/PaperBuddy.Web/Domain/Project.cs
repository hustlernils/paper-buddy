using PaperBuddy.Web.Domain.Entities;

namespace PaperBuddy.Web.Domain;

public class Project : TrackedEntity
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}