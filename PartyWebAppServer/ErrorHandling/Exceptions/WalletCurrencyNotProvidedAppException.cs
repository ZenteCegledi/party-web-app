using System.Net;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.ErrorHandling.ErrorModels;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class WalletCurrencyNotProvidedAppException : AppException
{
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }

    public WalletCurrencyNotProvidedAppException()
    {
        Message = $"Currency not provided in request.";
        HttpStatusCode = HttpStatusCode.BadRequest;
    }
}