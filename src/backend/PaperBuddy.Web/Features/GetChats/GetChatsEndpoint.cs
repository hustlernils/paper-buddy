using Microsoft.AspNetCore.Mvc;
using PaperBuddy.Web.Domain;

namespace PaperBuddy.Web.Features.GetChats;

public static class GetChatsEndpoint
{
    public static void MapGetChatsEndpoint(this WebApplication app)
    {
        app.MapGet("/chats/{parentId}", async (
            [FromRoute] Guid parentId,
            [FromQuery] string parentType,
            [FromServices] GetChatsHandler handler, 
            CancellationToken ct) =>
        {
            var request = new GetChatsRequest(parentId, parentType);

            var respones = await handler.Execute(request, ct);

            return Results.Ok(respones);
        })
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .DisableAntiforgery();
    }
}