using System.Net;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class WalletInsufficientFundsAppException : AppException
{
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }

    public WalletInsufficientFundsAppException(int walletId, int amount, CurrencyType currency)
    {
        Message = $"Wallet {walletId} has insufficient funds to withdraw {amount} {currency}.";
        ErrorObject = new WalletInsufficientFundsModel
        {
            WalletId = walletId,
            Amount = amount,
            Currency = currency
        };
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
}