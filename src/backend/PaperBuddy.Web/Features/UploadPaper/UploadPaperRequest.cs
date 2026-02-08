namespace PaperBuddy.Web.Features.UploadPaper;

public record UploadPaperRequest(IFormFile File, Guid? ProjectId = null);