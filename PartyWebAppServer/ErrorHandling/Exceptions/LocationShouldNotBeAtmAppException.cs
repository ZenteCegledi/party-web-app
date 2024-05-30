using System.Net;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class LocationShouldNotBeAtmAppException : AppException
{
    public LocationShouldNotBeAtmAppException(TransactionType transactionType)
    {
        Message = $"Cannot complete transaction: '{transactionType.ToString()}' from ATM";
        ErrorObject = new  LocationShouldNotBeAtmErrorModel{TransactionTypeId = (int)transactionType};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}