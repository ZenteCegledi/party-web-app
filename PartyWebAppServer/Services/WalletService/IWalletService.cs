using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;

namespace PartyWebAppServer.Services.WalletService;

public interface IWalletService
{
    public List<WalletDto> GetWallets(string username);
    public Task<WalletDto> CreateWallet(WalletDto wallet);
    public Task<WalletDto> DeleteWallet(string username, CurrencyType currency);
    public Task<WalletDto> DepositToWallet(WalletDto wallet, decimal amount);
    public Task<WalletDto> WithdrawFromWallet(WalletDto wallet, decimal amount);
    public Task<WalletDto> SetPrimaryWallet(WalletDto wallet);
}