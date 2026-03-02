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
        var userMessage = new ChatMessage(content: request.Content, role: request.Role, chatId: request.ChatId,
            userId: new Guid("a3b99d2e-2fdf-4956-9690-cb6be5cf900a"));

        await InsertMessage(userMessage);
        
        // query related messages for context
        string sql = @"
            SELECT role, content 
            FROM chat_messages 
            WHERE chat_id = @ChatId 
            ORDER BY created_at ASC 
            LIMIT 50";
        
        var conversationHistory = await Database.QueryAsync<ChatHistoryItem>(sql, new { ChatId = request.ChatId });
        
        var systemAnswer = await chatService.GetAnswerAsync(conversationHistory);
        
        var systemMessage = ChatMessage.CreateSystemMessage(content: systemAnswer, chatId: request.ChatId,
            userId: new Guid("a3b99d2e-2fdf-4956-9690-cb6be5cf900a"));
        
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