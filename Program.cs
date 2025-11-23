using System.ComponentModel;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using Spectre.Console.Cli;
using SSH.Navigator.Models;
using SSH.Navigator.Utility;

var app = new CommandApp<FileSizeCommand>();

return app.Run(args);

internal sealed class FileSizeCommand : Command<FileSizeCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [Description("Connect using definition named 'name'.")]
        [CommandOption("-n|--name")]
        public string? UseName { get; init; }

        [Description("Connect as type 'type'. Possible values are 'ssh' and 'sftp'.")]
        [CommandOption("-t|--type")]
        [DefaultValue("ssh")]
        public string? UseType { get; init; }

        [Description("Force password prompt, e.g., when you need to confirm host authenticity.")]
        [CommandOption("-p|--password")]
        [DefaultValue(false)]
        public bool ForcePasswordPrompt { get; init; }

        [Description("Display host definitions.")]
        [CommandOption("-l|--list")]
        [DefaultValue(false)]
        public bool DisplayHostDefinitions { get; init; }

        [Description("Show environment variable values in host definition list.")]
        [CommandOption("-s|--show-env")]
        [DefaultValue(false)]
        public bool ShowEnvironmentVariableValues { get; init; }

        [Description("Show password values in host definition list. USE WITH CAUTION!")]
        [CommandOption("-x|--show-passwords")]
        [DefaultValue(false)]
        public bool ShowPasswordValues { get; init; }
    }

    public override int Execute(CommandContext context, Settings settings, CancellationToken cancellationToken)
    {
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile(Path.Join(Path.GetDirectoryName(Environment.ProcessPath ?? ""), "ssh-nav.json"))
            .AddEnvironmentVariables()
            .Build();

        AppSettings appSettings = config.Get<AppSettings>() ?? new();

        DefinitionManager definitionManager = new(appSettings.HostDefinitions ?? []);

        bool actionTaken = false;

        if (settings.UseName is not null && settings.UseType is not null)
        {
            actionTaken = true;
            definitionManager.Execute(settings.UseName, settings.UseType, settings.ForcePasswordPrompt);
        }

        if (settings.DisplayHostDefinitions)
        {
            actionTaken = true;
            definitionManager.DisplayDefinitions(settings.ShowPasswordValues, settings.ShowEnvironmentVariableValues);
        }

        if (!actionTaken)
            Console.WriteLine("Not sure what you want to do.  Try --help to see available options.");

        return 0;
    }
}