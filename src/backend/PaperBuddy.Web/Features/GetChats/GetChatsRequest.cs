using PaperBuddy.Web.Domain;

namespace PaperBuddy.Web.Features.GetChats;

public record GetChatsRequest(Guid ParentId, string ParentType);