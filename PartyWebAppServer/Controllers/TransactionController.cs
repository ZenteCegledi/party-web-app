using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("/api/transactions")]
public class TransactionController
{
    private AppDbContext DbContext { get; set; }

    public TransactionController(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    [HttpGet()]
    public async Task<List<Transaction>> GetTransactions() => DbContext.Transactions.ToList();

    [HttpGet("{username}")]
    public async Task<List<Transaction>> GetTransaction(string username) =>
        DbContext.Transactions.Where(t => t.User.Username == username).OrderBy(t => t.Date).ToList();
    
    [HttpGet("{transactionType}")]
    public async Task<List<Transaction>> GetTransaction(Transaction.TransactionTypes transactionType) =>
        DbContext.Transactions.Where(t => t.TransactionType == transactionType).OrderBy(t => t.Date).ToList();

    [HttpPost("{id},{username},{walletId},{spentCurrency},{count},{locationId},{eventId},{transactionType},{date}")]
    public async void NewTransactionRequest(int id, string username, int walletId, int spentCurrency, int count,
        int locationId, int eventId, Transaction.TransactionTypes transactionType, DateTime date)
    {
        User user = DbContext.Users.Where(u => u.Username == username).FirstOrDefault();
        Wallet wallet = DbContext.Wallets.Where(w => w.Id == walletId).FirstOrDefault();
        
        Transaction transaction = new Transaction{ Id = id, User = user, Wallet = wallet};
    }
}