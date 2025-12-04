using Microsoft.Extensions.DependencyInjection;
using PaperBuddy.MessageBus.Abstractions;

namespace PaperBuddy.MessageBus;

internal class MessageDispatcher(ISubscriptionManager subscriptions, 
    IServiceProvider serviceProvider)
{
    private readonly ISubscriptionManager _subscriptions = subscriptions;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    
    public async Task DispatchAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
    {
        var consumerType = _subscriptions.GetConsumer(message);
        
        using var scope = _serviceProvider.CreateScope();
        var consumer = scope.ServiceProvider.GetRequiredService(consumerType) as IConsumer<TMessage>;
        
        await consumer!.ConsumeAsync(message, cancellationToken);
    } 
    
    public async Task DispatchAsync(IMessage message, CancellationToken cancellationToken = default)
    {
        await DispatchAsync((dynamic)message, cancellationToken);
    } 
}