using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.DTOs;

public class EventDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public EventType Type { get; set; }
    public int? LocationId { get; set; }
    public LocationDto? Location { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public RepourProviderDto RepourProvider { get; set; }

}