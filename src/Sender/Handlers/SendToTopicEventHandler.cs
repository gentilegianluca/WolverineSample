using Sender.Events;
using Shared;
using Wolverine;

namespace Sender.Handlers;

public class SendToTopicEventHandler(IMessageBus messageBus)
{
    public async Task HandleAsync(SendToTopicEvent message)
        => await messageBus.PublishAsync(new TopicTestMessage(message.Application, message.Content));
}
