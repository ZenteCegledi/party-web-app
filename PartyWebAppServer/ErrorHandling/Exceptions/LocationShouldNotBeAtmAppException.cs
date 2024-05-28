using System.Net;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.enums;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class LocationShouldNotBeAtmAppException : AppException
{
    public LocationShouldNotBeAtmAppException(TransactionDto transaction)
    {
        Message = $"Cannot buy {transaction.TransactionType.ToString()} from ATM";
        ErrorObject = new  LocationShouldNotBeAtmErrorModel{Transaction = transaction};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}