using System.Net;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class WalletInsufficientFundsAppException : AppException
{
    public WalletInsufficientFundsAppException(int walletId, int amount, CurrencyType currency)
    {
        Message = $"wallet with id: {walletId} has insufficient funds: {amount} {currency}";
        ErrorObject = new WalletInsufficientFundsErrorModel{WalletId = walletId, Amount = amount, Currency = (int)currency};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}