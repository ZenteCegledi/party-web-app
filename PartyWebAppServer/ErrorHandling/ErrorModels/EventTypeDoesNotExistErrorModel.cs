using PartyWebAppCommon.Enums;

namespace PartyWebAppServer.ErrorHandling.ErrorModels;

public class EventTypeDoesNotExistErrorModel
{
    public EventType Type { get; set; }
}