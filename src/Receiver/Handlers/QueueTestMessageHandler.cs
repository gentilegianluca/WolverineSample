using System;
using System.Collections.Generic;
using System.Text;
using Receiver.Events;
using Shared;
using Wolverine;

namespace Receiver.Handlers;

public class QueueTestMessageHandler(IMessageBus messageBus)
{
    public async Task HandleAsync(QueueTestMessage message)
        => await messageBus.InvokeAsync(new ReceiveEvent(message.Body));
}
