using System.Threading.Channels;
using PaperBuddy.MessageBus.Abstractions;

namespace PaperBuddy.MessageBus;

internal class InMemoryMessageQueue
{
    private readonly Channel<IMessage> _channel = Channel.CreateUnbounded<IMessage>();

    internal ChannelReader<IMessage> Reader => _channel.Reader;
    internal ChannelWriter<IMessage> Writer => _channel.Writer;

}