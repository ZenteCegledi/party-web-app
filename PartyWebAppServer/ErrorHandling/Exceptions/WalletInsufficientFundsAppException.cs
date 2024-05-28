using System.Net;
using PartyWebAppCommon.DTOs;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class WalletInsufficientFundsAppException : AppException
{
    public WalletInsufficientFundsAppException(WalletDto wallet, int amount)
    {
        Message = $"{wallet.Username}'s wallet has insufficient funds: {amount} {wallet.Currency}";
        ErrorObject = new WalletInsufficientFundsErrorModel{Username = wallet.Username, Amount = amount, Currency = wallet.Currency};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}