namespace PaperBuddy.MessageBus.Abstractions;

public interface IConsumer<in TMessage>
{
    public Task ConsumeAsync(TMessage message, CancellationToken cancellationToken);
}