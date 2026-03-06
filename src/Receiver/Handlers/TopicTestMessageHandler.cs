using Receiver.Events;
using Receiver.Settings;
using Shared;
using Wolverine;

namespace Receiver.Handlers;

public class TopicTestMessageHandler(Setting setting, IMessageBus messageBus)
{
    public async Task HandleAsync(TopicTestMessage message)
    {
        if (message.Application == setting.Application)
            await messageBus.InvokeAsync(new ReceiveEvent(message.Content));
    }
}
