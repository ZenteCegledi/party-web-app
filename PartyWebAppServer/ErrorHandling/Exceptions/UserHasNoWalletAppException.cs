using System.Net;
using PartyWebAppCommon.DTOs;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class UserHasNoWalletAppException : AppException
{
    public UserHasNoWalletAppException(UserDTO user, WalletDto wallet)
    {
        Message = $"User '{user.Username}' has no wallet with currency type '{wallet.Currency.ToString()}'";
        ErrorObject = new UserHasNoWalletErrorModel{Username = user.Username, Currency = wallet.Currency};
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }
}