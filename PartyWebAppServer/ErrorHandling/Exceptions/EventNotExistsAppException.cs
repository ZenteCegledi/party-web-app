using System.Net;
using PartyWebAppServer.ErrorHandling.ErrorModels;


namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class EventNotExistsAppException : AppException
{
    public EventNotExistsAppException(int eventId)
    {
        Message = $"Event with id: '{eventId}' does not exist.";
        ErrorObject = new EventNotExistsErrorModel{EventId = eventId};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}