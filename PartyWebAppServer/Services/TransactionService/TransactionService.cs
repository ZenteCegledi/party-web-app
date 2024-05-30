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
    public async Task<List<TransactionDto>> GetTransactions() =>  
        _mapper
            .Map<List<TransactionDto>>(
                await 
                    _dbContext
                    .Transactions
                    .ToListAsync()
                );

    public async Task<List<TransactionDto>> GetUserTransactions(string username) => 
        _mapper
            .Map<List<TransactionDto>>(
                await 
                    _dbContext
                        .Transactions
                        .Where(t => t.Wallet.Owner.Username == username)
                        .OrderBy(t => t.Date)
                        .ToListAsync()
                    );

    public async Task<List<TransactionDto>> GetWalletTransactions(string username, CurrencyType currencyType) =>
        _mapper
            .Map<List<TransactionDto>>(
                await 
                    _dbContext
                        .Transactions
                        .Where(t => t.Wallet.Owner.Username == username && t.Wallet.Currency == currencyType)
                        .OrderBy(t => t.Date)
                        .ToListAsync()
            );
    
    public async Task<List<TransactionDto>> GetTransactionsByType(TransactionType transactionType) => 
        _mapper
            .Map<List<TransactionDto>>(
                await 
                    _dbContext
                        .Transactions
                        .Where(t => t.TransactionType == transactionType)
                        .OrderBy(t => t.Date)
                        .ToListAsync()
                    );

    public Transaction CreateTransaction(NewTransactionRequest newTransactionRequest)
    {
        Wallet? wallet = _dbContext.Wallets.FirstOrDefault(w => w.Id == newTransactionRequest.WalletId);

        Location? location = _dbContext.Locations.FirstOrDefault(l => l.Id == newTransactionRequest.LocationId);
        Event? currentEvent = location != null ? _dbContext.Events.FirstOrDefault(e => e.Id == newTransactionRequest.EventId) : null;

        if (currentEvent != null && currentEvent.Location != location)
            throw new EventNotExistsAtLocationAppException(currentEvent.Name, location.Name, location.Address);
        
        switch (newTransactionRequest.TransactionType)
        {
            case TransactionType.Food:
                if (location == null) throw new LocationIdNotFoundAppException(newTransactionRequest.LocationId);
                if (location.Type == LocationType.ATM) throw new LocationShouldNotBeAtmAppException(newTransactionRequest.TransactionType);
                if (wallet == null) throw new WalletNotExistsAppException($"Wallet with id: {newTransactionRequest.WalletId} not found.");

                if (wallet.Amount < newTransactionRequest.Amount) 
                    throw new WalletInsufficientFundsAppException(newTransactionRequest.WalletId, newTransactionRequest.Amount, wallet.Currency);
                break;
            case TransactionType.Ticket:
                if (location == null) throw new LocationIdNotFoundAppException(newTransactionRequest.LocationId);
                if (location.Type == LocationType.ATM) throw new LocationShouldNotBeAtmAppException(newTransactionRequest.TransactionType);
                if (currentEvent == null) throw new EventIdNotFoundAppException(newTransactionRequest.EventId);
                if (wallet == null) throw new WalletNotExistsAppException($"Wallet with id: {newTransactionRequest.WalletId} not found.");
                
                if (wallet.Amount < newTransactionRequest.Amount)
                    throw new WalletInsufficientFundsAppException(newTransactionRequest.WalletId, newTransactionRequest.Amount, wallet.Currency);
                break;
            case TransactionType.Deposit:
                if (location == null) throw new LocationIdNotFoundAppException(newTransactionRequest.LocationId);
                if (location.Type != LocationType.ATM) throw new LocationShouldBeAtmAppException(newTransactionRequest.TransactionType, location.Type);

                if (wallet == null)
                    throw new WalletNotExistsAppException($"Wallet with id: {newTransactionRequest.WalletId} not found.");;
                break;
            case TransactionType.Withdraw:
                if (location == null) throw new LocationIdNotFoundAppException(newTransactionRequest.LocationId);
                if (location.Type != LocationType.ATM) throw new LocationShouldBeAtmAppException(newTransactionRequest.TransactionType, location.Type);
                if (wallet == null) throw new WalletNotExistsAppException($"Wallet with id: {newTransactionRequest.WalletId} not found.");;
                
                if (wallet.Amount < newTransactionRequest.Amount)
                    throw new WalletInsufficientFundsAppException(newTransactionRequest.WalletId, newTransactionRequest.Amount, wallet.Currency);
                break;
            case TransactionType.Credit:
                location = null;
                currentEvent = null;
                
                if (wallet == null) 
                    throw new WalletNotExistsAppException($"Wallet with id: {newTransactionRequest.WalletId} not found.");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newTransactionRequest.TransactionType), newTransactionRequest.TransactionType, "Transaction type does not exist.");
        }
        
        return new 
            Transaction
            {
                Wallet = wallet, 
                Amount = newTransactionRequest.Amount, 
                ItemCount = newTransactionRequest.ItemCount, 
                Location = location, 
                Event = currentEvent, 
                TransactionType = newTransactionRequest.TransactionType, 
                Date = newTransactionRequest.Date
            };
    }

    private Transaction ExecuteTransaction(Transaction transaction)
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
            case TransactionType.Withdraw:
                ChargeWallet(transaction.Wallet, transaction.SpentCurrency);
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
    
    public async Task<TransactionDto> AddTransactionToDb(Transaction transaction)
    {
        _dbContext.Transactions.Add(transaction);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<TransactionDto>(transaction);
    }
}