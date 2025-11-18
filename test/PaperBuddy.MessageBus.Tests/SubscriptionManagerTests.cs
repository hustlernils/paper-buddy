using Microsoft.Extensions.DependencyInjection;
using PaperBuddy.MessageBus.Abstractions;

namespace PaperBuddy.MessageBus.Tests;

public class SubscriptionManagerTests
{
    [Fact]
    public void SubscriptionManager_RegistersConsumerType()
    {
        var provider = new ServiceCollection().BuildServiceProvider();
        
        var manager = new SubscriptionManager();
        
        manager.Subscribe<TestMessageConsumer>();
        
        var consumerType = manager.GetConsumer(new TestMessage("Hello World!"));
        
        Assert.Equal(typeof(IConsumer<TestMessage>), consumerType);
    }
}