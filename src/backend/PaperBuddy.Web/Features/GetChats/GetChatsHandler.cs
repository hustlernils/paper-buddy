using System.Data;
using Dapper;
using PaperBuddy.Web.Common;
using PaperBuddy.Web.Domain;

namespace PaperBuddy.Web.Features.GetChats;

public class GetChatsHandler(IDbConnection connection) : RequestHandler<GetChatsRequest, List<ChatResponse>>(connection)
{
    protected override async Task<List<ChatResponse>> HandleAsync(GetChatsRequest request, CancellationToken cancellationToken)
    {
        var sql = "SELECT  id, created_at AS CreatedAt FROM chats WHERE parent_id = @ParentId AND UPPER(parent_type) = UPPER(@ParentType) AND user_id = @UserId;";

        var result = await Database.QueryAsync<ChatResponse>(sql, new
        {
            ParentId = request.ParentId,
            ParentType = request.ParentType,
            UserId = new Guid("a3b99d2e-2fdf-4956-9690-cb6be5cf900a")
        });
        
        return result.ToList();
    }
}