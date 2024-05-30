using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using PartyWebAppServer.ErrorHandling.Exceptions;

namespace PartyWebAppServer.Services.TransactionService;

public class TransactionService(AppDbContext _dbContext, IMapper _mapper) : ITransactionService
{
    public async Task<List<TransactionDTO>> GetTransactions() =>  
        _mapper
            .Map<List<TransactionDTO>>(
                await 
                    _dbContext
                    .Transactions
                    .ToListAsync()
                );

    public async Task<List<TransactionDTO>> GetUserTransactions(string username) => 
        _mapper
            .Map<List<TransactionDTO>>(
                await 
                    _dbContext
                        .Transactions
                        .Where(t => t.Wallet.Owner.Username == username)
                        .OrderBy(t => t.Date)
                        .ToListAsync()
                    );

    public async Task<List<TransactionDTO>> GetTransactionsByType(TransactionType transactionType) => 
        _mapper
            .Map<List<TransactionDTO>>(
                await 
                    _dbContext
                        .Transactions
                        .Where(t => t.TransactionType == transactionType)
                        .OrderBy(t => t.Date)
                        .ToListAsync()
                    );

    public Transaction CreateTransaction(NewTransactionRequest newTransactionRequest)
    {
        User user = _dbContext.Users.FirstOrDefault(u => u.Username == newTransactionRequest.User.Username) ?? throw new UserNotExistsAppException(newTransactionRequest.User);
        Wallet? wallet = _dbContext.Wallets.FirstOrDefault(w => w.Owner.Username == newTransactionRequest.Wallet.Username && w.Currency == newTransactionRequest.Wallet.Currency);

        Location? location = _dbContext.Locations.FirstOrDefault(l => l.Name == newTransactionRequest.Location.Name && l.Address == newTransactionRequest.Location.Address);
        Event? currentEvent = location != null ? _dbContext.Events.FirstOrDefault(e => e.Location.Id == newTransactionRequest.Event.LocationId && e.Type == newTransactionRequest.Event.Type) : null;

        if (currentEvent.Location != location)
            throw new EventNotExistsAtLocationAppException(newTransactionRequest.Event, newTransactionRequest.Location);
        
        switch (newTransactionRequest.TransactionType)
        {
            case TransactionType.Food:
                if (location == null) throw new LocationNotExistsAppException(newTransactionRequest.Location);
                if (location.Type == LocationType.ATM) throw new LocationShouldNotBeAtmAppException(newTransactionRequest.TransactionType);
                if (wallet == null) throw new UserHasNoWalletAppException(newTransactionRequest.User, newTransactionRequest.Wallet);

                if (wallet.Amount < newTransactionRequest.SpentCurrency) 
                    throw new WalletInsufficientFundsAppException(newTransactionRequest.Wallet, newTransactionRequest.SpentCurrency);
                break;
            case TransactionType.Ticket:
                if (location == null) throw new LocationNotExistsAppException(newTransactionRequest.Location);
                if (location.Type == LocationType.ATM) throw new LocationShouldNotBeAtmAppException(newTransactionRequest.TransactionType);
                if (currentEvent == null) throw new EventNotExistsAppException(newTransactionRequest.Event);
                if (wallet == null) throw new UserHasNoWalletAppException(newTransactionRequest.User, newTransactionRequest.Wallet);
                
                if (wallet.Amount < newTransactionRequest.SpentCurrency)
                    throw new WalletInsufficientFundsAppException(newTransactionRequest.Wallet, newTransactionRequest.SpentCurrency);
                break;
            case TransactionType.Deposit:
                if (location == null) throw new LocationNotExistsAppException(newTransactionRequest.Location);
                if (location.Type != LocationType.ATM) throw new LocationShouldBeAtmAppException(newTransactionRequest.Location);

                if (wallet == null)
                    wallet = new Wallet { Currency = newTransactionRequest.Wallet.Currency, Owner = user, Amount = 0};
                break;
            case TransactionType.Credit:
                location = null;
                currentEvent = null;
                if (wallet == null) 
                    throw new UserHasNoWalletAppException(newTransactionRequest.User, newTransactionRequest.Wallet);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newTransactionRequest.TransactionType), newTransactionRequest.TransactionType, "Transaction type does not exist.");
        }
        
        return new 
            Transaction
            {
                Wallet = wallet, 
                SpentCurrency = newTransactionRequest.SpentCurrency, 
                Count = newTransactionRequest.Count, 
                Location = location, 
                Event = currentEvent, 
                TransactionType = newTransactionRequest.TransactionType, 
                Date = newTransactionRequest.Date
            };
    }

    public Transaction ExecuteTransaction(Transaction transaction)
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

        return transaction;
    }
    private void ChargeWallet(Wallet wallet, int spentCurrency) =>
        wallet.Amount -= spentCurrency;
    private void DepositToWallet(Wallet wallet, int spentCurrency) =>
        wallet.Amount += spentCurrency;
    
    public async Task<TransactionDTO> AddTransactionToDb(Transaction transaction)
    {
        _dbContext.Transactions.Add(transaction);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<TransactionDTO>(transaction);
    }
}