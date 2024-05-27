using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.Database;
using PartyWebAppServer.ErrorHandling.Exceptions;

namespace PartyWebAppServer.Services;

public interface IWalletService
{
    List<WalletDto> GetWallets(string username);
    WalletDto GetWallet(string username, CurrencyType currency);
}

public class ServerWalletService(AppDbContext context)
{
    public List<WalletDto> GetWallets(string username)
    {
        var user = context.Users.FirstOrDefault(u => u.Username == username);
        if (user == null) throw new Exception("User not found");

        var wallets = context.Wallets.Where(w => w.Username == username).ToList();

        return wallets.Select(w => new WalletDto
        {
            Currency = w.Currency,
            Username = w.Username,
            Amount = w.Amount,
            IsPrimary = w.IsPrimary
        }).ToList();
    }

    public WalletDto GetWallet(string username, CurrencyType currency)
    {
        var user = context.Users.FirstOrDefault(u => u.Username == username);
        if (user == null) throw new Exception("No user found with that username: " + username);

        var wallet = context.Wallets.FirstOrDefault(w => w.Username == username && w.Currency == currency);
        if (wallet == null) throw new WalletNotExistsAppException();

        return new WalletDto
        {
            Currency = wallet.Currency,
            Username = wallet.Username,
            Amount = wallet.Amount,
            IsPrimary = wallet.IsPrimary
        };
    }
}