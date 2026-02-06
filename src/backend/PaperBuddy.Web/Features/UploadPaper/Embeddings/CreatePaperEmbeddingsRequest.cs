using PaperBuddy.MessageBus.Abstractions;

namespace PaperBuddy.Web.Features.UploadPaper;

public record CreatePaperEmbeddingsRequest(Guid PaperId) : IMessage;