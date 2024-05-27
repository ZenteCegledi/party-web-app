using System.Net;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class EmailAlreadyInUseException : AppException
{
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }

    public EmailAlreadyInUseException(string email)
    {
        Message = $"{email} already in use.";
        ErrorObject = new EmailAlreadyInUseModel() { Email = email};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
}