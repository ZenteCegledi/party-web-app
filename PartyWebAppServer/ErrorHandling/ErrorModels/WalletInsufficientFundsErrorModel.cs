using PartyWebAppCommon.Enums;

namespace PartyWebAppServer.ErrorHandling.ErrorModels;

public class WalletInsufficientFundsErrorModel
{
    public int WalletId { get; set; }
    public int Amount { get; set; }
    public int Currency { get; set; }
}