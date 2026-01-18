using PaperBuddy.MessageBus.Abstractions;

namespace PaperBuddy.Web.Features.UploadPaper;

public record SummarizePaperRequest(Guid PaperId) : IMessage;