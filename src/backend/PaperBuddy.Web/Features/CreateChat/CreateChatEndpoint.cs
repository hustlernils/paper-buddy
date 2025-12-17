using Microsoft.AspNetCore.Mvc;

namespace PaperBuddy.Web.Features.CreateChat;

public static class CreateChatEndpoint
{
    public static void MapCreateChatEndpoint(this WebApplication app)
    {
        app.MapPost("/chats", async (
                [FromServices] CreateChatHandler handler,
                [FromBody] CreateChatRequest request) =>
            {
                var chatId = await handler.HandleAsync(request, cancellationToken: CancellationToken.None);

                return Results.Ok(new { ChatId = chatId });
            })
            .Accepts<CreateChatRequest>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .DisableAntiforgery();
    }
}