using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.Database;
using PartyWebAppServer.ErrorHandling.Exceptions;
using PartyWebAppServer.Database.Models;
using System.ComponentModel;

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

    public WalletDto CreateWallet(WalletDto wallet)
    {
        var user = context.Users.FirstOrDefault(u => u.Username == wallet.Username);
        if (user == null) throw new Exception("No user found with that username: " + wallet.Username);

        var newWallet = new Wallet
        {
            Currency = wallet.Currency,
            Username = wallet.Username,
            Amount = wallet.Amount,
            IsPrimary = wallet.IsPrimary
        };

        context.Wallets.Add(newWallet);
        context.SaveChanges();

        return wallet;
    }

    public WalletDto DeleteWallet(string username, CurrencyType currency)
    {
        var user = context.Users.FirstOrDefault(u => u.Username == username);
        if (user == null) throw new Exception("No user found with that username: " + username);

        var wallet = context.Wallets.FirstOrDefault(w => w.Username == username && w.Currency == currency);
        if (wallet == null) throw new WalletNotExistsAppException();

        context.Wallets.Remove(wallet);
        context.SaveChanges();

        return new WalletDto
        {
            Currency = wallet.Currency,
            Username = wallet.Username,
            Amount = wallet.Amount,
            IsPrimary = wallet.IsPrimary
        };
    }

    public WalletDto DepositToWallet(WalletDto wallet, decimal amount)
    {
        var user = context.Users.FirstOrDefault(u => u.Username == wallet.Username);
        if (user == null) throw new Exception("No user found with that username: " + wallet.Username);

        var walletEntity = context.Wallets.FirstOrDefault(w => w.Username == wallet.Username && w.Currency == wallet.Currency);
        if (walletEntity == null) throw new WalletNotExistsAppException();

        walletEntity.Amount += amount;
        context.SaveChanges();

        return new WalletDto
        {
            Currency = walletEntity.Currency,
            Username = walletEntity.Username,
            Amount = walletEntity.Amount,
            IsPrimary = walletEntity.IsPrimary
        };
    }

    public WalletDto WithdrawFromWallet(WalletDto wallet, decimal amount)
    {
        var user = context.Users.FirstOrDefault(u => u.Username == wallet.Username);
        if (user == null) throw new Exception("No user found with that username: " + wallet.Username);

        var walletEntity = context.Wallets.FirstOrDefault(w => w.Username == wallet.Username && w.Currency == wallet.Currency);
        if (walletEntity == null) throw new WalletNotExistsAppException();

        if (walletEntity.Amount < amount) throw new Exception("Not enough money in the wallet");

        walletEntity.Amount -= amount;
        context.SaveChanges();

        return new WalletDto
        {
            Currency = walletEntity.Currency,
            Username = walletEntity.Username,
            Amount = walletEntity.Amount,
            IsPrimary = walletEntity.IsPrimary
        };
    }

    public WalletDto SetPrimaryWallet(WalletDto wallet)
    {
        var user = context.Users.FirstOrDefault(u => u.Username == wallet.Username);
        if (user == null) throw new Exception("No user found with that username: " + wallet.Username);

        var walletEntity = context.Wallets.FirstOrDefault(w => w.Username == wallet.Username && w.Currency == wallet.Currency);
        if (walletEntity == null) throw new WalletNotExistsAppException();

        var primaryWallet = context.Wallets.FirstOrDefault(w => w.Username == wallet.Username && w.IsPrimary);
        if (primaryWallet != null) primaryWallet.IsPrimary = false;

        walletEntity.IsPrimary = true;
        context.SaveChanges();

        return new WalletDto
        {
            Currency = walletEntity.Currency,
            Username = walletEntity.Username,
            Amount = walletEntity.Amount,
            IsPrimary = walletEntity.IsPrimary
        };
    }
}