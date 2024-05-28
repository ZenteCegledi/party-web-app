using PartyWebAppCommon.Enums;

namespace PartyWebAppServer.ErrorHandling.ErrorModels;

public class WalletInsufficientFundsErrorModel
{
    public string Username { get; set; }
    public int Amount { get; set; }
    public CurrencyType Currency { get; set; }
}