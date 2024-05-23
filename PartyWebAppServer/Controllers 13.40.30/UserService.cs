using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("/")]
public class UserService
{
    private AppDbContext DbContext { get; set; }

    public UserService(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }

    [HttpGet("api/")]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        return await DbContext.Users.ToListAsync();
    }
    
    [HttpPost("api/")]
    public async Task<ActionResult<User>> CreateUserRequest(string username, string name, DateTime birthDate, string email, string phone, string password, List<Wallet> wallets)
    {
        User user = new User
        {
            Username = username,
            Name = name,
            BirthDate = birthDate,
            Email = email,
            Phone = phone,
            Password = password,
            Wallets = wallets
        };

        DbContext.Users.Add(user);
        await DbContext.SaveChangesAsync();
        return user;
    }

    [HttpGet("api/username/{username}")]
    public async Task<ActionResult<User>> GetUser(string username)
    {
        try {
            return await DbContext.Users.Where(u => u.Username == username).Include(u => u.Wallets).FirstAsync();
        } catch {
            throw new NotImplementedException("User not found");
        }
    }

    [HttpDelete("api/username/{username}")]
    public async Task<ActionResult<User>> DeleteUser(string username)
    {
        try {
            User user = await DbContext.Users.Where(u => u.Username == username).FirstAsync();
            DbContext.Users.Remove(user);
            await DbContext.SaveChangesAsync();
            return user;
        } catch {
            throw new NotImplementedException("User not found");
        }
    }

    [HttpPut("api/username/{username}")]
    public async Task<ActionResult<User>> EditUser(string username, string name, DateTime birthDate, string email, string phone, string password, List<Wallet> wallets)
    {
        try {
            User user = await DbContext.Users.Where(u => u.Username == username).FirstAsync();
            user.Name = name;
            user.BirthDate = birthDate;
            user.Email = email;
            user.Phone = phone;
            user.Password = password;
            user.Wallets = wallets;
            await DbContext.SaveChangesAsync();
            return user;
        } catch {
            throw new NotImplementedException("User not found");
        }
    }
}