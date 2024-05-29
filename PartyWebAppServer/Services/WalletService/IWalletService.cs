using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppCommon.Requests;

namespace PartyWebAppServer.Services.WalletService;

public interface IWalletService
{
    public Task<List<WalletDto>> GetWallets(GetWalletRequests _req);
    public Task<WalletDto> GetWallet(GetWalletRequest _req);
    public Task<WalletDto> CreateWallet(CreateWalletRequest _req);
    public Task<WalletDto> DeleteWallet(DeleteWalletRequest _req);
    public Task<WalletDto> SetPrimaryWallet(SetPrimaryWalletRequest _req);
    public Task<WalletDto> DepositToWallet(DepositToWalletRequest _req);
    public Task<WalletDto> WithdrawFromWallet(WithdrawFromWalletRequest _req);
}