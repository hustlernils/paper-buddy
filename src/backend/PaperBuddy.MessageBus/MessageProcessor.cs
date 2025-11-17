using Microsoft.Extensions.Hosting;

namespace PaperBuddy.MessageBus;

internal class MessageProcessor : BackgroundService
{
    private readonly InMemoryMessageQueue  _queue;
    private readonly MessageDispatcher  _dispatcher;
    internal MessageProcessor(InMemoryMessageQueue queue, MessageDispatcher dispatcher)
    {
        _queue = queue;
        _dispatcher = dispatcher;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var message in _queue.Reader.ReadAllAsync(stoppingToken))
        {
            Console.WriteLine($"Processing message: {message}");
            await _dispatcher.DispatchAsync(message, stoppingToken);
        }  
    }
}