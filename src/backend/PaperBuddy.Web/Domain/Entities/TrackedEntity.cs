namespace PaperBuddy.Web.Domain.Entities;

public class TrackedEntity : Entity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}