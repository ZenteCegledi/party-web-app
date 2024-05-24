using System.Net;
using PartyWebAppCommon.enums;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class EventTypeDoesNotExistAppException : AppException
{
    public override string Message { get; }
        
    public override HttpStatusCode HttpStatusCode { get; }

    public EventTypeDoesNotExistAppException(EventType? type)
    {
        Message = $"EventType {type} does not exist";
        HttpStatusCode = HttpStatusCode.BadRequest;
        ErrorObject = new EventTypeDoesNotExistErrorModel() {Type = (EventType)type};
    }
}