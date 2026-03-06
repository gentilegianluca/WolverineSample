using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Receiver.Settings;
using Shared;
using Spectre.Console;
using Wolverine;
using Wolverine.AzureServiceBus;

var application = AnsiConsole.Prompt(
                    new TextPrompt<string>("[#00824A]Application name?[/] [grey](empty to cancel)[/]")
                    .AllowEmpty()
                    .PromptStyle("white")
                    );

if (string.IsNullOrEmpty(application))
    return;

var setting = new Setting(application);

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.ClearProviders();
builder.UseWolverine(opts =>
{
    opts.UseAzureServiceBus(ConnectionStrings.ServiceBus)
            .AutoProvision();

    opts.ListenToAzureServiceBusQueue(WolverineExtensions.GetNameForAzureSeviceBus<QueueTestMessage>());
    opts.ListenToAzureServiceBusSubscription(setting.Application)
        .FromTopic(WolverineExtensions.GetNameForAzureSeviceBus<TopicTestMessage>());
});

builder.Services.AddSingleton(setting);
var app = builder.Build();
app.Start();
AnsiConsole.WriteLine();
AnsiConsole.Write(new FigletText($"Receiver {setting.Application}").Centered().Color(new Color(0, 130, 74)));
AnsiConsole.Write(new Rule("[dim]Azure Service Bus[/]").Centered());
AnsiConsole.WriteLine();
AnsiConsole.MarkupLine("[dim]Listening for messages... press ENTER to stop.[/]");
AnsiConsole.WriteLine();

Console.ReadLine();