using BitzArt.Blazor.Auth;
using Microsoft.IdentityModel.Tokens;
using PartyWebAppServer.Database.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PartyWebAppServer.Services;

public class JwtService
{
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly SigningCredentials _signingCredentials;
    private readonly TimeSpan _accessTokenDuration;
    private readonly TimeSpan _refreshTokenDuration;

    private readonly IConfiguration config;

    public JwtService(IConfiguration configuration)
    {
        config = configuration ?? throw new ArgumentNullException(nameof(configuration));
        string SecretKey = config.GetValue<string>("SecretKey")!;
        string PublicKey = config.GetValue<string>("PublicKey")!;
        int AccessTokenDuration = config.GetValue<int>("AccessTokenDuration");
        int RefreshTokenDuration = config.GetValue<int>("RefreshTokenDuration");

        var options = new JwtOptions
        {
            PublicKey = PublicKey,
            PrivateKey = SecretKey,
            AccessTokenDuration = TimeSpan.FromMinutes(AccessTokenDuration),
            RefreshTokenDuration = TimeSpan.FromMinutes(RefreshTokenDuration)
        };

        _tokenHandler = new JwtSecurityTokenHandler();

        var privateRsa = RSA.Create();
        var privateKey = Convert.FromBase64String(options.PrivateKey!);
        privateRsa.ImportRSAPrivateKey(privateKey, out _);

        var privateSecurityKey = new RsaSecurityKey(privateRsa);

        _signingCredentials = new SigningCredentials(privateSecurityKey, SecurityAlgorithms.RsaSha256);

        _accessTokenDuration = options.AccessTokenDuration;
        _refreshTokenDuration = options.RefreshTokenDuration;
    }

    public JwtPair BuildJwtPair(User? user)
    {
        var issuedAt = DateTime.UtcNow;
        var accessTokenExpiresAt = issuedAt + _accessTokenDuration;

        if (user == null) throw new JwtException("User is null at JWTService.BuildJwtPair()");

        var accessToken = _tokenHandler.WriteToken(new JwtSecurityToken(
            claims: new[]
            {
                new Claim("tt", "a"),
                new Claim("Username", user.Username),
                new Claim("RoleID", user.RoleId.ToString()),
            },
            notBefore: issuedAt,
            expires: accessTokenExpiresAt,
            signingCredentials: _signingCredentials
        ));

        var refreshTokenExpiresAt = issuedAt + _refreshTokenDuration;

        var refreshToken = _tokenHandler.WriteToken(new JwtSecurityToken(
            claims: new[]
            {
                new Claim("tt", "r"),
                new Claim("Username", user.Username),
                new Claim("RoleID", user.RoleId.ToString()),
            },
            notBefore: issuedAt,
            expires: refreshTokenExpiresAt,
            signingCredentials: _signingCredentials
        ));

        return new JwtPair
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiresAt = accessTokenExpiresAt,
            RefreshTokenExpiresAt = refreshTokenExpiresAt
        };
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _signingCredentials.Key,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        SecurityToken securityToken;
        try
        {
            var principal = _tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.RsaSha256, StringComparison.InvariantCultureIgnoreCase)) throw new SecurityTokenException("Invalid token");

            return principal;
        }
        catch (SecurityTokenExpiredException)
        {
            throw new JwtException("Token has expired");
        }
    }

    public JwtPair RefreshJwtPair(string refreshToken)
    {
        try
        {
            var principal = GetPrincipalFromExpiredToken(refreshToken);

            if (principal.FindFirst("tt")?.Value != "r") throw new JwtException("Invalid token type");

            var user = new User
            {
                Username = principal.FindFirst("Username")?.Value!,
                RoleId = int.Parse(principal.FindFirst("RoleID")?.Value!),
            };

            return BuildJwtPair(user);
        }
        catch (JwtException ex)
        {
            Console.WriteLine(ex.Message);

            throw new JwtException("Invalid token", ex);
        }
    }
}

internal class JwtException(string errorMessage, Exception? innerException = null) : Exception(errorMessage, innerException);

internal class JwtOptions
{
    public required string PublicKey { get; set; }
    public required string PrivateKey { get; set; }
    public TimeSpan AccessTokenDuration { get; set; }
    public TimeSpan RefreshTokenDuration { get; set; }
}