using System.Net.Http.Json;
using PartyWebAppClient.Models;
using PartyWebAppClient.Services;
using PartyWebAppCommon.DTOs;

public class ClientWalletService(IAppHttpClient http) : IClientWalletService
{
    public async Task<(List<WalletDTO>?, AppErrorModel?)> GetUserWallets(string username) =>
        await http.GetAsync<List<WalletDTO>>("http://localhost:5259/api/wallet/" + username);

    public async Task<(WalletDTO, AppErrorModel?)> CreateWallet(WalletDTO wallet) =>
        await http.PostAsync<WalletDTO>("http://localhost:5259/api/wallet", wallet);

    public async Task<(WalletDTO, AppErrorModel?)> UpdateWallet(WalletDTO wallet) =>
        await http.PutAsync<WalletDTO>("http://localhost:5259/api/wallet", wallet);

    public async Task<(WalletDTO, AppErrorModel?)> DeleteWallet(WalletDTO wallet) =>
        await http.DeleteAsync<WalletDTO>($"http://localhost:5259/api/wallet/{wallet.Username}/{wallet.Currency}");

    public async Task<(WalletDTO, AppErrorModel?)> SetPrimaryWallet(WalletDTO wallet) =>
        await http.PutAsync<WalletDTO>($"http://localhost:5259/api/wallet/primary/{wallet.Username}/{wallet.Currency}");

    public async Task<(WalletDTO, AppErrorModel?)> DepositToWallet(WalletDTO wallet, decimal amount) =>
        await http.PutAsync<WalletDTO>($"http://localhost:5259/api/wallet/deposit/{wallet.Username}/{wallet.Currency}/{amount}");

    public async Task<(WalletDTO, AppErrorModel?)> WithdrawFromWallet(WalletDTO wallet, decimal amount) =>
        await http.PutAsync<WalletDTO>($"http://localhost:5259/api/wallet/withdraw/{wallet.Username}/{wallet.Currency}/{amount}");


}