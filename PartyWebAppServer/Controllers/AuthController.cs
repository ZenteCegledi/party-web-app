using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [HttpGet("me")]
    public IActionResult GetMe()
    {
        if (HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            var username = HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            var role = HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            var user = DbContext.Users.Where(u => u.Username == username).FirstOrDefault();
            
            return new JsonResult(new { user.Username, user.Name, user.BirthDate, user.Email, user.Phone, role });
        }
        
        return new JsonResult(new { user = "not authenticated"});
    }
    
    [HttpPost("login")]
    public async Task<ClaimsIdentity> Login(SignInData data)
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
                
                return identity;
            }

            return new ClaimsIdentity();
    }
}

public class SignInData
{
    public string Username { get; set; }
    public string Password { get; set; }
}