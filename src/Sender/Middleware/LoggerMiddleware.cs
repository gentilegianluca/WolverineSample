using System.Diagnostics;
using Spectre.Console;
using Wolverine;

namespace Sender.Middleware;

public class LoggerMiddleware()
{
    private readonly Stopwatch stopwatch = new();
    public void Before(Envelope envelope)
    {
        stopwatch.Start();
        AnsiConsole.MarkupLine($"[dim]{DateTime.Now:HH:mm:ss}[/] Before >> {Markup.Escape(envelope.MessageType!)}");
    }

    public void Finally(Envelope envelope)
    {
        stopwatch.Stop();
        AnsiConsole.MarkupLine($"[dim]{DateTime.Now:HH:mm:ss}[/] Finally >> {Markup.Escape(envelope.MessageType!)} {stopwatch.ElapsedMilliseconds}");
    }
}
