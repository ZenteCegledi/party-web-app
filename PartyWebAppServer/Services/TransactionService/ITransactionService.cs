using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.enums;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Services.TransactionService;

public interface ITransactionService
{
    public Task<List<TransactionDto>> GetTransactions();
    public Task<List<TransactionDto>> GetUserTransactions(string username);
    public Task<List<TransactionDto>> GetTransactionsByType(TransactionType transactionType);
    
    public Transaction NewTransactionRequest(int id, string username, int spentCurrency, CurrencyType currencyType, int count,
        int locationId, int eventId, TransactionType transactionType, DateTime date);
    public Task<TransactionDto> AddTransactionToDb(Transaction transaction);
}