using PartyWebAppClient.Models;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppCommon.Requests;

namespace PartyWebAppClient.Services.WalletService;

public interface IWalletService
{
    Task<(List<WalletDto>?, AppErrorModel?)> GetUserWallets(string username);
    Task<(WalletDto, AppErrorModel?)> CreateWallet(CreateWalletRequest _req);
    Task<(WalletDto, AppErrorModel?)> DeleteWallet(string username, CurrencyType currency);
    Task<(WalletDto, AppErrorModel?)> SetPrimaryWallet(SetPrimaryWalletRequest _req);
    Task<(WalletDto, AppErrorModel?)> DepositToWallet(DepositToWalletRequest _req);
    Task<(WalletDto, AppErrorModel?)> WithdrawFromWallet(WithdrawFromWalletRequest _req);
}