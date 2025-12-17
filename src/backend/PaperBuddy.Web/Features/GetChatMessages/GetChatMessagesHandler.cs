using System.Data;
using Dapper;
using PaperBuddy.Web.Common;
using PaperBuddy.Web.Domain;

namespace PaperBuddy.Web.Features.GetChatMessages;

public class GetChatMessagesHandler(IDbConnection connection) : RequestHandler<GetChatMessagesRequest, List<GetChatMessagesResponse>>(connection)
{
    protected override async Task<List<GetChatMessagesResponse>> HandleAsync(GetChatMessagesRequest request, CancellationToken cancellationToken)
    {
        string sql = "SELECT UPPER(role) as Role, content, created_at as CreatedAt FROM chat_messages " +
                     "WHERE  user_id = @UserId and chat_id = @ChatId;";
        
        var result = await Database.QueryAsync<GetChatMessagesResponse>(sql, new
        {
            UserId = new Guid("a3b99d2e-2fdf-4956-9690-cb6be5cf900a"),
            ChatId = request.ChatId
        });
        
        return result.ToList();
    }
}