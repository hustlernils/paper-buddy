using Microsoft.AspNetCore.Mvc;

namespace PaperBuddy.Web.Features.GetChatMessages;

public static class GetChatMessagesEndpoint
{
    public static void MapGetChatMessagesEndpoint(this WebApplication app)
    {
        app.MapGet("/chats/{chatId}/messages", async (
                [FromRoute] Guid chatId,
                [FromServices] GetChatMessagesHandler handler,
                CancellationToken ct) =>
            {
                var request = new GetChatMessagesRequest(chatId);
                
                var response = await handler.Execute(request, ct);
                
                return Results.Ok(response);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest)
            .DisableAntiforgery();
    }
}