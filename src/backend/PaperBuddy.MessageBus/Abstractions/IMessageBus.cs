namespace PaperBuddy.MessageBus.Abstractions;

public interface IMessageBus
{
    Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken) where TMessage : class, IMessage;
}