﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.enums;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using PartyWebAppServer.Services;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletController
{
    public WalletController(AppDbContext context)
    {
        WalletService = new WalletService(context);
    }
    private WalletService WalletService { get; set; }

    [HttpGet("{username}")]
    // public async Task<List<Wallet>> GetWallets(string username)
    // {
    //     return await DbContext.Wallets.Where(w => w.Username == username).ToListAsync();
    // }
    
    // use the Services.WalletService.GetWallets method
    public async Task<List<WalletDto>> GetWallets(string username)
    {
        return WalletService.GetWallets(username);
    }

    [HttpGet("{username}/{currency}")]
    public async Task<WalletDto> GetWallet(string username, CurrencyType currency)
    {
       return WalletService.GetWallet(username, currency);
    }
}