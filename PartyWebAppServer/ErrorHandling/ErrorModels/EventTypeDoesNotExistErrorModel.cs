using PartyWebAppCommon.enums;

namespace PartyWebAppServer.ErrorHandling.ErrorModels;

public class EventTypeDoesNotExistErrorModel
{
    public EventType Type { get; set; }
}