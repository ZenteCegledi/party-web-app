using System.Net;
using PartyWebAppCommon.enums;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class UserHasNoWalletAppException : AppException
{
    public UserHasNoWalletAppException(string username, CurrencyType currencyType)
    {
        Message = $"User '{username}' has no wallet with currency type '{currencyType.ToString()}'";
        ErrorObject = new UserHasNoWalletErrorModel{Username = username, Currency = currencyType};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}