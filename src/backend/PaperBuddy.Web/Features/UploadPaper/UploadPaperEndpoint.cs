using Microsoft.AspNetCore.Mvc;

namespace PaperBuddy.Web.Features.UploadPaper;

public static class UploadPaperEndpoint
{
    public static void MapUploadPaperEndpoint(this WebApplication app)
    {
        app.MapPost("/papers/upload", async (
                [FromServices] UploadPaperHandler handler,
                IFormFile file) =>
            {
                var request = new UploadPaperRequest(file);
                var paperId = await handler.HandleAsync(request);
                return Results.Ok(new { PaperId = paperId });
            })
            .Accepts<UploadPaperRequest>("multipart/form-data")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .DisableAntiforgery();
    }
}