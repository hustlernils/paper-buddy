using PaperBuddy.MessageBus;
using PaperBuddy.MessageBus.Abstractions;

namespace Microsoft.Extensions.DependencyInjection;

internal static class SubscriptionRegistrar
{
    internal static void RegisterConsumer<TConsumer>(IServiceCollection services) where TConsumer : class
    {
        var messageInterface = typeof(TConsumer)
            .GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IConsumer<>));

        if (messageInterface == null)
        {
            throw new InvalidOperationException($"{typeof(TConsumer).Name} does not implement IConsumer<>");
        }

        services.AddScoped(messageInterface, typeof(TConsumer));

        services.AddSingleton(provider =>
        {
            var manager = provider.GetRequiredService<SubscriptionManager>();
            manager.Subscribe<TConsumer>();
            return manager;
        });
    }
}