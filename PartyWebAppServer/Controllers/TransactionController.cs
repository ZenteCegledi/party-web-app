using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using PartyWebAppCommon.enums;

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
    public async Task<List<Transaction>> GetTransaction(TransactionType transactionType) =>
        DbContext.Transactions.Where(t => t.TransactionType == transactionType).OrderBy(t => t.Date).ToList();

    [HttpPost("{id},{username},{spentCurrency},{count},{locationId},{eventId},{transactionType},{date}")]
    public async void NewTransactionRequest(int id, string username, int spentCurrency, int count,
        int locationId, int eventId, TransactionType transactionType, DateTime date)
    {
        Location? location = null;
        if(locationId != null)
            location = DbContext.Locations.FirstOrDefault(l => l.Id == locationId);
        Event? currentEvent = null;
        if(eventId != null)
            currentEvent = DbContext.Events.FirstOrDefault(e => e.Id == eventId);
        
        switch (transactionType)
        {
            case TransactionType.Food:
                if (location == null)
                    throw new ArgumentNullException(nameof(location),"Incorrect location.");
                if (location.Type == LocationType.ATM)
                    throw new ArgumentException("Cannot buy food from ATM.");
                if (currentEvent == null)
                    break;
                break;
            case TransactionType.Ticket:
                break;
            case TransactionType.Deposit:
                break;
            case TransactionType.Credit:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, null);
        }
        
        User user = DbContext.Users.FirstOrDefault(u => u.Username == username);
        Wallet wallet = DbContext.Wallets.FirstOrDefault(w => w.Username == username);
        
        Transaction transaction = new Transaction{ Id = id, User = user, Wallet = wallet, SpentCurrency = spentCurrency, Count = count, Location = location, Event = currentEvent, TransactionType = transactionType, Date = date};
    }
}