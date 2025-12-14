using Microsoft.AspNetCore.Mvc;

namespace PaperBuddy.Web.Features.CreateProject;

public static class CreateProjectEndpoint
{
    public static void MapCreateProjectEndpoint(this WebApplication app)
    {
        app.MapPost("/projects", async (
                [FromServices] CreateProjectHandler handler,
                [FromBody] CreateProjectRequest request) =>
            {
                var projectId = await handler.HandleAsync(request, cancellationToken: CancellationToken.None);

                return Results.Ok(new { ProjectId = projectId });
            })
            .Accepts<CreateProjectRequest>("application/json")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .DisableAntiforgery();
    }
}