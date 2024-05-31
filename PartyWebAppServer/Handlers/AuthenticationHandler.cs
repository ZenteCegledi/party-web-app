using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using PartyWebAppServer.Services.JwtService;

public class UserAuthenticationHandler : AuthenticationHandler<UserAuthenticationOptions>
    {
        public const string Schema = "Basic";
        private const string HeaderAuthorization = "Authorization";
        private readonly IJwtService JwtService;
        
        public UserAuthenticationHandler(
            IOptionsMonitor<UserAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IJwtService jwtService)
            : base(options, logger, encoder, clock)
        {
            JwtService = jwtService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {
                string? token = JwtService.GetTokenFromRequest(Request);

            if (string.IsNullOrEmpty(token)) return AuthenticateResult.NoResult();

            try
            {
                return ValidateToken(token);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
        }

        protected AuthenticateResult ValidateToken(string token) {
            var principal  = JwtService.GetPrincipalFromToken(token);
            var ticket    = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
            
        }
    }

    public class UserAuthenticationOptions : AuthenticationSchemeOptions
    {
    }