using PartyWebAppCommon.enums;

namespace PartyWebAppCommon.Requests;

public class CreateEventRequest
{
    public string Name { get; set; }
    public EventType Type {get ; set;}
    public int LocationId { get; set; }
    public int Price { get; set; }
}