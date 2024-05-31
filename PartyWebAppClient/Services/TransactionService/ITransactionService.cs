using PartyWebAppClient.Models;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppClient.Services.TransactionService;

public interface ITransactionService
{
    Task<(List<TransactionDto>?, AppErrorModel?)> GetWalletTransactions(int walletId);
}