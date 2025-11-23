namespace SSH.Navigator.Models;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class AppSettings
{
    [JsonPropertyName("Settings")]
    public Settings? Settings { get => field; set => field = value; }

    [JsonPropertyName("HostDefinitions")]
    public List<HostDefinition>? HostDefinitions { get => field; set => field = value; }

    [JsonPropertyName("HostDefinitionHelp")]
    public HostDefinitionHelp? HostDefinitionHelp { get => field; set => field = value; }
}

public class Settings
{
    [JsonPropertyName("Title")]
    public string? Title { get => field; set => field = value; }

    [JsonPropertyName("Timeout")]
    public int? Timeout { get => field; set => field = value; }
}

public class HostDefinition
{
    [JsonPropertyName("Name")]
    public string? Name { get => field; set => field = value; }

    [JsonPropertyName("Type")]
    public string? Type { get => field; set => field = value; }

    [JsonPropertyName("HostName")]
    public string? HostName { get => field; set => field = value; }

    [JsonPropertyName("UserName")]
    public string? UserName { get => field; set => field = value; }

    [JsonPropertyName("Password")]
    public string? Password { get => field ?? ""; set => field = value; }

    [JsonPropertyName("PasswordType")]
    public string? PasswordType { get => field ?? "prompt"; set => field = value; }

    [JsonPropertyName("Port")]
    public int? Port { get => field ?? 22; set => field = value; }
}

public class HostDefinitionHelp
{
    [JsonPropertyName("ValidConnectionTypes")]
    public string? ValidConnectionTypes { get => field; set => field = value; }

    [JsonPropertyName("ValidPasswordTypes")]
    public string? ValidPasswordTypes { get => field; set => field = value; }

    [JsonPropertyName("DefaultValues")]
    public List<HostDefinitionDefaultValues>? DefaultValues { get => field; set => field = value; }
}

public class HostDefinitionDefaultValues
{
    [JsonPropertyName("Password")]
    public string? Password { get => field; set => field = value; }

    [JsonPropertyName("PasswordType")]
    public string? PasswordType { get => field; set => field = value; }

    [JsonPropertyName("Port")]
    public int? Port { get => field; set => field = value; }
}