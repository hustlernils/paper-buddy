using PaperBuddy.Web.Domain;

namespace PaperBuddy.Web.Features.AddChatMessage;

public record AddChatMessageRequest(
    Guid ChatId,
    MessageRole Role,
    string Content
);