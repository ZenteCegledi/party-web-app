using System.Net;
using PartyWebAppCommon.DTOs;
using PartyWebAppServer.ErrorHandling.ErrorModels;


namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class EventNotExistsAppException : AppException
{
    public EventNotExistsAppException(EventDTO currentEvent)
    {
        Message = $"Event: '{currentEvent.Name}' does not exist in DB.";
        ErrorObject = new EventNotExistsErrorModel{Name = currentEvent.Name};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}