using PaperBuddy.Web.Domain;

namespace PaperBuddy.Web.Features.GetChatMessages;

public class GetChatMessagesResponse
{
    public string Role { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}