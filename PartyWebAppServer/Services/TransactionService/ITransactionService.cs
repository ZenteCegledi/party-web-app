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
    public Task<List<TransactionDTO>> GetTransactions();
    public Task<List<TransactionDTO>> GetUserTransactions(string username);
    public Task<List<TransactionDTO>> GetTransactionsByType(TransactionType transactionType);
    
    public Transaction CreateTransaction(NewTransactionRequest transactionRequest);
    public Transaction ExecuteTransaction(Transaction transaction);
    public Task<TransactionDTO> AddTransactionToDb(Transaction transaction);
}