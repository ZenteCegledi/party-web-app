namespace PartyWebAppServer.Services;

using BitzArt.Blazor.Auth;
using PartyWebAppCommon.Models;
using BCrypt.Net;
using PartyWebAppServer.Database.Models;
using Microsoft.Extensions.Configuration;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;

public class ServerSideAuthenticationService : ServerSideAuthenticationService<SignInModel, SignUpModel>
{
    // private static readonly IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    private readonly JwtService jwtService;
    private readonly AppDbContext dbContext;
    private readonly IConfiguration config;

    public ServerSideAuthenticationService(JwtService jwtService, AppDbContext dbContext, IConfiguration config)
    {
        this.jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        this.config = config ?? throw new ArgumentNullException(nameof(config));
    }

    protected override Task<AuthenticationResult> GetSignInResultAsync(SignInModel SignInModel)
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Email == SignInModel.Email);

        if (user == null) return Task.FromResult(AuthenticationResult.Failure("User not found"));

        if (!BCrypt.Verify(SignInModel.Password, user.Password)) return Task.FromResult(AuthenticationResult.Failure("Invalid password"));

        var authResult = AuthenticationResult.Success(jwtService.BuildJwtPair());

        return Task.FromResult(authResult);
    }

    protected override Task<AuthenticationResult> GetSignUpResultAsync(SignUpModel SignUpModel)
    {
        var user = new User
        {
            Email = SignUpModel.Email,
            Password = BCrypt.HashPassword(SignUpModel.Password),
            Username = SignUpModel.Username,
            RoleId = SignUpModel.RoleId,
            Name = SignUpModel.Username,
            Phone = SignUpModel.Phone,
            BirthDate = SignUpModel.BirhtDate.ToUniversalTime(),
            PasswordUpdated = DateTime.Now.ToUniversalTime(),
        };

        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        var authResult = AuthenticationResult.Success(jwtService.BuildJwtPair());

        return Task.FromResult(authResult);
    }

    public override Task<AuthenticationResult> RefreshJwtPairAsync(string refreshToken)
    {
        var authResult = AuthenticationResult.Success(jwtService.BuildJwtPair());

        return Task.FromResult(authResult);
    }
}