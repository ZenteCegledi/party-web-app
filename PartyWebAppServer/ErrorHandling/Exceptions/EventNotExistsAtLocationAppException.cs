using System.Net;
using PartyWebAppServer.ErrorHandling.ErrorModels;
using PartyWebAppCommon.DTOs;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class EventNotExistsAtLocationAppException : AppException
{
    public EventNotExistsAtLocationAppException(EventDto currentEvent, LocationDTO location)
    {
        Message = $"Event: '{currentEvent.Name}' does not exist at location: {location.Name}, {location.Address}.";
        ErrorObject = new EventNotExistsAtLocationErrorModel{EventName = currentEvent.Name, LocationName = location.Name, LocationAddress = location.Address};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}