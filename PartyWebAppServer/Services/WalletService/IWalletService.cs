using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppCommon.Requests;

namespace PartyWebAppServer.Services.WalletService;

public interface IWalletService
{
    public Task<List<WalletDto>> GetWallets(string username);
    public Task<WalletDto> GetWallet(string username, CurrencyType currency);
    public Task<WalletDto> GetWalletById(int id);
    public Task<WalletDto> CreateWallet(CreateWalletRequest _req);
    public Task<WalletDto> DeleteWallet(string username, CurrencyType currency);
    public Task<WalletDto> DepositToWallet(DepositToWalletRequest _req);
    public Task<WalletDto> WithdrawFromWallet(WithdrawFromWalletRequest _req);
    public Task<WalletDto> SetPrimaryWallet(SetPrimaryWalletRequest _req);
}