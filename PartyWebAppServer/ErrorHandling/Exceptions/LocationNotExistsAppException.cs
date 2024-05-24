using System.Net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class LocationNotExistsAppException : AppException
{
    public LocationNotExistsAppException(int locationId)
    {
        Message = $"Location with id: '{locationId}' does not exist.";
        ErrorObject = new LocationNotExistsErrorModel{LocationId = locationId};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}