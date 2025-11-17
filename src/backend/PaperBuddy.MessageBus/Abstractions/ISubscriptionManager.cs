namespace PaperBuddy.MessageBus.Abstractions;

public interface ISubscriptionManager
{
    void Subscribe<TConsumer>() where TConsumer : class;
    Type GetConsumer<TMessage>(TMessage message);
}