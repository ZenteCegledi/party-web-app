using PartyWebAppCommon.Enums;

namespace PartyWebAppServer.ErrorHandling.ErrorModels;

public class WalletInsufficientFundsModel
{
    public int WalletId { get; set; }
    public int Amount { get; set; }
    public CurrencyType Currency { get; set; }
}