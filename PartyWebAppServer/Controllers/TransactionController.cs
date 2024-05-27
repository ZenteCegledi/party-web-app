using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using PartyWebAppCommon.enums;
using PartyWebAppServer.ErrorHandling.Exceptions;
using PartyWebAppServer.Services.TransactionService;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("/api/transactions")]
public class TransactionController
{
    private AppDbContext DbContext { get; set; }
    private TransactionService TransactionService { get; set; }

    public TransactionController(AppDbContext dbContext)
    {
        DbContext = dbContext;
        TransactionService = new TransactionService { DbContext = dbContext };
    }

    [HttpGet()]
    public async Task<List<Transaction>> GetTransactions() => TransactionService.GetTransactions();

    [HttpGet("{username}")]
    public async Task<List<Transaction>> GetUserTransactions(string username) =>
        TransactionService.GetUserTransactions(username);

    [HttpGet("{transactionType}")]
    public async Task<List<Transaction>> GetTransactionsByType(TransactionType transactionType) =>
        TransactionService.GetTransactionsByType(transactionType);

    [HttpPost("{id},{username},{spentCurrency},{count},{locationId},{eventId},{transactionType},{date}")]
    public async Task<Transaction> NewTransactionRequest(int id, string username, int spentCurrency, CurrencyType currencyType, int count,
        int locationId, int eventId, TransactionType transactionType, DateTime date)
    {
        Transaction transaction = TransactionService.NewTransactionRequest(id, username, spentCurrency, currencyType, count, locationId, eventId, transactionType, date);
        TransactionService.AddTransactionToDb(transaction);
        return transaction;
    }
}