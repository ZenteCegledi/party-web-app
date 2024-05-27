using PartyWebAppCommon.enums;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using PartyWebAppServer.ErrorHandling.Exceptions;

namespace PartyWebAppServer.Services.TransactionService;

public class TransactionService : ITransactionService
{
    public AppDbContext DbContext { get; set; }

    public List<Transaction> GetTransactions() => 
        DbContext.Transactions.ToList();

    public List<Transaction> GetUserTransactions(string username) => 
        DbContext.Transactions.Where(t => t.Wallet.Owner.Username == username).OrderBy(t => t.Date).ToList();

    public List<Transaction> GetTransactionsByType(TransactionType transactionType) => 
        DbContext.Transactions.Where(t => t.TransactionType == transactionType).OrderBy(t => t.Date).ToList();

    public Transaction NewTransactionRequest(int id, string username, int spentCurrency, CurrencyType currencyType, int count,
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
        
        return new Transaction{ Id = id, Wallet = wallet, SpentCurrency = spentCurrency, Count = count, Location = location, Event = currentEvent, TransactionType = transactionType, Date = date};
    }
    
    public void AddTransactionToDb(Transaction transaction)
    {
        switch (transaction.TransactionType)
        {
            case TransactionType.Food:
                ChargeWallet(transaction.Wallet, transaction.SpentCurrency );
                break;
            case TransactionType.Ticket:
                ChargeWallet(transaction.Wallet, transaction.SpentCurrency);
                break;
            case TransactionType.Deposit:
                DepositToWallet(transaction.Wallet, transaction.SpentCurrency);
                break;
            case TransactionType.Credit:
                throw new NotImplementedException();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        DbContext.SaveChanges();
    }
    public void ChargeWallet(Wallet wallet, int spentCurrency) =>
        wallet.Amount -= spentCurrency;
    public void DepositToWallet(Wallet wallet, int spentCurrency) =>
        wallet.Amount += spentCurrency;
}