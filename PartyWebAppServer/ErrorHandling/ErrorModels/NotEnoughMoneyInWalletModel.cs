using PartyWebAppCommon.Enums;

namespace PartyWebAppServer.Controllers;

public class NotEnoughMoneyInWalletModel
{
    public string Username { get; set; }
    public CurrencyType Currency { get; set; }
}