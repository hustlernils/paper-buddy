using System.Data;
using Dapper;
using PaperBuddy.Web.Common;
using PaperBuddy.Web.Domain;

namespace PaperBuddy.Web.Features.AddChatMessage;

public class AddChatMessageHandler(IDbConnection connection) : RequestHandler<AddChatMessageRequest, AddChatMessageResponse>(connection)
{
    protected override async Task<AddChatMessageResponse> HandleAsync(AddChatMessageRequest request, CancellationToken cancellationToken)
    {
        var message = new ChatMessage()
        {
            Content = request.Content,
            Role = request.Role,
            ChatId = request.ChatId,
            UserId = new Guid("a3b99d2e-2fdf-4956-9690-cb6be5cf900a")
        };

        string sql = "INSERT INTO chat_messages (id, created_at, content, role, chat_id, user_id) VALUES " +
                     "(@Id, @CreatedAt, @Content, @Role, @ChatId, @UserId);";

        await Database.ExecuteAsync(sql, new
        {
            Id = message.Id,
            CreatedAt = message.CreatedAt,
            Content = request.Content,
            Role = request.Role.ToString(),
            ChatId = request.ChatId,
            UserId = message.UserId
        });
        
        return new AddChatMessageResponse(message.Id);
    }
}