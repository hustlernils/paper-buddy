using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
            .Configure<ConsumerRegistryOptions>(options => { })
            .AddSingleton<ISubscriptionManager, SubscriptionManager>()
            .AddHostedService<MessageProcessor>();

        var builder = new MessageBusBuilder(services);
        configure(builder);
        
        return services;
    }
    
    public static IServiceProvider UseMessageBus(this IServiceProvider provider)
    {
        var manager = provider.GetRequiredService<ISubscriptionManager>();
        var options = provider.GetRequiredService<IOptions<ConsumerRegistryOptions>>().Value;
        
        foreach (var consumer in options.ConsumerTypes)
        {
            manager.Subscribe(consumer);
        }

        return provider;
    }
}

public interface IMessageBusBuilder
{
    void AddConsumer<TConsumer>() where TConsumer : class;
}

internal class ConsumerRegistryOptions
{
    public List<Type> ConsumerTypes { get; } = [];
}

internal class MessageBusBuilder(IServiceCollection services) : IMessageBusBuilder
{
    // public void AddConsumer<TConsumer>() where TConsumer : class
    // {
    //     SubscriptionRegistrar.RegisterConsumer<TConsumer>(services);
    // }

    public void AddConsumer<TConsumer>() where TConsumer : class
    {
        services.AddScoped<TConsumer>();
        
        foreach (var @interface in typeof(TConsumer).GetInterfaces())
        {
            if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IConsumer<>))
            {
                services.AddScoped(@interface, typeof(TConsumer));
            }
        }
        
        services.Configure<ConsumerRegistryOptions>(options =>
        {
            options.ConsumerTypes.Add(typeof(TConsumer));
        });
    }
}