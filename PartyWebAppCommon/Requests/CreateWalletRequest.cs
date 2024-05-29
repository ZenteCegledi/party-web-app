using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.Requests;

public class CreateWalletRequest
{
    public string Username { get; set; }
    public CurrencyType Currency { get; set; }
    public decimal Amount { get; set; }
    public bool IsPrimary { get; set; }
}