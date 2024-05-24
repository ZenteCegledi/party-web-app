using System.Net;
using PartyWebAppCommon.enums;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class LocationIsAtmAppException : AppException
{
    public LocationIsAtmAppException(TransactionType type)
    {
        Message = $"Cannot buy {type.ToString()} from ATM";
        ErrorObject = new  LocationIsAtmErrorModel{LocationType = "ATM"};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}