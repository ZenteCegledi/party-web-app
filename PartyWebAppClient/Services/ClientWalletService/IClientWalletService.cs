using PartyWebAppClient.Models;
using PartyWebAppCommon.DTOs;

public interface IClientWalletService
{
    Task<(List<WalletDTO>?, AppErrorModel?)> GetUserWallets(string username);
    Task<(WalletDTO, AppErrorModel?)> CreateWallet(WalletDTO wallet);
    Task<(WalletDTO, AppErrorModel?)> UpdateWallet(WalletDTO wallet);
    Task<(WalletDTO, AppErrorModel?)> DeleteWallet(WalletDTO wallet);
    Task<(WalletDTO, AppErrorModel?)> SetPrimaryWallet(WalletDTO wallet);
    Task<(WalletDTO, AppErrorModel?)> DepositToWallet(WalletDTO wallet, decimal amount);
    Task<(WalletDTO, AppErrorModel?)> WithdrawFromWallet(WalletDTO wallet, decimal amount);

    // Task<(WalletDTO, AppErrorModel?)> TransferBetweenWallets(WalletDTO fromWallet, WalletDTO toWallet, decimal amount);
}