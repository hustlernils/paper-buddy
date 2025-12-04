using PaperBuddy.MessageBus.Abstractions;

namespace PaperBuddy.MessageBus.Tests;

internal class TestMessageConsumer : IConsumer<TestMessage>
{
    public Task ConsumeAsync(TestMessage message,  CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}