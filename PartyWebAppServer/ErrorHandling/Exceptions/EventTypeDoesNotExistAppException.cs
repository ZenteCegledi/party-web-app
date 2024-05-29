using System.Net;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class EventTypeDoesNotExistAppException : AppException
{
    public override string Message { get; }

    public override HttpStatusCode HttpStatusCode { get; }

    public EventTypeDoesNotExistAppException(int type)
    {
        Message = $"EventType {type} does not exist";
        HttpStatusCode = HttpStatusCode.BadRequest;
        ErrorObject = new EventTypeDoesNotExistErrorModel { Type = type };
    }
}