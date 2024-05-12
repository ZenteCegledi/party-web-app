namespace PartyWebAppClient.Models;

public class AppErrorModel
{
    public string Id { get; set; }
    public string? DetailedMessage { get; set; }
    public Dictionary<string, object>? ErrorObject { get; set; }
    public string Message { get; set; }
}
