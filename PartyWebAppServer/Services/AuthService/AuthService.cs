namespace PartyWebAppServer.Services;

using BitzArt.Blazor.Auth;
using BCrypt.Net;
using PartyWebAppServer.Database.Models;
using PartyWebAppServer.Database;
using PartyWebAppServer.Services.JwtService;
using PartyWebAppCommon.Requests;

public class AuthService : ServerSideAuthenticationService<SignInRequest, SignUpRequest>, IAuthService
{
    private readonly IJwtService jwtService;
    private readonly AppDbContext dbContext;

    public AuthService(IJwtService jwtService, AppDbContext dbContext)
    {
        this.jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    protected override Task<AuthenticationResult> GetSignInResultAsync(SignInRequest signInRequest)
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Email == signInRequest.Email);

        if (user == null) return Task.FromResult(AuthenticationResult.Failure("User not found"));

        if (!BCrypt.Verify(signInRequest.Password, user.Password)) return Task.FromResult(AuthenticationResult.Failure("Invalid password"));

        var authResult = AuthenticationResult.Success(jwtService.BuildJwtPair(user));

        return Task.FromResult(authResult);
    }

    protected override Task<AuthenticationResult> GetSignUpResultAsync(SignUpRequest signUpRequest)
    {
        if (dbContext.Users.Any(u => u.Email == signUpRequest.Email)) return Task.FromResult(AuthenticationResult.Failure("User already exists"));

        var user = new User
        {
            Email = signUpRequest.Email,
            Password = BCrypt.HashPassword(signUpRequest.Password),
            Username = signUpRequest.Username,
            RoleId = signUpRequest.RoleId,
            Name = signUpRequest.Username,
            Phone = signUpRequest.Phone,
            BirthDate = signUpRequest.BirhtDate.ToUniversalTime(),
            PasswordUpdated = DateTime.Now.ToUniversalTime(),
        };

        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        var authResult = AuthenticationResult.Success(jwtService.BuildJwtPair(user));

        return Task.FromResult(authResult);
    }

    public override Task<AuthenticationResult> RefreshJwtPairAsync(string refreshToken)
    {
        var authResult = AuthenticationResult.Success(jwtService.RefreshJwtPair(refreshToken));

        return Task.FromResult(authResult);
    }
}