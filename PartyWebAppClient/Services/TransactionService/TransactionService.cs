using PartyWebAppClient.Models;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppClient.Services.TransactionService;

public class TransactionService(IAppHttpClient http) : ITransactionService
{
    public async Task<(List<TransactionDto>?, AppErrorModel?)> GetWalletTransactions(int walletId) =>
        await http.GetAsync<List<TransactionDto>>($"http://localhost:5259/api/transaction/{walletId}");
}