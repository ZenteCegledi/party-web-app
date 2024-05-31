using System.Net;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class WalletNotExistsAppException : AppException
{
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }

    public WalletNotExistsAppException(string username, CurrencyType currency)
    {
        Message = $"{currency} wallet does not exist for user {username}.";
        ErrorObject = new WalletDoesNotExistModel { Username = username, Currency = currency };
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
}