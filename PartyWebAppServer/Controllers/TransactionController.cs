using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.DTOs;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using PartyWebAppCommon.Enums;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.ErrorHandling.Exceptions;
using PartyWebAppServer.Services.JwtService;
using PartyWebAppServer.Services.TransactionService;
using PartyWebAppServer.Services.WalletService;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TransactionController(
    ITransactionService _transactionService,
    IHttpContextAccessor httpContextAccessor,
    IJwtService jwtService)
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    [HttpGet()]
    public async Task<List<TransactionDto>> GetTransactions()
    {
        if (!jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("Only admin has access to all transactions");
        
        return await
            _transactionService
                .GetTransactions();
    }

    [HttpGet("{username}")]
    public async Task<List<TransactionDto>> GetUserTransactions(string username)
    {
        if (!jwtService.IsUserTheUser(_httpContext.Request, username) && !jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You can only access your own transactions.");
        return await _transactionService.GetUserTransactions(username);
    }

    [HttpGet("wallet/{username}/{currency}")]
    public async Task<List<TransactionDto>> GetUserTransactions(string username, CurrencyType currency)
    {
        if (!jwtService.IsUserTheUser(_httpContext.Request, username) && !jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You can only access your own transactions.");
        
        return await _transactionService.GetWalletTransactions(username, currency);
    }

    [HttpGet("type/{transactionType}")]
    public async Task<List<TransactionDto>> GetTransactionsByType(TransactionType transactionType)
    {
        if (!jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("Only admin has access to all transactions");
        
        return await _transactionService.GetTransactionsByType(transactionType);
    }

    [HttpPost()]
    public async Task<TransactionDto> NewTransactionRequest(NewTransactionRequest newTransactionRequest)
    {
        string? username = null;
        
        var token = jwtService.GetTokenFromRequest(_httpContext.Request);
        ClaimsPrincipal claims = jwtService.GetPrincipalFromToken(token);
        username = claims.FindFirst("Username").Value;
        
        if (!jwtService.IsUserTheUser(_httpContext.Request, username) && !jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You can only create your own transactions.");
        
        return await _transactionService.NewTransaction(newTransactionRequest, username);
    } 
        
}