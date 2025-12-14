using Microsoft.AspNetCore.Mvc;

namespace PaperBuddy.Web.Features.GetProjects;

public static class GetProjectsEndpoint
{
    public static void MapGetProjectsEndpoint(this WebApplication app)
    {
        app.MapGet("/projects", async (
                [FromServices] GetProjectsHandler handler) =>
            {
                var request = new GetProjectsRequest();
                var responses = await handler.HandleAsync(request, cancellationToken: CancellationToken.None);

                return Results.Ok(responses);
            })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .DisableAntiforgery();
    }
}