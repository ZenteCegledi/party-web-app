using System.Net;

namespace PartyWebAppServer.ErrorHandling;

public abstract class AppException : Exception
{
    public string Id => GetType().Name;
    public abstract override string Message { get; }
    public string DetailedMessage { get; set; }
    public abstract HttpStatusCode HttpStatusCode { get; }
    public object ErrorObject { get; set; }

    public AppError<object> ToError()
    {
        return new AppError<object>
        {
            Id = Id,
            Message = Message,
            DetailedMessage = DetailedMessage,
            ErrorObject = ErrorObject
        };
    }
}