using Microsoft.AspNetCore.Mvc;

namespace PaperBuddy.Web.Features.GetPapers;

public static class GetPapersEndpoint
{
    public static void MapGetPapersEndpoint(this WebApplication app)
    {
        app.MapGet("/papers", async (
                [FromServices] GetPapersHandler handler) =>
            {
                var request = new GetPapersRequest();
                var responses = await handler.HandleAsync(request, cancellationToken: CancellationToken.None);

                return Results.Ok(responses);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .DisableAntiforgery();
    }
}