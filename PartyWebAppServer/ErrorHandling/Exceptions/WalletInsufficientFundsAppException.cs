using System.Net;
using PartyWebAppCommon.enums;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class WalletInsufficientFundsAppException : AppException
{
    public WalletInsufficientFundsAppException(int amount, CurrencyType currency)
    {
        Message = $"{amount} {currency} exceeds wallet funds.";
        ErrorObject = new WalletInsufficientFundsErrorModel{Amount = amount, Currency = currency};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}