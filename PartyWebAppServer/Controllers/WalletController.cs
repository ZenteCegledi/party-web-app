using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppServer.Database;
using PartyWebAppServer.Services.JwtService;
using PartyWebAppServer.Services.WalletService;
using PartyWebAppCommon.Requests;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletController(IWalletService walletService, AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    [HttpGet("{username}")]
    public async Task<List<WalletDto>> GetWallets(GetWalletRequests _req)
    {
        if (!jwtService.IsUserTheUser(_httpContext.Request, _req.Username) && !jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You can only access your own wallets.");

        return await walletService.GetWallets(_req);
    }

    [HttpGet("{username}/{currency}")]
    public async Task<WalletDto> GetWallet(GetWalletRequest _req)
    {
        if (!jwtService.IsUserTheUser(_httpContext.Request, _req.Username) && !jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You can only access your own wallets.");

        return await walletService.GetWallet(_req);
    }

    [HttpPost]
    public async Task<WalletDto> CreateWallet(CreateWalletRequest _req)
    {
        if (!jwtService.IsAuthorized(_httpContext.Request))
            throw new UnauthorizedAccessException("You need to be authorized.");

        return await walletService.CreateWallet(_req);
    }

    [HttpDelete("{username}/{currency}")]
    public async Task<WalletDto> DeleteWallet(DeleteWalletRequest _req)
    {
        if (!jwtService.IsUserTheUser(_httpContext.Request, _req.Username) && !jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You can only delete your own wallets.");

        return await walletService.DeleteWallet(_req);
    }

    [HttpPut("deposit/{username}/{currency}/{amount}")]
    public async Task<WalletDto> DepositToWallet(DepositToWalletRequest _req)
    {
        if (!jwtService.IsUserTheUser(_httpContext.Request, _req.Username) && !jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You can only deposit to your own wallets.");

        return await walletService.DepositToWallet(_req);
    }

    [HttpPut("withdraw/{username}/{currency}/{amount}")]
    public async Task<WalletDto> WithdrawFromWallet(WithdrawFromWalletRequest _req)
    {
        if (!jwtService.IsUserTheUser(_httpContext.Request, _req.Username) && !jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You can only withdraw from your own wallets.");

        return await walletService.WithdrawFromWallet(_req);
    }

    [HttpPut("primary/{username}/{currency}")]
    public async Task<WalletDto> SetPrimaryWallet(SetPrimaryWalletRequest _req)
    {
        if (!jwtService.IsUserTheUser(_httpContext.Request, _req.Username) && !jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You can only set your own primary wallets.");

        return await walletService.SetPrimaryWallet(_req);
    }
}