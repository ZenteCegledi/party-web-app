using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.enums;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletController
{
    public WalletController(AppDbContext context)
    {
        DbContext = context;
    }
    private AppDbContext DbContext { get; set; }


    [HttpGet("{username}")]
    public async Task<List<Wallet>> GetWallets(string username)
    {
        return await DbContext.Wallets.Where(w => w.Username == username).ToListAsync();
    }

    [HttpGet("{username}/{currency}")]
    public async Task<Wallet> GetWallet(string username, CurrencyType currency)
    {
        var wallet = await DbContext.Wallets.FirstOrDefaultAsync(w => w.Username == username && w.Currency == currency);
        if (wallet == null)
        {
            throw new Exception("Wallet not found");
        }
        return wallet;
    }
}