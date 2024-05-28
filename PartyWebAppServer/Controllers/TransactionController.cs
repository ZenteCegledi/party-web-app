using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.DTOs;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using PartyWebAppCommon.enums;
using PartyWebAppCommon.Requests;
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

    [HttpPost("{newTransactionRequest}")]
    public async Task<TransactionDto> NewTransactionRequest(NewTransactionRequest newTransactionRequest)
    {
        Transaction transaction = _transactionService.CreateTransaction(newTransactionRequest);
        _transactionService.ExecuteTransaction(transaction);
        return await _transactionService.AddTransactionToDb(transaction);
    }
}