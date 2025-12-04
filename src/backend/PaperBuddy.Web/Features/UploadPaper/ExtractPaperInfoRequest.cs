using PaperBuddy.MessageBus.Abstractions;

namespace PaperBuddy.Web.Features.UploadPaper;

public record ExtractPaperInfoRequest(Guid PaperId) : IMessage;