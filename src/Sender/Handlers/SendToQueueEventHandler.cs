using Sender.Events;
using Shared;
using Wolverine;

namespace Sender.Handlers;

public class SendToQueueEventHandler(IMessageBus messageBus)
{
    public async Task HandleAsync(SendToQueueEvent message)
        => await messageBus.SendAsync(new QueueTestMessage(message.Body));
}
