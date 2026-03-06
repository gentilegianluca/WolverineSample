using Sender.Events;

namespace Sender.Handlers;

public class CleanTextCommandHandler
{
    public string Handle(CleanTextCommand command)
        => command.Text.Replace("[data]", DateTime.Now.ToString());
}
