namespace PartyWebAppServer.Services;

using PartyWebAppServer;
using PartyWebAppServer.Services;
using BitzArt.Blazor.Auth;
using PartyWebAppCommon.Models;

public class SignUpModel { }

public class ServerSideAuthenticationService(JwtService jwtService)
    : ServerSideAuthenticationService<SignInModel, SignUpModel>()
{
    protected override Task<AuthenticationResult> GetSignInResultAsync(SignInModel SignInModel)
    {
        Console.WriteLine("asdsaddas");

        var authResult = AuthenticationResult.Success(jwtService.BuildJwtPair());

        return Task.FromResult(authResult);
    }

    public override Task<AuthenticationResult> RefreshJwtPairAsync(string refreshToken)
    {
        var authResult = AuthenticationResult.Success(jwtService.BuildJwtPair());

        return Task.FromResult(authResult);
    }
}