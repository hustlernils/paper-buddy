using Microsoft.Extensions.DependencyInjection;
using PaperBuddy.MessageBus.Abstractions;

namespace PaperBuddy.MessageBus;

internal class MessageDispatcher(ISubscriptionManager subscriptions, 
    IServiceProvider serviceProvider)
{
    private readonly ISubscriptionManager _subscriptions = subscriptions;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    
    public async Task DispatchAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
    {
        var consumerType = _subscriptions.GetConsumer(message);
        
        using var scope = _serviceProvider.CreateScope();
        var consumer = scope.ServiceProvider.GetRequiredService(consumerType) as IConsumer<TMessage>;
        var consumer2 = scope.ServiceProvider.GetRequiredService(consumerType);
        var consumer3 = scope.ServiceProvider.GetRequiredService<IConsumer<TMessage>>();
        
        await consumer!.ConsumeAsync(message, cancellationToken);
    } 
}