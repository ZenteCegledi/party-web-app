using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.Requests;

public class EditEventRequest
{
    public string? Name { get; set; }
    public EventType? Type { get; set; }
    public int? LocationId { get; set; }
    public int? Price { get; set; }
}