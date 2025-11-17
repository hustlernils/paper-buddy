using PaperBuddy.MessageBus;

namespace PaperBuddy.MessageBus.Tests;

public class SubscriptionManagerTests
{
    [Fact]
    public void SubscriptionManager_RegistersConsumerType()
    {
        var manager = new SubscriptionManager();
        
        manager.Subscribe<TestMessageConsumer>();
        
        var consumerType = manager.GetConsumer(new TestMessage("Hello World!"));
        Assert.Equal(typeof(TestMessageConsumer), consumerType);
    }
}