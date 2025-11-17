using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PaperBuddy.MessageBus.Abstractions;

namespace PaperBuddy.MessageBus.Tests;


internal class TestMessageConsumerWrapper : IConsumer<TestMessage>
{
    public static bool WasCalled = false;

    public Task ConsumeAsync(TestMessage message, CancellationToken ct)
    {
        WasCalled = true;
        return Task.CompletedTask;
    }
}
public class MessageDispatcherTests
{
    private IServiceProvider ServiceProvider { get; }

    public MessageDispatcherTests()
    {
        var services = new ServiceCollection();
        
        services.AddMessageBus(config =>
        {
            config.AddConsumer<TestMessageConsumerWrapper>();
        });
        
        ServiceProvider = services.BuildServiceProvider();
    }
    
    [Fact]
    public async Task Test()
    {
        var consumerMock = ServiceProvider.GetRequiredService<IConsumer<TestMessage>>();
        
        var dispatcher = ServiceProvider.GetRequiredService<MessageDispatcher>();

        await dispatcher.DispatchAsync(new TestMessage("Hello"), CancellationToken.None);

        Assert.True(TestMessageConsumerWrapper.WasCalled);
    }
}