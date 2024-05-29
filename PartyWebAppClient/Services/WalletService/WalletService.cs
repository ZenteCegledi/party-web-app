using PartyWebAppClient.Models;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppCommon.Requests;

namespace PartyWebAppClient.Services.WalletService;

public class WalletService(IAppHttpClient http) : IWalletService
{
    public async Task<(List<WalletDto>?, AppErrorModel?)> GetUserWallets(string username) =>
        await http.GetAsync<List<WalletDto>>($"http://localhost:5259/api/wallet/{username}");

    public async Task<(WalletDto, AppErrorModel?)> CreateWallet(CreateWalletRequest _req) =>
        await http.PostAsync<WalletDto>($"http://localhost:5259/api/wallet", _req);

    public async Task<(WalletDto, AppErrorModel?)> DeleteWallet(string username, CurrencyType currency) =>
        await http.DeleteAsync<WalletDto>($"http://localhost:5259/api/wallet/{username}/{currency}");

    public async Task<(WalletDto, AppErrorModel?)> SetPrimaryWallet(SetPrimaryWalletRequest _req) =>
        await http.PutAsync<WalletDto>($"http://localhost:5259/api/wallet/primary", _req);

    public async Task<(WalletDto, AppErrorModel?)> DepositToWallet(DepositToWalletRequest _req) =>
        await http.PutAsync<WalletDto>($"http://localhost:5259/api/wallet/deposit", _req);

    public async Task<(WalletDto, AppErrorModel?)> WithdrawFromWallet(WithdrawFromWalletRequest _req) =>
        await http.PutAsync<WalletDto>($"http://localhost:5259/api/wallet/withdraw", _req);
}