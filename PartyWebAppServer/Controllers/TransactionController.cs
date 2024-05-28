using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.DTOs;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using PartyWebAppCommon.enums;
using PartyWebAppServer.ErrorHandling.Exceptions;
using PartyWebAppServer.Services.TransactionService;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("/api/transactions")]
public class TransactionController(ITransactionService _transactionService)
{
    [HttpGet()]
    public async Task<List<TransactionDto>> GetTransactions() => 
        await 
            _transactionService
            .GetTransactions();

    [HttpGet("/username/{username}")]
    public async Task<List<TransactionDto>> GetUserTransactions(string username) =>
        await _transactionService.GetUserTransactions(username);

    [HttpGet("/transactionType/{transactionType}")]
    public async Task<List<TransactionDto>> GetTransactionsByType(TransactionType transactionType) =>
        await _transactionService.GetTransactionsByType(transactionType);

    [HttpPost("{id},{username},{spentCurrency},{count},{locationId},{eventId},{transactionType},{date}")]
    public async Task<TransactionDto> NewTransactionRequest(int id, string username, int spentCurrency, CurrencyType currencyType, int count,
        int locationId, int eventId, TransactionType transactionType, DateTime date)
    {
        Transaction transaction = _transactionService.NewTransactionRequest(id, username, spentCurrency, currencyType, count, locationId, eventId, transactionType, date);
        return await _transactionService.AddTransactionToDb(transaction);
    }
}