using System.Net;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class UserNotExistsAppException : AppException
{
    public UserNotExistsAppException(string username)
    {
        Message = $"User with '{username}' username does not exist.";
        ErrorObject = new UserNotExistsErrorModel{ Username = username};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}