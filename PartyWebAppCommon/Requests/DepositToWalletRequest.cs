using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.Requests;

public class DepositToWalletRequest
{
    public string Username { get; set; }
    public CurrencyType Currency { get; set; }
    public decimal Amount { get; set; }
}