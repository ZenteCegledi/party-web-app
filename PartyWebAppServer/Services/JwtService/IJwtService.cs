using System.Security.Claims;
using BitzArt.Blazor.Auth;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Services.JwtService;

public interface IJwtService
{
    public JwtPair BuildJwtPair(User? user);
    public JwtPair RefreshJwtPair(string refreshToken);
    public ClaimsPrincipal? GetPrincipalFromToken(string token);

    public string? GetTokenFromRequest(HttpRequest request);

    public bool IsAuthorized(HttpRequest request);
    public bool IsUserAdmin(HttpRequest request);
    public bool IsUserTheUser(HttpRequest request, string username);
}