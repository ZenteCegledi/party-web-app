using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using PartyWebAppServer.ErrorHandling.Exceptions;

namespace PartyWebAppServer.Services.UserService;

public class UserService(AppDbContext dbContext, IMapper mapper) : IUserService
{
    public async Task<List<UserDto>> GetAllUsers()
    {
        return mapper.Map<List<UserDto>>(dbContext.Users.ToListAsync());
    }

    public async Task<UserDto> CreateUser(CreateUserRequest user)
    {
        if (await dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email) != null)
        {
            throw new EmailAlreadyInUseException(user.Email);
        }

        if (await dbContext.Users.FirstOrDefaultAsync(u => u.Username == user.Username) != null)
        {
            throw new UserCreationException(user.Username);
        }

        var tmpUser = new User
        {
            Username = user.Username,
            Name = user.Name,
            BirthDate = user.BirthDate,
            Email = user.Email,
            Phone = user.Phone,
            Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
            Language = user.Language
        };
        dbContext.Users.Add(tmpUser);
        await dbContext.SaveChangesAsync();

        return mapper.Map<UserDto>(tmpUser);
    }


    public async Task<UserDto> GetUser(string username)
    {
        var user = await dbContext.Users.Where(u => u.Username == username)
            .Include(u => u.Wallets)
            .FirstOrDefaultAsync();

        if (user != null)
        {
            return mapper.Map<UserDto>(user);
        }

        throw new UserNotFoundException(username);
    }


    public async Task<UserDto> DeleteUser(string username)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null)
        {
            throw new UserNotFoundException(username);
        }
        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
        return mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> EditUser(string username, EditUserRequest userRequest)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) throw new UserNotFoundException(username);

        if (userRequest.Email != null)
        {
            var userWithMail = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == userRequest.Email);
            if (userWithMail != null) throw new EmailAlreadyInUseException(userRequest.Email);
            user.Email = userRequest.Email;
        }

        if (userRequest.BirthDate != null) user.BirthDate = (DateTime)userRequest.BirthDate;
        if (userRequest.Phone != null) user.Phone = userRequest.Phone;
        if (userRequest.Password != null) user.Password = userRequest.Password;
        if (userRequest.Language != null) user.Language = (LanguageType)userRequest.Language;
        await dbContext.SaveChangesAsync();

        return mapper.Map<UserDto>(user);
    }
}