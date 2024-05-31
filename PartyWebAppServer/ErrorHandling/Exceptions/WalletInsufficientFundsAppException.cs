using System.Net;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class WalletInsufficientFundsAppException : AppException
{
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }

    public WalletInsufficientFundsAppException(string username, int amount, CurrencyType currency)
    {
        Message = $"User {username} has insufficient funds to withdraw {amount} {currency}.";
        ErrorObject = new WalletInsufficientFundsModel
        {
            Username = username,
            Amount = amount,
            Currency = currency
        };
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
}