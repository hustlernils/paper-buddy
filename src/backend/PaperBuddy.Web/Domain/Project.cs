using PaperBuddy.Web.Domain.Entities;

namespace PaperBuddy.Web.Domain;

public class Project : TrackedEntity
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }

    public Project(Guid userId, string title, string? description = null)
    {
        UserId = userId;
        Title = title;
        Description = description;
    }
}