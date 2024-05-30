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
    public async Task<List<TransactionDto>> GetTransactions() => 
        await 
            _transactionService
            .GetTransactions();

    [HttpGet("{username}")]
    public async Task<List<TransactionDto>> GetUserTransactions(string username) =>
        await _transactionService.GetUserTransactions(username);
    
    [HttpGet("wallet/{username}/{currency}")]
    public async Task<List<TransactionDto>> GetUserTransactions(string username, CurrencyType currency) =>
        await _transactionService.GetWalletTransactions(username, currency);

    [HttpGet("type/{transactionType}")]
    public async Task<List<TransactionDto>> GetTransactionsByType(TransactionType transactionType) =>
        await _transactionService.GetTransactionsByType(transactionType);

    [HttpPost()]
    public async Task<TransactionDto> NewTransactionRequest(NewTransactionRequest newTransactionRequest) =>
        await _transactionService.NewTransaction(newTransactionRequest);
}