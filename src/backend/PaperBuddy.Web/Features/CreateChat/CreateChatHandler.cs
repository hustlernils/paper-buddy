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
            ParentType = parentType,
            ParentId = request.ParentId,
            UserId = new Guid("a3b99d2e-2fdf-4956-9690-cb6be5cf900a"), // TODO: Get from authenticated user
        };
        
        await _dbConnection.ExecuteAsync(
            @"INSERT INTO chats (id, parent_type, parent_id, user_id, created_at)
            VALUES (@Id, @ParentType, @ParentId, @UserId, @CreatedAt)", new
            {
                Id= chat.Id, // creating new object because Dapper cannot serialize enums to strings
                ParentType = chat.ParentType.ToString(),
                ParentId = chat.ParentId,
                CreatedAt = chat.CreatedAt,
                UserId = chat.UserId
            });

        _dbConnection.Close();

        return chat.Id;
    }
}