using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sender.Events;
using Sender.Middleware;
using Shared;
using Spectre.Console;
using Wolverine;
using Wolverine.AzureServiceBus;

var builder = Host.CreateApplicationBuilder(args);
builder.UseWolverine(opts =>
{
    opts.UseAzureServiceBus(ConnectionStrings.ServiceBus)
        .AutoProvision();

    opts.PublishMessage<QueueTestMessage>()
        .ToAzureServiceBusQueue(WolverineExtensions.GetNameForAzureSeviceBus<QueueTestMessage>());

    opts.PublishMessage<TopicTestMessage>()
        .ToAzureServiceBusTopic(WolverineExtensions.GetNameForAzureSeviceBus<TopicTestMessage>());

    opts.Policies.AddMiddleware<LoggerMiddleware>();
});
var app = builder.Build();
app.Start();

AnsiConsole.Write(new FigletText("Sender").Centered().Color(new Color(0, 130, 74)));
AnsiConsole.Write(new Rule("[dim]Azure Service Bus[/]").Centered());
AnsiConsole.WriteLine();

var run = true;
var messageBus = app.Services.GetRequiredService<IMessageBus>();

while (run)
{
    var action = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title("[bold]Whot do you want send?[/]")
        .HighlightStyle(new Style(new Color(0, 130,74), decoration: Decoration.Bold))
        .AddChoices("Queue", "Topic", "Exit"));

    switch (action)
    {
        case "Queue":
            var body = AnsiConsole.Prompt(
                    new TextPrompt<string>("[#00824A]Body?[/] [grey]empty to cancel[/]")
                    .AllowEmpty()
                    .PromptStyle("white")
                );

            if (string.IsNullOrEmpty(body))
                break;

            var text = await messageBus.InvokeAsync<string>(new CleanTextCommand(body));

            await AnsiConsole.Status()
                    .Spinner(Spinner.Known.Dots)
                    .SpinnerStyle(Style.Parse("#00824A"))
                    .StartAsync(
                        "Sending...",
                        async _ => await messageBus.InvokeAsync(new SendToQueueEvent(body))
                        );

            break;

        case "Topic":
            var application = AnsiConsole.Prompt(
                new TextPrompt<string>("[#00824A]Application?[/] [grey]empty to cancel[/]")
                .AllowEmpty()
                .PromptStyle("white")
            );

            if (string.IsNullOrEmpty(application))
                break;

            var content = AnsiConsole.Prompt(
                            new TextPrompt<string>("[#00824A]Content?[/] [grey]empty to cancel[/]")
                            .AllowEmpty()
                            .PromptStyle("white")
                        );

            if (string.IsNullOrEmpty(content))
                break;

            await AnsiConsole.Status()
        .Spinner(Spinner.Known.Dots)
        .SpinnerStyle(Style.Parse("#00824A"))
        .StartAsync(
            "Sending...",
            async _ => await messageBus.InvokeAsync(new SendToTopicEvent(application, content))
            );

            break;

        default:
            run = false;
            break;
    }
}