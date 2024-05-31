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
        Message = $"Wallet for user {username} with currency {currency} does not exist.";
        ErrorObject = new WalletNotFoundModel
        {
            Username = username,
            Currency = currency
        };
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
}