using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.enums;
using PartyWebAppServer.Database;

namespace PartyWebAppServer.Services;

public class WalletService
{
    
    public WalletService(AppDbContext context)
    {
        DbContext = context;
    }
    private AppDbContext DbContext { get; set; }


    public List<WalletDto> GetWallets(string username)
    {
        var wallets = DbContext.Wallets.Where(w => w.Username == username).ToList();
        return wallets.Select(w => new WalletDto
        {
            Currency = w.Currency,
            Username = w.Username,
            Amount = w.Amount
        }).ToList();
    }

    public WalletDto GetWallet(string username, CurrencyType currency)
    {
        var wallet = DbContext.Wallets.FirstOrDefault(w => w.Username == username && w.Currency == currency);
        if (wallet == null)
        {
            throw new Exception("Wallet not found");
        }
        return new WalletDto
        {
            Currency = wallet.Currency,
            Username = wallet.Username,
            Amount = wallet.Amount
        };
    }
}