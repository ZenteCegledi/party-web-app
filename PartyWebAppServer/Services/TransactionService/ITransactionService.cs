using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.enums;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Services.TransactionService;

public interface ITransactionService
{
    public List<Transaction> GetTransactions();
    public List<Transaction> GetUserTransactions(string username);
    public List<Transaction> GetTransactionsByType(TransactionType transactionType);
    
    public Transaction NewTransactionRequest(int id, string username, int spentCurrency, CurrencyType currencyType, int count,
        int locationId, int eventId, TransactionType transactionType, DateTime date);
    public void AddTransactionToDb(Transaction transaction);
}