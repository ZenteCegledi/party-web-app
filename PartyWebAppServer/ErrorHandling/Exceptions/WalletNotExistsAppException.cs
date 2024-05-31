using System.Net;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class WalletNotExistsAppException : AppException
{
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }

    public WalletNotExistsAppException(string username)
    {
        Message = $"Wallet for user {username} does not have the requested wallet.";
        ErrorObject = new WalletNotFoundModel
        {
            Username = username,
        };
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
}