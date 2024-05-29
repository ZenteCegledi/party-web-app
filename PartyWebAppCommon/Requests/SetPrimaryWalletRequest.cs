using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.Requests;

public class SetPrimaryWalletRequest
{
    public string Username { get; set; }
    public CurrencyType Currency { get; set; }
}