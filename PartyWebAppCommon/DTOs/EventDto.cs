using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.DTOs;

public class EventDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public EventType Type { get; set; }
    public int? LocationId { get; set; }
    public int Price { get; set; }
}