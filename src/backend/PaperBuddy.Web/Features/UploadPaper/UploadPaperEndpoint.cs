using Microsoft.AspNetCore.Mvc;

namespace PaperBuddy.Web.Features.UploadPaper;

public static class UploadPaperEndpoint
{
    public static void MapUploadPaperEndpoint(this WebApplication app)
    {
        app.MapPost("/papers/upload", async (
                [FromServices] UploadPaperHandler handler,
                IFormFile file,
                Guid projectId) =>
            {
                var request = new UploadPaperRequest(file,  projectId);
                var paperId = await handler.HandleAsync(request, cancellationToken: CancellationToken.None);

                return Results.Ok(new { PaperId = paperId });
            })
            .Accepts<UploadPaperRequest>("multipart/form-data")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .DisableAntiforgery();
    }
}