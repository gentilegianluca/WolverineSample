using Receiver.Events;
using Spectre.Console;

namespace Receiver.Handlers;

public class ReceiveEventHandler()
{
    public async Task HandleAsync(ReceiveEvent message)
        => AnsiConsole.MarkupLine(
            $"[dim]{DateTime.Now:HH:mm:ss}[/] [bold]{Markup.Escape(message.Message)}[/]");
}
