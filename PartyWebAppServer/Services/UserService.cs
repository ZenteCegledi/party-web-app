using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.ErrorHandling.Exceptions;

namespace PartyWebAppServer.Services;

public class UserService(AppDbContext dbContext) : PartyWebAppServer.Services.IUserService
{
    public async Task<List<User?>> GetAllUsers()
    {
        return await dbContext.Users.ToListAsync();
    }

    public async Task<User?> CreateUser(CreateUserRequest user)
    {
        if (await dbContext.Users.AnyAsync(u => u.Email == user.Email)) {
            throw new EmailAlreadyInUseException(user.Email);
        }
        try {
            User? tmpUser = new User
            {
                Username = user.Username,
                Name = user.Name,
                BirthDate = user.BirthDate,
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password
            };
            dbContext.Users.Add(tmpUser);
            await dbContext.SaveChangesAsync();
            return tmpUser;
        } catch {
            throw new UserCreationException(user.Username);
        }
    }

    public async Task<User?> GetUser(string username)
    {
        var user = await dbContext.Users.Where(u => u.Username == username).Include(u => u.Wallets).FirstOrDefaultAsync();
        
        if (user != null)
        {
            return user;
        }

        throw new UserNotFoundException(username);
    }

    public async Task<User?> DeleteUser(string username)
    {
        try {
            User? user = await dbContext.Users.Where(u => u.Username == username).FirstAsync();
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return user;
        } catch {
            throw new NotImplementedException("User not found");
        }
    }

    public async Task<User?> EditUser(string username, string? name, DateTime? birthDate, string? email, string? phone, string? password)
    {
        try {
            User? user = await dbContext.Users.Where(u => u.Username == username).FirstAsync();
            if (name != null) user.Name = name;
            if (birthDate != null) user.BirthDate = (DateTime)birthDate;
            if (email != null) user.Email = email;
            if (phone != null) user.Phone = phone;
            if (password != null) user.Password = password;
            await dbContext.SaveChangesAsync();
            return user;
        } catch {
            throw new NotImplementedException("User not found");
        }
    }
}