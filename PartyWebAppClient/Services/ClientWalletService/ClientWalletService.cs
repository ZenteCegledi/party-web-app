using System.Net.Http.Json;
using PartyWebAppClient.Models;
using PartyWebAppClient.Services;
using PartyWebAppCommon.DTOs;

public class ClientWalletService(IAppHttpClient http) : IClientWalletService
{
    public async Task<(List<WalletDto>?, AppErrorModel?)> GetUserWallets(string username) =>
        await http.GetAsync<List<WalletDto>>("http://localhost:5259/api/wallet/" + username);

    public async Task<(WalletDto, AppErrorModel?)> CreateWallet(WalletDto wallet) =>
        await http.PostAsync<WalletDto>("http://localhost:5259/api/wallet", wallet);

    public async Task<(WalletDto, AppErrorModel?)> UpdateWallet(WalletDto wallet) =>
        await http.PutAsync<WalletDto>("http://localhost:5259/api/wallet", wallet);

    public async Task<(WalletDto, AppErrorModel?)> DeleteWallet(WalletDto wallet) =>
        await http.DeleteAsync<WalletDto>($"http://localhost:5259/api/wallet/{wallet.Username}/{wallet.Currency}");

    public async Task<(WalletDto, AppErrorModel?)> SetPrimaryWallet(WalletDto wallet) =>
        await http.PutAsync<WalletDto>($"http://localhost:5259/api/wallet/primary/{wallet.Username}/{wallet.Currency}");

    public async Task<(WalletDto, AppErrorModel?)> DepositToWallet(WalletDto wallet, decimal amount) =>
        await http.PutAsync<WalletDto>($"http://localhost:5259/api/wallet/deposit/{wallet.Username}/{wallet.Currency}/{amount}");

    public async Task<(WalletDto, AppErrorModel?)> WithdrawFromWallet(WalletDto wallet, decimal amount) =>
        await http.PutAsync<WalletDto>($"http://localhost:5259/api/wallet/withdraw/{wallet.Username}/{wallet.Currency}/{amount}");


}