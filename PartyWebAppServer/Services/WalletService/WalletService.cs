using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.Database;
using PartyWebAppServer.ErrorHandling.Exceptions;
using PartyWebAppServer.Database.Models;
using PartyWebAppCommon.Requests;
using AutoMapper;
namespace PartyWebAppServer.Services.WalletService;

public class WalletService(AppDbContext context, IMapper mapper) : IWalletService
{
    public async Task<List<WalletDto>> GetWallets(string username)
    {
        var user = context.Users.FirstOrDefault(u => u.Username == username);
        if (user == null) throw new UserNotFoundException("User not found");

        var wallets = context.Wallets.Where(w => w.Username == username).ToList();

        return mapper.Map<List<WalletDto>>(wallets);
    }
    public async Task<WalletDto> GetWallet(string username, CurrencyType currency)
    {
        var user = context.Users.FirstOrDefault(u => u.Username == username);
        if (user == null) throw new UserNotFoundException("No user found with that username: " + username);

        var wallet = context.Wallets.FirstOrDefault(w => w.Username == username && w.Currency == currency);
        if (wallet == null) throw new WalletNotExistsAppException();

        return mapper.Map<WalletDto>(wallet);
    }
    public async Task<WalletDto> CreateWallet(CreateWalletRequest _req)
    {
        var user = context.Users.FirstOrDefault(u => u.Username == _req.Username);
        if (user == null) throw new UserNotFoundException("No user found with that username: " + _req.Username);

        var newWallet = new Wallet
        {
            Currency = _req.Currency,
            Username = _req.Username,
            Amount = _req.Amount,
            IsPrimary = _req.IsPrimary
        };

        context.Wallets.Add(newWallet);
        context.SaveChanges();

        return mapper.Map<WalletDto>(newWallet);
    }
    public async Task<WalletDto> DeleteWallet(string username, CurrencyType currency)
    {
        var user = context.Users.FirstOrDefault(u => u.Username == username);
        if (user == null) throw new UserNotFoundException("No user found with that username: " + username);

        var wallet = context.Wallets.FirstOrDefault(w => w.Username == username && w.Currency == currency);
        if (wallet == null) throw new WalletNotExistsAppException(username, currency);

        context.Wallets.Remove(wallet);
        context.SaveChanges();

        return mapper.Map<WalletDto>(wallet);
    }
    public async Task<WalletDto> DepositToWallet(DepositToWalletRequest _req)
    {
        var user = context.Users.FirstOrDefault(u => u.Username == _req.Username);
        if (user == null) throw new UserNotFoundException("No user found with that username: " + _req.Username);

        var walletEntity = context.Wallets.FirstOrDefault(w => w.Username == _req.Username && w.Currency == _req.Currency);
        if (walletEntity == null) throw new WalletNotExistsAppException(_req.Username, _req.Currency);

        walletEntity.Amount += _req.Amount;

        // TEMPORARY - create a deposit transaction for the wallet
        var transaction = new Transaction
        {
            SpentCurrency = (int)_req.Amount,
            Count = 1,
            Date = DateTime.Now.ToUniversalTime(),
            WalletId = walletEntity.Id,
            TransactionType = TransactionType.Deposit,
        };
        context.Transactions.Add(transaction);

        context.SaveChanges();

        return mapper.Map<WalletDto>(walletEntity);
    }
    public async Task<WalletDto> WithdrawFromWallet(WithdrawFromWalletRequest _req)
    {
        var user = context.Users.FirstOrDefault(u => u.Username == _req.Username);
        if (user == null) throw new UserNotFoundException("No user found with that username: " + _req.Username);

        var walletEntity = context.Wallets.FirstOrDefault(w => w.Username == _req.Username && w.Currency == _req.Currency);
        if (walletEntity == null) throw new WalletNotExistsAppException();

        if (walletEntity.Amount < _req.Amount) throw new NotEnoughMoneyInWalletException(_req.Username, _req.Currency);

        walletEntity.Amount -= _req.Amount;
        context.SaveChanges();

        return mapper.Map<WalletDto>(walletEntity);
    }
    public async Task<WalletDto> SetPrimaryWallet(SetPrimaryWalletRequest _req)
    {
        var user = context.Users.FirstOrDefault(u => u.Username == _req.Username);
        if (user == null) throw new UserNotFoundException("No user found with that username: " + _req.Username);

        var walletEntity = context.Wallets.FirstOrDefault(w => w.Username == _req.Username && w.Currency == _req.Currency);
        if (walletEntity == null) throw new WalletNotExistsAppException();

        var primaryWallet = context.Wallets.FirstOrDefault(w => w.Username == _req.Username && w.IsPrimary);
        if (primaryWallet != null) primaryWallet.IsPrimary = false;

        walletEntity.IsPrimary = true;
        context.SaveChanges();

        return mapper.Map<WalletDto>(walletEntity);
    }
}