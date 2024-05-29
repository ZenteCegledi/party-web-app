using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.Database;
using PartyWebAppServer.Services.JwtService;
using PartyWebAppServer.Services.WalletService;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletController(IWalletService walletService, AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    [HttpGet("{username}")]
    public async Task<List<WalletDto>> GetWallets(string username)
    {
        if (!jwtService.IsUserTheUser(_httpContext.Request, username) && !jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You can only access your own wallets.");

        return await walletService.GetWallets(username);
    }

    [HttpGet("{username}/{currency}")]
    public async Task<WalletDto> GetWallet(string username, CurrencyType currency)
    {
        if (!jwtService.IsUserTheUser(_httpContext.Request, username) && !jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You can only access your own wallets.");

        return await walletService.GetWallet(username, currency);
    }

    [HttpPost]
    public async Task<WalletDto> CreateWallet(WalletDto wallet)
    {
        if (!jwtService.IsAuthorized(_httpContext.Request))
            throw new UnauthorizedAccessException("You need to be authorized.");

        return await walletService.CreateWallet(wallet);
    }

    [HttpDelete("{username}/{currency}")]
    public async Task<WalletDto> DeleteWallet(string username, CurrencyType currency)
    {
        if (!jwtService.IsUserTheUser(_httpContext.Request, username) && !jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You can only delete your own wallets.");

        return await walletService.DeleteWallet(username, currency);
    }

    [HttpPut("deposit/{username}/{currency}/{amount}")]
    public async Task<WalletDto> DepositToWallet(string username, CurrencyType currency, decimal amount)
    {
        if (!jwtService.IsUserTheUser(_httpContext.Request, username) && !jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You can only deposit to your own wallets.");

        return await walletService.DepositToWallet(_wallet, amount);
    }

    [HttpPut("withdraw/{username}/{currency}/{amount}")]
    public async Task<WalletDto> WithdrawFromWallet(string username, CurrencyType currency, decimal amount)
    {
        if (!jwtService.IsUserTheUser(_httpContext.Request, username) && !jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You can only withdraw from your own wallets.");

        return await walletService.WithdrawFromWallet(_wallet, amount);
    }

    [HttpPut("primary/{username}/{currency}")]
    public async Task<WalletDto> SetPrimaryWallet(string username, CurrencyType currency)
    {
        if (!jwtService.IsUserTheUser(_httpContext.Request, username) && !jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You can only set your own primary wallets.");

        return await walletService.SetPrimaryWallet(_wallet);
    }
}