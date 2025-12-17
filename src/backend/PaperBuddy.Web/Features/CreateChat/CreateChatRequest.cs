using PaperBuddy.Web.Domain;

namespace PaperBuddy.Web.Features.CreateChat;

public record CreateChatRequest(string ParentType, Guid ParentId);