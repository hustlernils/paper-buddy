namespace PaperBuddy.Web.Features.GetPapers;

public record GetPapersResponse(Guid Id, string? Title, string Authors, string[] Keywords);