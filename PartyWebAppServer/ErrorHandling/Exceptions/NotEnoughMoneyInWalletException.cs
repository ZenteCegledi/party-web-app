using System.Net;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.Controllers;

namespace PartyWebAppServer.ErrorHandling.Exceptions;

public class NotEnoughMoneyInWalletException : AppException
{
    public override string Message { get; }
    public override HttpStatusCode HttpStatusCode { get; }

    public NotEnoughMoneyInWalletException(string username, CurrencyType currency)
    {
        Message = $"Not enough money in wallet the {currency} for user {username}";
        HttpStatusCode = HttpStatusCode.BadRequest;
        ErrorObject = new NotEnoughMoneyInWalletModel { Username = username, Currency = currency };
    }
}