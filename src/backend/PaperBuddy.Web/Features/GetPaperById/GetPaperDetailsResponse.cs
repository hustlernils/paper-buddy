namespace PaperBuddy.Web.Features.GetPaperById;

public record GetPaperDetailsResponse(Guid Id, string? Title, string Authors, string Summary);