using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.DTOs;

public class EventDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public EventType Type { get; set; }
    public int? LocationId { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
}