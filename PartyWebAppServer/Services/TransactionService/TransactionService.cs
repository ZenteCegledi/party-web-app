﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.enums;
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
        _mapper.Map<List<TransactionDto>>(await _dbContext.Transactions.Where(t => t.Wallet.Owner.Username == username).OrderBy(t => t.Date).ToListAsync());

    public async Task<List<TransactionDto>> GetTransactionsByType(TransactionType transactionType) => 
        _mapper.Map<List<TransactionDto>>(await _dbContext.Transactions.Where(t => t.TransactionType == transactionType).OrderBy(t => t.Date).ToListAsync());

    public Transaction CreateTransaction(NewTransactionRequest newTransactionRequest)
    {
        User user = _dbContext.Users.FirstOrDefault(u => u.Username == newTransactionRequest.User.Username) ?? throw new UserNotExistsAppException(newTransactionRequest.User.Username);
        Wallet? wallet = _dbContext.Wallets.FirstOrDefault(w => w.Owner.Username == newTransactionRequest.Wallet.Username && w.Currency == newTransactionRequest.Wallet.Currency);

        Location? location = _dbContext.Locations.FirstOrDefault(l => l.Name == newTransactionRequest.Location.Name && l.Address == newTransactionRequest.Location.Address);
        Event? currentEvent = location != null ? _dbContext.Events.FirstOrDefault(e => e.Location == newTransactionRequest.Event.Location && e.Type == newTransactionRequest.Event.Type) : null;

        if (currentEvent.Location != location)
            throw new NotImplementedException();
        
        switch (transactionType)
        {
            case TransactionType.Food:
                if (location == null) throw new LocationNotExistsAppException(newTransactionRequest.Location);
                if (location.Type == LocationType.ATM) throw new LocationShouldNotBeAtmAppException(newTransactionRequest.Location);
                if (wallet == null) throw new UserHasNoWalletAppException(newTransactionRequest.User, newTransactionRequest.Wallet);

                if (wallet.Amount < spentCurrency) 
                    throw new WalletInsufficientFundsAppException(newTransactionRequest.Wallet, spentCurrency);
                break;
            case TransactionType.Ticket:
                if (location == null) throw new LocationNotExistsAppException(locationId);
                if (location.Type == LocationType.ATM) throw new LocationShouldNotBeAtmAppException(transactionType);
                if (currentEvent == null) throw new EventNotExistsAppException(eventId);
                if (wallet == null) throw new UserHasNoWalletAppException(username, currencyType);
                
                if (wallet.Amount < spentCurrency)
                    throw new WalletInsufficientFundsAppException(spentCurrency, currencyType);
                break;
            case TransactionType.Deposit:
                if (location == null) throw new LocationNotExistsAppException(locationId);
                if (location.Type != LocationType.ATM) throw new LocationShouldBeAtmAppException(transactionType);

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
    
    public async Task<TransactionDto> AddTransactionToDb(Transaction transaction)
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

        await _dbContext.SaveChangesAsync();
        return _mapper.Map<TransactionDto>(transaction);
    }
    public void ChargeWallet(Wallet wallet, int spentCurrency) =>
        wallet.Amount -= spentCurrency;
    public void DepositToWallet(Wallet wallet, int spentCurrency) =>
        wallet.Amount += spentCurrency;
}