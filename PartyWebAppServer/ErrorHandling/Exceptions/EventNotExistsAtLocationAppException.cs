using System.Net;
using PartyWebAppServer.ErrorHandling.ErrorModels;
using PartyWebAppCommon.DTOs;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class EventNotExistsAtLocationAppException : AppException
{
    public EventNotExistsAtLocationAppException(string eventName, string locationName, string locationAddress)
    {
        Message = $"Event: '{eventName}' does not exist at location: {locationName}, {locationAddress}.";
        ErrorObject = new EventNotExistsAtLocationErrorModel{EventName = eventName, LocationName = locationName, LocationAddress = locationAddress};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}