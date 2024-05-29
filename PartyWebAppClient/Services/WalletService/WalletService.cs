using System.Net.Http.Json;
using PartyWebAppClient.Models;
using PartyWebAppClient.Services;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;

public class WalletService(IAppHttpClient http) : IWalletService, IWalletService {
    public async Task<(List<WalletDto>?, AppErrorModel?)> GetUserWallets(GetWalletRequest _req) =>
        await http.GetAsync<List<WalletDto>>("http://localhost:5259/api/wallet/" + _req.Username);

    public async Task<(WalletDto, AppErrorModel?)> CreateWallet(CreateWalletRequest _req) =>
        await http.PostAsync<WalletDto>("http://localhost:5259/api/wallet", _req);

    public async Task<(WalletDto, AppErrorModel?)> DeleteWallet(DeleteWalletRequest _req) =>
        await http.DeleteAsync<WalletDto>($"http://localhost:5259/api/wallet/{_req.Username}/{_req.Currency}");

    public async Task<(WalletDto, AppErrorModel?)> SetPrimaryWallet(SetPrimaryWalletRequest _req) =>
        await http.PutAsync<WalletDto>($"http://localhost:5259/api/wallet/primary/{_req.Username}/{_req.Currency}");

    public async Task<(WalletDto, AppErrorModel?)> DepositToWallet(DepositToWalletRequest _req) =>
        await http.PutAsync<WalletDto>($"http://localhost:5259/api/wallet/deposit/{_req.Username}/{_req.Currency}/{_req.Amount}");

    public async Task<(WalletDto, AppErrorModel?)> WithdrawFromWallet(WithdrawFromWalletRequest _req) =>
        await http.PutAsync<WalletDto>($"http://localhost:5259/api/wallet/withdraw/{_req.Username}/{_req.Currency}/{_req.Amount}");
}