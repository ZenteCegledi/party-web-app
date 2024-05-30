using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.DTOs;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using PartyWebAppCommon.Enums;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.ErrorHandling.Exceptions;
using PartyWebAppServer.Services.TransactionService;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TransactionController(ITransactionService _transactionService)
{
    [HttpGet()]
    public async Task<List<TransactionDTO>> GetTransactions() => 
        await 
            _transactionService
            .GetTransactions();

    [HttpGet("user/{username}")]
    public async Task<List<TransactionDTO>> GetUserTransactions(string username) =>
        await _transactionService.GetUserTransactions(username);
    
    [HttpGet("wallet/{username}/{currency}")]
    public async Task<List<TransactionDTO>> GetUserTransactions(string username, CurrencyType currency) =>
        await _transactionService.GetWalletTransactions(username, currency);

    [HttpGet("type/{transactionType}")]
    public async Task<List<TransactionDTO>> GetTransactionsByType(TransactionType transactionType) =>
        await _transactionService.GetTransactionsByType(transactionType);

    [HttpPost()]
    public async Task<TransactionDTO> NewTransactionRequest(NewTransactionRequest newTransactionRequest)
    {
        Transaction transaction = _transactionService.CreateTransaction(newTransactionRequest);
        _transactionService.ExecuteTransaction(transaction);
        return await _transactionService.AddTransactionToDb(transaction);
    }
}