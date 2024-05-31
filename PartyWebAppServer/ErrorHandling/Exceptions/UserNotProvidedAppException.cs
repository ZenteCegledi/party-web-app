using System.Net;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class UserNotProvidedAppException : AppException
{
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }

    public UserNotProvidedAppException()
    {
        Message = $"User not provided in request.";
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
}