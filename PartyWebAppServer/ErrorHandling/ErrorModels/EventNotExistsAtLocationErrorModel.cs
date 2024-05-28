namespace PartyWebAppServer.ErrorHandling.ErrorModels;
using PartyWebAppCommon.DTOs;

public class EventNotExistsAtLocationErrorModel
{
    public int EventId { get; set; }
    public  LocationDTO Location { get; set; }
}