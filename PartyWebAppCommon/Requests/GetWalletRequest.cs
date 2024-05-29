using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.Requests;

public class GetWalletRequest
{
    public string Username { get; set; }
    public CurrencyType Currency { get; set; }
}