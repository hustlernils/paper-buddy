# PaperBuddy.MessageBus

A simple Bus implementation using Channels.

## Overview

```mermaid
sequenceDiagram
    participant Publisher as Publisher (API / Service)
    participant Bus as InMemoryEventBus
    participant Channel as Message Channel
    participant Dispatcher as Dispatch Loop
    participant Consumer as IConsumer<TMessage>

    Publisher->>Bus: Publish(message)
    Bus->>Channel: Write message to channel
    Channel-->>Dispatcher: Message available
    Dispatcher->>Bus: Read message from channel
    Bus->>Consumer: Invoke Consume(message)
    Consumer-->>Bus: Completed
```

## Usage

Registration:

```csharp
// Program.cs
servies.AddInMessageBus();
```

Implementing Consumers:

```csharp
internal sealed class ExampleConsumer : IConsumer<ExampleMessage>
{
    public Task ConsumeAsync(TMessage message)
    {
        // do stuff
    }   
}
```