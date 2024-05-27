using System.Net;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class LocationAlreadyExistsAppException : AppException
{
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }

    public LocationAlreadyExistsAppException(string name, string address)
    {
        Message = $"Location with the name: {name} and address: {address} already exists.";
        HttpStatusCode = HttpStatusCode.BadRequest;
        ErrorObject = new LocationAlreadyExistsErrorModel { Name = name, Address = address };
    }
}