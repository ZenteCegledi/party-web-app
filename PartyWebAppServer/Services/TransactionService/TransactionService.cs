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
                        .OrderByDescending(t => t.Date)
                        .ToListAsync()
                    );

    public async Task<List<TransactionDto>> GetWalletTransactions(string username, CurrencyType currencyType) =>
        _mapper
            .Map<List<TransactionDto>>(
                await 
                    _dbContext
                        .Transactions
                        .Where(t => t.Wallet.Owner.Username == username && t.Wallet.Currency == currencyType)
                        .OrderByDescending(t => t.Date)
                        .ToListAsync()
            );
    
    public async Task<List<TransactionDto>> GetTransactionsByType(TransactionType transactionType) => 
        _mapper
            .Map<List<TransactionDto>>(
                await 
                    _dbContext
                        .Transactions
                        .Where(t => t.TransactionType == transactionType)
                        .OrderByDescending(t => t.Date)
                        .ToListAsync()
                    );
    
    public async Task<TransactionDto> NewTransaction(NewTransactionRequest transactionRequest)
    {
        Wallet? wallet = _dbContext.Wallets.FirstOrDefault(w => w.Id == transactionRequest.WalletId);

        Location? location = _dbContext.Locations.FirstOrDefault(l => l.Id == transactionRequest.LocationId);
        Event? currentEvent = location != null ? _dbContext.Events.FirstOrDefault(e => e.Id == transactionRequest.EventId) : null;

        if (currentEvent != null && currentEvent.Location != location)
            throw new EventNotExistsAtLocationAppException(currentEvent.Name, location.Name, location.Address);
        
        switch (transactionRequest.TransactionType)
        {
            case TransactionType.Food:
                if (location == null) throw new LocationIdNotFoundAppException(transactionRequest.LocationId);
                if (location.Type == LocationType.ATM) throw new LocationShouldNotBeAtmAppException(transactionRequest.TransactionType);
                if (wallet == null) throw new WalletNotExistsAppException($"Wallet with id: {transactionRequest.WalletId} not found.");

                if (wallet.Amount < transactionRequest.Amount) 
                    throw new WalletInsufficientFundsAppException(transactionRequest.WalletId, transactionRequest.Amount, wallet.Currency);
                break;
            case TransactionType.Ticket:
                if (location == null) throw new LocationIdNotFoundAppException(transactionRequest.LocationId);
                if (location.Type == LocationType.ATM) throw new LocationShouldNotBeAtmAppException(transactionRequest.TransactionType);
                if (currentEvent == null) throw new EventIdNotFoundAppException(transactionRequest.EventId);
                if (wallet == null) throw new WalletNotExistsAppException($"Wallet with id: {transactionRequest.WalletId} not found.");
                
                if (wallet.Amount < transactionRequest.Amount)
                    throw new WalletInsufficientFundsAppException(transactionRequest.WalletId, transactionRequest.Amount, wallet.Currency);
                break;
            case TransactionType.Deposit:
                if (location == null) throw new LocationIdNotFoundAppException(transactionRequest.LocationId);
                if (location.Type != LocationType.ATM) throw new LocationShouldBeAtmAppException(transactionRequest.TransactionType, location.Type);

                if (wallet == null)
                    throw new WalletNotExistsAppException($"Wallet with id: {transactionRequest.WalletId} not found.");;
                break;
            case TransactionType.Withdraw:
                if (location == null) throw new LocationIdNotFoundAppException(transactionRequest.LocationId);
                if (location.Type != LocationType.ATM) throw new LocationShouldBeAtmAppException(transactionRequest.TransactionType, location.Type);
                if (wallet == null) throw new WalletNotExistsAppException($"Wallet with id: {transactionRequest.WalletId} not found.");;
                
                if (wallet.Amount < transactionRequest.Amount)
                    throw new WalletInsufficientFundsAppException(transactionRequest.WalletId, transactionRequest.Amount, wallet.Currency);
                break;
            case TransactionType.Credit:
                location = null;
                currentEvent = null;
                
                if (wallet == null) 
                    throw new WalletNotExistsAppException($"Wallet with id: {transactionRequest.WalletId} not found.");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(transactionRequest.TransactionType), transactionRequest.TransactionType, "Transaction type does not exist.");
        }
        
        return await ExecuteTransaction(
                new Transaction
                {
                    Wallet = wallet, 
                    Amount = transactionRequest.Amount, 
                    ItemCount = transactionRequest.ItemCount, 
                    Location = location, 
                    Event = currentEvent, 
                    TransactionType = transactionRequest.TransactionType, 
                    Date = transactionRequest.Date
                }
            );
    }

    private async Task<TransactionDto> ExecuteTransaction(Transaction transaction)
    {
        switch (transaction.TransactionType)
        {
            case TransactionType.Deposit:
                DepositToWallet(transaction.Wallet, transaction.Amount);
                break;
            case TransactionType.Credit:
                throw new NotImplementedException();
                break;
            default:
                ChargeWallet(transaction.Wallet, transaction.Amount);
                break;
        }

        return await AddTransactionToDb(transaction);
    }
    private void ChargeWallet(Wallet wallet, int amount) =>
        wallet.Amount -= amount;
    private void DepositToWallet(Wallet wallet, int amount) =>
        wallet.Amount += amount;
    
    public async Task<TransactionDto> AddTransactionToDb(Transaction transaction)
    {
        _dbContext.Transactions.Add(transaction);
        await _dbContext.SaveChangesAsync();
        var transactionDto = _mapper.Map<TransactionDto>(transaction);
        transactionDto.Currency = transaction.Wallet.Currency;
        return transactionDto;
    }
}