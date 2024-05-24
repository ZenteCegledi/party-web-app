using Microsoft.AspNetCore.Mvc;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using PartyWebAppCommon.enums;
using PartyWebAppServer.ErrorHandling.Exceptions;

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
        DbContext.Transactions.Where(t => t.Wallet.Owner.Username == username).OrderBy(t => t.Date).ToList();
    
    [HttpGet("{transactionType}")]
    public async Task<List<Transaction>> GetTransaction(TransactionType transactionType) =>
        DbContext.Transactions.Where(t => t.TransactionType == transactionType).OrderBy(t => t.Date).ToList();

    [HttpPost("{id},{username},{spentCurrency},{count},{locationName},{eventName},{transactionType},{date}")]
    public async void NewTransactionRequest(int id, string username, int spentCurrency, CurrencyType currencyType, int count,
        string? locationName, string? eventName, TransactionType transactionType, DateTime date)
    {
        if (id == null) throw new ArgumentNullException(nameof(id), "Id cannot be empty");
        if (username == null) throw new ArgumentNullException(nameof(username), "Username cannot be empty");
        if (spentCurrency == null) throw new ArgumentNullException(nameof(spentCurrency), "Spent currency cannot be empty");
        if (count == null) throw new ArgumentNullException(nameof(count), "Count cannot be empty");
        if (transactionType == null) throw new ArgumentNullException(nameof(transactionType), "Transaction type cannot be empty");
        if (currencyType == null) throw new ArgumentNullException(nameof(currencyType), "Currency type cannot be empty");
        if (date == null) throw new ArgumentNullException(nameof(date), "Date cannot be empty");

        Location? location = DbContext.Locations.FirstOrDefault(l => l.Name == locationName);
        Event? currentEvent = DbContext.Events.FirstOrDefault(e => e.Name == eventName);
        Wallet? wallet = DbContext.Wallets.FirstOrDefault(w => w.Owner.Username == username && w.Currency == currencyType);
        User? user = DbContext.Users.FirstOrDefault(u => u.Username == username);
        
        if (user == null) throw new ArgumentException("User does not exist.");
        
        switch (transactionType)
        {
            case TransactionType.Food:
                if (location == null) throw new LocationNotExistsAppException(locationName);
                if (location.Type == LocationType.ATM) throw new ArgumentException("Cannot buy food from ATM.");
                if (wallet == null) throw new ArgumentException("Username does not exist.");
                if (currencyType != wallet.Currency) throw new ArgumentException("Spent- and wallet currency does not match.");

                if (wallet.Amount < spentCurrency) 
                    throw new ArithmeticException("Insufficient funds.");
                break;
            case TransactionType.Ticket:
                if (location == null) throw new ArgumentException("Location does not exist.");
                if (location.Type == LocationType.ATM) throw new ArgumentException("Cannot buy ticket from ATM.");
                if (currentEvent == null) throw new ArgumentException("Event does not exist.");
                if (wallet == null) throw new ArgumentException("Username does not exist.");
                if (currencyType != wallet.Currency) throw new ArgumentException("Spent- and wallet currency does not match.");
                
                if (wallet.Amount < spentCurrency)
                    throw new ArithmeticException("Insufficent funds.");
                break;
            case TransactionType.Deposit:
                if (location == null) throw new ArgumentException("Location does not exist.");
                if (location.Type != LocationType.ATM) throw new ArgumentException("Only deposit to ATM.");
                if (currentEvent == null) throw new ArgumentException("Event does not exist.");

                if (wallet == null)
                    wallet = new Wallet { Currency = currencyType, Owner = user, Amount = 0};
                break;
            case TransactionType.Credit:
                if (location != null || currentEvent == null) throw new ArgumentException("Location and event should be empty.");
                if (location.Type != LocationType.ATM) throw new ArgumentException("Only deposit to ATM.");
                if (wallet == null) throw new ArgumentException("");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, "Transaction type does not exist.");
        }
        
        Transaction transaction = new Transaction{ Id = id, Wallet = wallet, SpentCurrency = spentCurrency, Count = count, Location = location, Event = currentEvent, TransactionType = transactionType, Date = date};
    }
}