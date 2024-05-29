using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.Database;
using PartyWebAppServer.Services.JwtService;
using PartyWebAppServer.Services.WalletService;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletController(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
{
    private readonly ServerWalletService WalletService = new ServerWalletService(dbContext);
    private readonly HttpContext httpContext = httpContextAccessor.HttpContext;

    [HttpGet("{username}")]
    public async Task<List<WalletDTO>> GetWallets(string username)
    {
        if (!jwtService.IsUserTheUser(httpContext.Request, username) && !jwtService.IsUserAdmin(httpContext.Request))
            throw new UnauthorizedAccessException("You can only access your own wallets.");

        return WalletService.GetWallets(username);
    }

    [HttpGet("{username}/{currency}")]
    public async Task<WalletDTO> GetWallet(string username, CurrencyType currency)
    {
        if (!jwtService.IsUserTheUser(httpContext.Request, username) && !jwtService.IsUserAdmin(httpContext.Request))
            throw new UnauthorizedAccessException("You can only access your own wallets.");

        return WalletService.GetWallet(username, currency);
    }

    [HttpPost]
    public async Task<WalletDTO> CreateWallet(WalletDTO wallet)
    {
        if (!jwtService.IsAuthorized(httpContext.Request))
            throw new UnauthorizedAccessException("You need to be authorized.");

        return WalletService.CreateWallet(wallet);
    }

    [HttpDelete("{username}/{currency}")]
    public async Task<WalletDTO> DeleteWallet(string username, CurrencyType currency)
    {
        if (!jwtService.IsUserTheUser(httpContext.Request, username) && !jwtService.IsUserAdmin(httpContext.Request))
            throw new UnauthorizedAccessException("You can only delete your own wallets.");

        var _wallet = WalletService.GetWallet(username, currency);

        return WalletService.DeleteWallet(username, currency);
    }

    [HttpPut("deposit/{username}/{currency}/{amount}")]
    public async Task<WalletDTO> DepositToWallet(string username, CurrencyType currency, decimal amount)
    {
        if (!jwtService.IsUserTheUser(httpContext.Request, username) && !jwtService.IsUserAdmin(httpContext.Request))
            throw new UnauthorizedAccessException("You can only deposit to your own wallets.");

        var _wallet = WalletService.GetWallet(username, currency);

        return WalletService.DepositToWallet(_wallet, amount);
    }

    [HttpPut("withdraw/{username}/{currency}/{amount}")]
    public async Task<WalletDTO> WithdrawFromWallet(string username, CurrencyType currency, decimal amount)
    {
        if (!jwtService.IsUserTheUser(httpContext.Request, username) && !jwtService.IsUserAdmin(httpContext.Request))
            throw new UnauthorizedAccessException("You can only withdraw from your own wallets.");

        var _wallet = WalletService.GetWallet(username, currency);

        return WalletService.WithdrawFromWallet(_wallet, amount);
    }

    [HttpPut("primary/{username}/{currency}")]
    public async Task<WalletDTO> SetPrimaryWallet(string username, CurrencyType currency)
    {
        if (!jwtService.IsUserTheUser(httpContext.Request, username) && !jwtService.IsUserAdmin(httpContext.Request))
            throw new UnauthorizedAccessException("You can only set your own primary wallets.");

        var _wallet = WalletService.GetWallet(username, currency);

        return WalletService.SetPrimaryWallet(_wallet);
    }
}