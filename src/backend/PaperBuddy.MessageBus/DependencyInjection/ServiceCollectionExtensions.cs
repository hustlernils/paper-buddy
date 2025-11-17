using Microsoft.Extensions.DependencyInjection;
using PaperBuddy.MessageBus.Abstractions;
using PaperBuddy.MessageBus;

namespace Microsoft.Extensions.DependencyInjection;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services,
        Action<IMessageBusBuilder> configure)
    {
        _ = services.AddSingleton<InMemoryMessageQueue>()
            .AddSingleton<IMessageBus, InMemoryMessageBus>()
            .AddSingleton<MessageDispatcher>()
            .AddSingleton<ISubscriptionManager, SubscriptionManager>()
            .AddHostedService<MessageProcessor>();

        var builder = new MessageBusBuilder(services);
        configure(builder);

        return services;
    }
}

public interface IMessageBusBuilder
{
    void AddConsumer<TConsumer>() where TConsumer : class;
}

internal class MessageBusBuilder(IServiceCollection services) : IMessageBusBuilder
{
    public void AddConsumer<TConsumer>() where TConsumer : class
    {
        SubscriptionRegistrar.RegisterConsumer<TConsumer>(services);
    }
}