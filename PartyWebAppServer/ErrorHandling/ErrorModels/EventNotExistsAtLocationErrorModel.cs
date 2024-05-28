namespace PartyWebAppServer.ErrorHandling.ErrorModels;
using PartyWebAppCommon.DTOs;

public class EventNotExistsAtLocationErrorModel
{
    public string EventName { get; set; }
    public  string LocationName { get; set; }
    public  string LocationAddress { get; set; }
}