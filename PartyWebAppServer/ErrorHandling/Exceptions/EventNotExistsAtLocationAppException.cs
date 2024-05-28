using System.Net;
using PartyWebAppServer.ErrorHandling.ErrorModels;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class EventNotExistsAtLocationAppException : AppException
{
    public EventNotExistsAtLocationAppException(int eventId, LocationDTO location)
    {
        Message = $"Event with id: '{eventId}' does not exist at location: {location.Name}, {location.Address}.";
        ErrorObject = new EventNotExistsAtLocationErrorModel{EventId = eventId, Location = location};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}