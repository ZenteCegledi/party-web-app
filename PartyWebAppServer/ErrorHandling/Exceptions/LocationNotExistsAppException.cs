using System.Net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PartyWebAppCommon.DTOs;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class LocationNotExistsAppException : AppException
{
    public LocationNotExistsAppException(LocationDTO location)
    {
        Message = $"Location with name:'{location.Name}' and address:'{location.Address}' does not exist in DB.";
        ErrorObject = new LocationNotExistsErrorModel{Name = location.Name, Address = location.Address};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}