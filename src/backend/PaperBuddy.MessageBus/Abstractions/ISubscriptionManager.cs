namespace PaperBuddy.MessageBus.Abstractions;

public interface ISubscriptionManager
{
    void Subscribe<TConsumer>() where TConsumer : class;
    void Subscribe(Type consumerType);
    Type GetConsumer<TMessage>(TMessage message);
}