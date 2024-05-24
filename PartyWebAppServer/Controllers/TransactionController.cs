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

    [HttpPost("{id},{username},{spentCurrency},{count},{locationId},{eventId},{transactionType},{date}")]
    public async void NewTransactionRequest(int id, string username, int spentCurrency, CurrencyType currencyType, int count,
        int locationId, int eventId, TransactionType transactionType, DateTime date)
    {
        Location? location = DbContext.Locations.FirstOrDefault(l => l.Id == locationId);
        Event? currentEvent = DbContext.Events.FirstOrDefault(e => e.Id == eventId);
        Wallet? wallet = DbContext.Wallets.FirstOrDefault(w => w.Owner.Username == username && w.Currency == currencyType);
        User? user = DbContext.Users.FirstOrDefault(u => u.Username == username);
        
        if (user == null) throw new UserNotExistsAppException(username);
        
        switch (transactionType)
        {
            case TransactionType.Food:
                if (location == null) throw new LocationNotExistsAppException(locationId);
                if (location.Type == LocationType.ATM) throw new LocationIsAtmAppException(transactionType);
                if (wallet == null) throw new UserHasNoWalletAppException(username, currencyType);

                if (wallet.Amount < spentCurrency) 
                    throw new WalletInsufficientFundsAppException(spentCurrency, currencyType);
                break;
            case TransactionType.Ticket:
                if (location == null) throw new LocationNotExistsAppException(locationId);
                if (location.Type == LocationType.ATM) throw new LocationIsAtmAppException(transactionType);
                if (currentEvent == null) throw new EventNotExistsAppException(eventId);
                if (wallet == null) throw new UserHasNoWalletAppException(username, currencyType);
                
                if (wallet.Amount < spentCurrency)
                    throw new WalletInsufficientFundsAppException(spentCurrency, currencyType);
                break;
            case TransactionType.Deposit:
                if (location == null) throw new LocationNotExistsAppException(locationId);
                if (location.Type != LocationType.ATM) throw new LocationNotAtmAppException(location.Type);

                if (wallet == null)
                    wallet = new Wallet { Currency = currencyType, Owner = user, Amount = 0};
                break;
            case TransactionType.Credit:
                location = null;
                currentEvent = null;
                if (wallet == null) throw new UserHasNoWalletAppException(username, currencyType);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, "Transaction type does not exist.");
        }
        
        Transaction transaction = new Transaction{ Id = id, Wallet = wallet, SpentCurrency = spentCurrency, Count = count, Location = location, Event = currentEvent, TransactionType = transactionType, Date = date};
    }
}