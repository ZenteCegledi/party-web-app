using PartyWebAppClient.Models;
using PartyWebAppCommon.DTOs;

public interface IClientWalletService
{
    Task<(List<WalletDto>?, AppErrorModel?)> GetUserWallets(string username);
    Task<(WalletDto, AppErrorModel?)> CreateWallet(WalletDto wallet);
    Task<(WalletDto, AppErrorModel?)> UpdateWallet(WalletDto wallet);
    Task<(WalletDto, AppErrorModel?)> DeleteWallet(WalletDto wallet);
    Task<(WalletDto, AppErrorModel?)> SetPrimaryWallet(WalletDto wallet);
    Task<(WalletDto, AppErrorModel?)> DepositToWallet(WalletDto wallet, decimal amount);
    Task<(WalletDto, AppErrorModel?)> WithdrawFromWallet(WalletDto wallet, decimal amount);

    // Task<(WalletDTO, AppErrorModel?)> TransferBetweenWallets(WalletDTO fromWallet, WalletDTO toWallet, decimal amount);
}