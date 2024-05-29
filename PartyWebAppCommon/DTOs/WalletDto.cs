using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.DTOs
{
    public class WalletDTO
    {
        public CurrencyType Currency { get; set; }
        public string Username { get; set; }
        public decimal Amount { get; set; }
        public bool IsPrimary { get; set; }
    };
};