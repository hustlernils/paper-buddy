using Microsoft.AspNetCore.Mvc;

namespace PaperBuddy.Web.Features.GetPaperById;

public static class GetPaperByIdEndpoint
{
    public static void MapGetPaperByIdEndpoint(this WebApplication app)
    {
        app.MapGet("/papers/{paperId}", async (
                Guid paperId,
                [FromServices] GetPaperByIdHandler handler) =>
            {
                var request = new GetPaperByIdRequest(paperId);
                var response = await handler.HandleAsync(request, CancellationToken.None);
                return response != null ? Results.Ok(response) : Results.NotFound();
            })
            .Produces<PaperDetails>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .DisableAntiforgery();
    }
}