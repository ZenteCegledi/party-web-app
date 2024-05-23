using PartyWebAppCommon.enums;

namespace PartyWebAppCommon.DTOs
{
    public class WalletDto
    {
        public CurrencyType Currency { get; set; }
        public string Username { get; set; }
        public decimal Amount { get; set; }

        public override string ToString()
        {
            return $"Wallet: {Currency} {Amount}";
        }
    };
};