using PaperBuddy.MessageBus.Abstractions;

namespace PaperBuddy.MessageBus;

internal sealed class InMemoryMessageBus(InMemoryMessageQueue queue) : IMessageBus
{
    private readonly InMemoryMessageQueue  _queue = queue;

    public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken=default) where TMessage : class, IMessage
    {
        await _queue.Writer.WriteAsync(message, cancellationToken);
    }
}