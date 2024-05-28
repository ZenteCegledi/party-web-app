using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.Database;
using PartyWebAppServer.Services;
using PartyWebAppServer.Services.WalletService;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletController
{
    public WalletController(AppDbContext context)
    {
        WalletService = new ServerWalletService(context);
    }
    private ServerWalletService WalletService { get; set; }

    [HttpGet("{username}")]
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