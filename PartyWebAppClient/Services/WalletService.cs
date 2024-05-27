using System.Net.Http.Json;
using PartyWebAppClient.Models;
using PartyWebAppClient.Services;
using PartyWebAppCommon.DTOs;

public class ClientWalletService(HttpClient http)
{
    public async Task<List<WalletDto>> GetUserWallets(string username)
    {
        return await http.GetFromJsonAsync<List<WalletDto>>($"http://localhost:5259/api/Wallet/{username}") ?? new List<WalletDto>();
    }

    // public async Task<(List<WalletDto>?, AppErrorModel?)> GetUserWallets(string username)
    // {        
    //     return await http.GetAsync<List<WalletDto>>("http://localhost:5259/api/wallet/" + username);
    // }

    // public async Task<(WalletDto, AppErrorModel?)> CreateWallet(WalletDto wallet)
    // {
    //     return await http.PostAsync<WalletDto>("http://localhost:5259/api/wallet", wallet);
    // }
}