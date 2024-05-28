using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.Database;
using PartyWebAppServer.Services;

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

    [HttpPost]
    public async Task<WalletDto> CreateWallet(WalletDto wallet)
    {
        return WalletService.CreateWallet(wallet);
    }

    [HttpDelete("{username}/{currency}")]
    public async Task<WalletDto> DeleteWallet(string username, CurrencyType currency)
    {
        var _wallet = WalletService.GetWallet(username, currency);

        return WalletService.DeleteWallet(username, currency);
    }

    [HttpPut("deposit/{username}/{currency}/{amount}")]
    public async Task<WalletDto> DepositToWallet(string username, CurrencyType currency, decimal amount)
    {
        var _wallet = WalletService.GetWallet(username, currency);

        return WalletService.DepositToWallet(_wallet, amount);
    }

    [HttpPut("withdraw/{username}/{currency}/{amount}")]
    public async Task<WalletDto> WithdrawFromWallet(string username, CurrencyType currency, decimal amount)
    {
        var _wallet = WalletService.GetWallet(username, currency);

        return WalletService.WithdrawFromWallet(_wallet, amount);
    }

    [HttpPut("primary/{username}/{currency}")]
    public async Task<WalletDto> SetPrimaryWallet(string username, CurrencyType currency)
    {
        var _wallet = WalletService.GetWallet(username, currency);

        return WalletService.SetPrimaryWallet(_wallet);
    }
}