using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.Requests;

public class WithdrawFromWalletRequest
{
    public string Username { get; set; }
    public CurrencyType Currency { get; set; }
    public decimal Amount { get; set; }
}