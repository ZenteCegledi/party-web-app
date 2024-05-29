using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.Requests;

public class GetWalletRequests
{
    public string Username { get; set; }
    public CurrencyType Currency { get; set; }
}