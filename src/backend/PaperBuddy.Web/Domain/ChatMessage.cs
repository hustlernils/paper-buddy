using PaperBuddy.Web.Domain.Entities;

namespace PaperBuddy.Web.Domain;

public enum MessageRole
{
    User,
    Assistant
}

public class ChatMessage : TrackedEntity
{
    public ChatMessage(string content, MessageRole role, Guid chatId, Guid userId)
    {
        Content = content;
        Role = role;
        ChatId = chatId;
        UserId = userId;
    }

    public Guid ChatId { get; }
    public Guid UserId { get; }
    public MessageRole Role { get; }
    public string Content { get; } = string.Empty;

    public static ChatMessage CreateSystemMessage(Guid chatId, Guid userId, string content)
        => new(content, MessageRole.Assistant, chatId, userId);
}