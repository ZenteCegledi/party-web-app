using System.Net;
using PartyWebAppCommon.enums;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class LocationNotAtmAppException : AppException
{
    public LocationNotAtmAppException(LocationType? type)
    {
        Message = $"Cannot Deposit from {type.ToString()}";
        ErrorObject = new  LocationNotAtmErrorModel{LocationType = type.ToString()};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}