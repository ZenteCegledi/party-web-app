using System.Net;
using PartyWebAppCommon.DTOs;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class UserNotExistsAppException : AppException
{
    public UserNotExistsAppException(UserDto user)
    {
        Message = $"User with '{user.Username}' username does not exist.";
        ErrorObject = new UserNotExistsErrorModel{User = user};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}