using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PaperBuddy.MessageBus;

internal class MessageProcessor : BackgroundService
{
    private readonly ILogger<MessageProcessor> _logger;
    private readonly InMemoryMessageQueue  _queue;
    private readonly MessageDispatcher  _dispatcher;
    public MessageProcessor(ILogger<MessageProcessor> logger, InMemoryMessageQueue queue, MessageDispatcher dispatcher)
    {
        _logger = logger;
        _queue = queue;
        _dispatcher = dispatcher;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await foreach (var message in _queue.Reader.ReadAllAsync(stoppingToken))
                {
                    await _dispatcher.DispatchAsync(message, stoppingToken);
                }
            }
            catch (OperationCanceledException)
            {
                // Normal shutdown, do nothing
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "MessageProcessor crashed. Restarting loop.");

                // Backoff to avoid tight crash-loop
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}