using PartyWebAppClient.Models;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;

namespace PartyWebAppClient.Services.WalletService;

public interface IWalletService
{
    Task<(List<WalletDto>?, AppErrorModel?)> GetUserWallets(GetWalletRequest _req);
    Task<(WalletDto, AppErrorModel?)> CreateWallet(CreateWalletRequest _req);
    Task<(WalletDto, AppErrorModel?)> DeleteWallet(DeleteWalletRequest _req);
    Task<(WalletDto, AppErrorModel?)> SetPrimaryWallet(SetPrimaryWalletRequest _req);
    Task<(WalletDto, AppErrorModel?)> DepositToWallet(DepositToWalletRequest _req);
    Task<(WalletDto, AppErrorModel?)> WithdrawFromWallet(WithdrawFromWalletRequest _req);
}