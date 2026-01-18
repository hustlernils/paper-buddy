namespace PaperBuddy.Web.Features.GetPaperById;

public record PaperDetails(Guid Id, string? Title, string Authors, string Summary);