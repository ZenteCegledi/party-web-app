using System.Net;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;
using PartyWebAppServer.ErrorHandling;

public class UserCreationException : AppException
{
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }

    public UserCreationException(string username)
    {
        Message = $"Username {username} already taken.";
        ErrorObject = new UserCreationErrorModel() { Username = username};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
}