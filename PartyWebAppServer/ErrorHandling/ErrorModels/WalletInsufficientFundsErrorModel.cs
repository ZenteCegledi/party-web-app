using PartyWebAppCommon.enums;

namespace PartyWebAppServer.ErrorHandling.ErrorModels;

public class WalletInsufficientFundsErrorModel
{
    public int Amount { get; set; }
    public CurrencyType Currency { get; set; }
}