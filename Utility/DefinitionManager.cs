using System.Diagnostics;
using Spectre.Console;
using SSH.Navigator.Models;

namespace SSH.Navigator.Utility;

public class DefinitionManager
{
    public DefinitionManager(List<HostDefinition> hostDefinitions)
    {
        this.HostDefinitions = hostDefinitions;
    }

    public List<HostDefinition> HostDefinitions { get; set; }

    public void DisplayDefinitions(bool showPasswordValues, bool showEnvironmentVariableValues)
    {
        Table hostDefinitionsTable = new();

        hostDefinitionsTable.AddColumn("Name (-n)");
        hostDefinitionsTable.AddColumn("Type (-t)");
        hostDefinitionsTable.AddColumn("Host Name");
        hostDefinitionsTable.AddColumn("User");
        hostDefinitionsTable.AddColumn("Password");

        foreach (HostDefinition item in this.HostDefinitions)
        {
            string? passwordDisplay;
            switch (item.PasswordType)
            {
                case "inline":
                    passwordDisplay = "Hard Coded";
                    if (showPasswordValues)
                        passwordDisplay = $"{passwordDisplay}: {item.Password}";
                    break;
                case "env_variable":
                    passwordDisplay = $"Environment variable";
                    if (showEnvironmentVariableValues)
                        passwordDisplay = $"{passwordDisplay}: {item.Password}";
                    break;
                case "prompt":
                    passwordDisplay = "User Prompt";
                    break;
                default:
                    passwordDisplay = "";
                    break;
            }

            hostDefinitionsTable.AddRow(item.Name ?? "", item.Type ?? "", item.HostName ?? "", item.UserName ?? "", passwordDisplay);
        }

        AnsiConsole.Write(hostDefinitionsTable);
    }

    /// <summary>
    /// Launch a secure session.
    /// </summary>
    /// <param name="useName">Host Definition name</param>
    /// <param name="useType">Host Definition type</param>
    /// <param name="forcePasswordPrompt">Force a password prompt regardless of password type.</param>
    public int? Execute(string useName, string useType, bool forcePasswordPrompt)
    {
        HostDefinition? match = this.HostDefinitions
            .FirstOrDefault(item => item.Name == useName && item.Type == useType);

        if (match != null)
        {
            string exeName = "";
            string args = "";

            string portArg = (match.Type == "sftp") ? "-P" : "-p";

            if (match.PasswordType == "prompt" || forcePasswordPrompt)
            {
                exeName = (match.Type == "sftp") ? "sftp" : "ssh";
                args = $"{portArg} {match.Port} {match.UserName}@{match.HostName}";
            }
            else
            {
                string? password = (match.PasswordType is not null && match.PasswordType == "inline")
                    ? match.Password
                    : Environment.GetEnvironmentVariable(match.Password ?? "");

                exeName = "sshpass";
                args = $"-p {password ?? ""} {((match.Type == "sftp") ? "sftp" : "ssh")} {portArg} {match.Port} {match.UserName}@{match.HostName}";
            }

            ProcessStartInfo psi = new()
            {
                FileName = exeName,
                Arguments = args,
                UseShellExecute = true
            };

            using Process? p = Process.Start(psi);
            p?.WaitForExit();

            return p?.ExitCode;
        }
        else
            return -99;
    }

    public void DisplayHostStatuses()
    {
        Console.Write("Checking ");

        Table hostDefinitionsTable = new();

        hostDefinitionsTable.AddColumn("Name");
        hostDefinitionsTable.AddColumn("Type");
        hostDefinitionsTable.AddColumn("Host Name");
        hostDefinitionsTable.AddColumn("Status");

        foreach (HostDefinition item in this.HostDefinitions)
        {
            bool status = NetworkUtil.Ping(item.HostName ?? "");

            hostDefinitionsTable.AddRow(item.Name ?? "", item.Type ?? "", item.HostName ?? "", (status) ? "[green]Online[/]" : "[red]Offline[/]");

            Console.Write($"{item.Name} ");
        }
        Console.WriteLine("");

        AnsiConsole.Write(hostDefinitionsTable);
    }
}
