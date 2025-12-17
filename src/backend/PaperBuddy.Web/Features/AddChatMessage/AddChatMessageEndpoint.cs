using Microsoft.AspNetCore.Mvc;
using PaperBuddy.Web.Domain;

namespace PaperBuddy.Web.Features.AddChatMessage;

public static class AddChatMessageEndpoint
{
    public static void MapAddChatMessageEndpoint(this WebApplication app)
    {
        app.MapPost("/chats/{chatId}/messages", async (
                [FromRoute] Guid chatId,
                [FromBody] AddChatMessageRequest request,
                [FromServices] AddChatMessageHandler handler,
                CancellationToken ct) =>
            {
                var fullRequest = new AddChatMessageRequest(
                    chatId,
                    MessageRole.User,
                    request.Content
                );
                
                var response = await handler.Execute(fullRequest, ct);
                
                return Results.Created($"/chats/{chatId}/messages/{response.Id}", response);
            })
            .Accepts<AddChatMessageRequest>("application/json")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .DisableAntiforgery();
    }
}