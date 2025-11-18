using System.Collections.Concurrent;
using PaperBuddy.MessageBus;
using PaperBuddy.MessageBus.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

internal static class SubscriptionRegistrar
{
    private static readonly List<Type> _consumers = [];
    
    public static IReadOnlyCollection<Type> Consumers => _consumers;
    
    internal static void RegisterConsumer<TConsumer>(IServiceCollection services) where TConsumer : class
    {
        var consumerType = typeof(TConsumer);
        
        var messageInterface = consumerType
            .GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IConsumer<>));

        if (messageInterface == null)
        {
            throw new InvalidOperationException($"{consumerType.Name} does not implement IConsumer<>");
        }

        _consumers.Add(consumerType);
        services.AddScoped(messageInterface, consumerType);
    }
}