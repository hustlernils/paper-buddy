using PaperBuddy.Web.Domain;

namespace PaperBuddy.Web.Features.GetChatMessages;

public record GetChatMessagesResponse(
    string Role,
    string Content,
    DateTime CreatedAt
);