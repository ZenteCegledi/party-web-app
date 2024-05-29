using PartyWebAppClient.Models;

namespace PartyWebAppClient.Services.TransactionService;

public interface ITransactionService
{
    Task<(List<TransactionDto>?, AppErrorModel?)> GetWalletTransactions(int walletId);
}