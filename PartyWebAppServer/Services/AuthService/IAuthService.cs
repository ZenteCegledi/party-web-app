using BitzArt.Blazor.Auth;
using PartyWebAppCommon.Requests;

public interface IAuthService
{
    public Task<AuthenticationResult> SignInAsync(SignInRequest signInRequest);
    public Task<AuthenticationResult> SignUpAsync(SignUpRequest signUpRequest);
    public Task<AuthenticationResult> RefreshJwtPairAsync(string refreshToken);
}