using BitzArt.Blazor.Auth;
using Microsoft.IdentityModel.Tokens;
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
        string SecretKey = config.GetValue<string>("SecretKey") ?? throw new JwtException("SecretKey not found in appsettings.json");

        var options = new JwtOptions
        {
            PublicKey = "MIIBCgKCAQEA12zIJKpaIuNNk2yAdQ4e/EsT7al1hozyi/qFeTduf7BJFS4niFK7k9OL4VJFoUbpDt18y7Yqlz0nsEyinu/7wZJjf646yYymA8jBib/4kxQw6zH7C3qaam283k72pxb+aZOeJ6iU9KNkwTbfMHxKuTHoxySS6VH0vt3Sn0FYWryp8BVdPFlbuJp6K5otksTbdFOPgzgvwNreoI3TgA0e2clRKaEv+FGwhmY6WqR/hp/ebo0mflL2hPwJI1PLzjXdlx1sPHmYYfDTA02eJWkGYVti4oUZ9UTI5pZeRMNItSu1IyjHi45iLDQ+kRaPsx2bL/YZ7NXJu/g+dk7Lb4KdfQIDAQAB",
            PrivateKey = SecretKey,
            AccessTokenDuration = new TimeSpan(0, 1, 0),
            RefreshTokenDuration = new TimeSpan(0, 5, 0)
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

    public JwtPair BuildJwtPair()
    {
        var issuedAt = DateTime.UtcNow;
        var accessTokenExpiresAt = issuedAt + _accessTokenDuration;

        var accessToken = _tokenHandler.WriteToken(new JwtSecurityToken(
            claims: new[]
            {
                new Claim("tt", "a")
            },
            notBefore: issuedAt,
            expires: accessTokenExpiresAt,
            signingCredentials: _signingCredentials
        ));

        var refreshTokenExpiresAt = issuedAt + _refreshTokenDuration;

        var refreshToken = _tokenHandler.WriteToken(new JwtSecurityToken(
            claims: new[]
            {
                new Claim("tt", "r")
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
}

internal class JwtException(string errorMessage, Exception? innerException = null) : Exception(errorMessage, innerException);

internal class JwtOptions
{
    public required string PublicKey { get; set; }
    public required string PrivateKey { get; set; }
    public TimeSpan AccessTokenDuration { get; set; }
    public TimeSpan RefreshTokenDuration { get; set; }
}