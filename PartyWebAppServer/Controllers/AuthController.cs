using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyWebAppServer.Database;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController
{
    
    public AuthController(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        DbContext = dbContext;
        HttpContextAccessor = httpContextAccessor;
        
    }
    
    private AppDbContext DbContext { get; set; }
    private IHttpContextAccessor HttpContextAccessor { get; set; }
    // [HttpGet("me")]
    // public IActionResult GetMe()
    // {
    //     return Ok();
    // }
    
    // Login (/login): léptessen be egy usert a paraméterül kapott username és password segítségével. Ha nincs ilyen user, vagy nem jó a jelszó, dobjon hibát.
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(SignInData data)
    {
            if (await DbContext.Users.AnyAsync(u => u.Username == data.Username && u.Password == data.Password))
            {
                var user = await DbContext.Users.Where(u => u.Username == data.Username).FirstOrDefaultAsync();
                var role = await DbContext.Roles.Where(r => r.Id == user.RoleId).Select(r => r.Name).FirstOrDefaultAsync();
                var cookieAndAuthTokenExpiration = DateTimeOffset.UtcNow.AddSeconds(10);
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, data.Username));
                identity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Expiration, cookieAndAuthTokenExpiration.ToString()));
                
                await HttpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
                {
                    ExpiresUtc = cookieAndAuthTokenExpiration,
                     IsPersistent = true,
                });

                return new StatusCodeResult(200);


            }
            // what should i do here?
            // answer: return Unauthorized();
            return new StatusCodeResult(401);
    }
}

public class SignInData
{
    public string Username { get; set; }
    public string Password { get; set; }
}