using System.Data;
using Dapper;
using PaperBuddy.Web.Domain;

namespace PaperBuddy.Web.Features.CreateChat;

public class CreateChatHandler(IDbConnection connection)
{
    private readonly IDbConnection _dbConnection = connection;

    public async Task<Guid> HandleAsync(CreateChatRequest request, CancellationToken cancellationToken)
    {
        _dbConnection.Open();
        Enum.TryParse(request.ParentType, out ParentType parentType);
        var chat = new Chat
        {
            Id = Guid.NewGuid(),
            ParentType = parentType,
            ParentId = request.ParentId,
            UserId = new Guid("a3b99d2e-2fdf-4956-9690-cb6be5cf900a"), // TODO: Get from authenticated user
            CreatedAt = DateTime.UtcNow,
        };
        
        await _dbConnection.ExecuteAsync(
            @"INSERT INTO chats (id, parent_type, parent_id, user_id, created_at)
            VALUES (@Id, @ParentType, @ParentId, @UserId, @CreatedAt)", chat);

        _dbConnection.Close();

        return chat.Id;
    }
}