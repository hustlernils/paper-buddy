using System.Collections.Concurrent;
using PaperBuddy.MessageBus.Abstractions;

namespace PaperBuddy.MessageBus;

internal sealed class SubscriptionManager : ISubscriptionManager
{
    private readonly ConcurrentDictionary<Type, Type> _consumerMap = [];
    
    public void Subscribe<TConsumer>() where TConsumer : class
    {
        var type = typeof(TConsumer);
        
        var messageType = type
            .GetInterfaces()
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IConsumer<>))
            .Select(i => i.GetGenericArguments()[0])
            .Single();

        _consumerMap.TryAdd(messageType, type);
    }
    
    public Type GetConsumer<TMessage>(TMessage message)
    {
        return !_consumerMap.TryGetValue(message.GetType(), out var consumerType ) 
            ? throw new InvalidOperationException($"Consumer for {message.GetType().Name} not found.") 
            : consumerType;
    }
}