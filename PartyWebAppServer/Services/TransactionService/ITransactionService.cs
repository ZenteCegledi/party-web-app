using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Services.TransactionService;

public interface ITransactionService
{
    public Task<List<TransactionDto>> GetTransactions();
    public Task<List<TransactionDto>> GetUserTransactions(string username);
    public Task<List<TransactionDto>> GetWalletTransactions(string username, CurrencyType currencyType);
    public Task<List<TransactionDto>> GetTransactionsByType(TransactionType transactionType);

    public Task<TransactionDto> NewTransaction(NewTransactionRequest transactionRequest, string? username = null);
}