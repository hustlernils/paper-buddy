using PaperBuddy.Web.Domain.Entities;

namespace PaperBuddy.Web.Domain;

public enum MessageRole
{
    User,
    System
}

public class ChatMessage : TrackedEntity
{
    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }
    public MessageRole Role { get; set; }
    public string Content { get; set; } = string.Empty;
}