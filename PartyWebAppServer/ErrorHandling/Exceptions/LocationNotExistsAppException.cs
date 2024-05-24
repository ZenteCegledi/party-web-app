using System.Net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class LocationNotExistsAppException : AppException
{
    public LocationNotExistsAppException(string location)
    {
        Message = $"The {location} does not exist.";
        ErrorObject = new LocationNotExistsErrorModel{Location = location};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}