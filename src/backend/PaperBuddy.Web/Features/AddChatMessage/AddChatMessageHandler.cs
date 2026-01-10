using System.Data;
using Dapper;
using PaperBuddy.Web.Common;
using PaperBuddy.Web.Domain;
using PaperBuddy.Web.Infrastructure.Services;

namespace PaperBuddy.Web.Features.AddChatMessage;

public class AddChatMessageHandler(IDbConnection connection, ChatService chatService) : TransactionRequestHandler<AddChatMessageRequest, AddChatMessageResponse>(connection)
{
    protected override async Task<AddChatMessageResponse> HandleAsync(AddChatMessageRequest request, CancellationToken cancellationToken)
    {
        var userMessage = new ChatMessage()
        {
            Content = request.Content,
            Role = request.Role,
            ChatId = request.ChatId,
            UserId = new Guid("a3b99d2e-2fdf-4956-9690-cb6be5cf900a")
        };

        // TODO: query existing messages and resend them to the LLM for context
        
        var systemAnswer = await chatService.GetAnswerAsync(userMessage.Content);
        var systemMessage = new ChatMessage()
        {
            Content = systemAnswer,
            Role = MessageRole.System,
            ChatId = request.ChatId,
            UserId = new Guid("a3b99d2e-2fdf-4956-9690-cb6be5cf900a")
        };
        
        await InsertMessage(userMessage);
        await InsertMessage(systemMessage);
        
        return new AddChatMessageResponse(userMessage.Id);
    }

    private async Task InsertMessage(ChatMessage message)
    {
        string sql = "INSERT INTO chat_messages (id, created_at, content, role, chat_id, user_id) VALUES " +
                     "(@Id, @CreatedAt, @Content, @Role, @ChatId, @UserId);";

        await Database.ExecuteAsync(sql, new
        {
            Id = message.Id,
            CreatedAt = message.CreatedAt,
            Content = message.Content,
            Role = message.Role.ToString(),
            ChatId = message.ChatId,
            UserId = message.UserId
        });
    }
}