using System.Net;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class LocationShouldBeAtmAppException : AppException
{
    public LocationShouldBeAtmAppException(TransactionType transactionType, LocationType? locationType)
    {
        Message = $"Can only complete transaction: '{transactionType}' from ATM and is: '{locationType}'";
        ErrorObject = new  LocationShouldBeAtmErrorModel{TransactionTypeId = (int)transactionType, LocationTypeId = (int)locationType};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}