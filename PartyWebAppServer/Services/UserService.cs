using AutoMapper;
using PartyWebAppServer.Database;
using PartyWebAppServer.Database.Models;
using Microsoft.EntityFrameworkCore;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.ErrorHandling.Exceptions;

namespace PartyWebAppServer.Services;

public class UserService(AppDbContext dbContext, IMapper mapper) : IUserService
{
    public async Task<List<UserDTO>> GetAllUsers()
    {
        List<UserDTO> users = new List<UserDTO>();

        mapper.Map<List<UserDTO>>(dbContext.Users.ToListAsync());

        return users;
    }

    public async Task<UserDTO> CreateUser(CreateUserRequest user)
    {
        if (await dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email) != null)
        {
            throw new EmailAlreadyInUseException(user.Email);
        }

        if (await dbContext.Users.FirstOrDefaultAsync(u => u.Username == user.Username) != null)
        {
            throw new UserCreationException(user.Username);
        }


        User? tmpUser = new User
        {
            Username = user.Username,
            Name = user.Name,
            BirthDate = user.BirthDate,
            Email = user.Email,
            Phone = user.Phone,
            Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
        };
        dbContext.Users.Add(tmpUser);
        await dbContext.SaveChangesAsync();

        return mapper.Map<UserDTO>(tmpUser);


    }


    public async Task<UserDTO> GetUser(string username)

    {
        var user = await dbContext.Users.Where(u => u.Username == username).Include(u => u.Wallets)
            .FirstOrDefaultAsync();

        if (user != null)
        {
            return mapper.Map<UserDTO?>(user);
        }

        throw new UserNotFoundException(username);
    }


    public async Task<UserDTO> DeleteUser(string username)

    {
        User? user = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null)
        {
            throw new UserNotFoundException(username);
        }
        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
        return mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO> EditUser(string username, EditUserRequest userRequest)
    {


        User? user = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null)
        {
            throw new UserNotFoundException(username);
        }
        if (userRequest.BirthDate != null) user.BirthDate = (DateTime)userRequest.BirthDate;
        var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == userRequest.Email);
        if (existingUser == null)
        {
            user.Email = userRequest.Email;
        }
        else
        {
            throw new EmailAlreadyInUseException(userRequest.Email);
        }

        if (userRequest.Phone != null) user.Phone = userRequest.Phone;
        if (userRequest.Password != null) user.Password = userRequest.Password;
        await dbContext.SaveChangesAsync();

        return mapper.Map<UserDTO>(user);
    }
}