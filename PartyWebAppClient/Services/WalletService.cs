using PartyWebAppClient.Models;
using PartyWebAppClient.Services;
using PartyWebAppCommon.DTOs;

public class ClientWalletService(AppHttpClient http)
{
    public async Task<(List<WalletDto>?, AppErrorModel?)> GetUserWallets(string username)
    {
        return await http.GetAsync<List<WalletDto>>("http://localhost:5259/api/wallet/" + username);
    }

    public async Task<(WalletDto, AppErrorModel?)> CreateWallet(WalletDto wallet)
    {
        return await http.PostAsync<WalletDto>("http://localhost:5259/api/wallet", wallet);
    }
}