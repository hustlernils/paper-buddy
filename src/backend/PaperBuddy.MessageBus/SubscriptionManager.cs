using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using PaperBuddy.MessageBus.Abstractions;

namespace PaperBuddy.MessageBus;

internal sealed class SubscriptionManager() : ISubscriptionManager
{
    private readonly ConcurrentDictionary<Type, Type> _consumerMap = new();

    public void Subscribe<TConsumer>() where TConsumer : class
    {
        var consumerType = typeof(TConsumer);

        Subscribe(consumerType);
    }

    public void Subscribe(Type consumerType)
    {
        var messageInterface = consumerType
            .GetInterfaces()
            .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IConsumer<>));

        var messageType = messageInterface.GetGenericArguments()[0];

        _consumerMap.TryAdd(messageType, consumerType);
    }
    
    public Type GetConsumer<TMessage>(TMessage message)
    {
        if (!_consumerMap.TryGetValue(typeof(TMessage), out var consumerType))
        {
            throw new InvalidOperationException($"No consumer registered for type {typeof(TMessage)}");
        }
        
        return consumerType;
    }
}