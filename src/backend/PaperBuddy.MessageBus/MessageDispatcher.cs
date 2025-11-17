using Microsoft.Extensions.DependencyInjection;
using PaperBuddy.MessageBus.Abstractions;

namespace PaperBuddy.MessageBus;

internal class MessageDispatcher(ISubscriptionManager subscriptions, IServiceProvider serviceProvider)
{
    private readonly ISubscriptionManager _subscriptions = subscriptions;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    
    public async Task DispatchAsync<TMessage>(TMessage message, CancellationToken cancellationToken)
    {
        var consumerType = _subscriptions.GetConsumer(message);

        var consumer = _serviceProvider.GetRequiredService(consumerType) as IConsumer<TMessage>;
        
        await consumer.ConsumeAsync(message, cancellationToken);
    } 
}