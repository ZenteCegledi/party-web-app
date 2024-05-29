using System.Net;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class UserNotFoundException : AppException
{
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }

    public UserNotFoundException(string username)
    {
        Message = $"{username} not found.";
        ErrorObject = new UserNotFoundModel { Username = username };
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
}