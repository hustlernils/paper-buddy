using PaperBuddy.MessageBus.Abstractions;

namespace PaperBuddy.Web.Features.UploadPaper;

public class SummarizePaperHandler(ILogger<SummarizePaperHandler> logger) : IConsumer<SummarizePaperRequest>
{
    public Task ConsumeAsync(SummarizePaperRequest message, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handling paper with id: {message.Id}");
        return Task.CompletedTask;
    }
}