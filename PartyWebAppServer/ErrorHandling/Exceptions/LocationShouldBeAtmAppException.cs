using System.Net;
using PartyWebAppCommon.DTOs;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class LocationShouldBeAtmAppException : AppException
{
    public LocationShouldBeAtmAppException(LocationDTO location)
    {
        Message = $"Cannot Deposit from {location.Type}";
        ErrorObject = new  LocationShouldBeAtmErrorModel{Name = location.Name, Address = location.Address};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}