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
        List<UserDTO?> users = new List<UserDTO?>();

        mapper.Map<List<UserDTO>>(dbContext.Users.ToListAsync());

        return users;
    }

    public async Task<UserDTO> CreateUser(CreateUserRequest user)
    {
        if (await dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email) != null) {
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

            return mapper.Map<UserDTO>(tmpUser);
        } catch (Exception e) {
            Console.WriteLine(e);
            throw new UserCreationException(user.Username);
        }
    }


    public async Task<UserDTO> GetUser(string username)

    {
        var user = await dbContext.Users.Where(u => u.Username == username).Include(u => u.Wallets).FirstOrDefaultAsync();
        
        if (user != null)
        {
            return mapper.Map<UserDTO?>(user);

        }

        throw new UserNotFoundException(username);
    }


    public async Task<UserDTO> DeleteUser(string username)

    {
        try {
            User? user = await dbContext.Users.Where(u => u.Username == username).FirstAsync();
            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return mapper.Map<UserDTO>(user);
        } catch {
            throw new UserNotFoundException(username);
        }
    }

    public async Task<UserDTO> EditUser(EditUserRequest userRequest)
    {
        string username = userRequest.Username;
        string? name = userRequest.Name;
        DateTime? birthDate = userRequest.BirthDate;
        string? email = userRequest.Email;
        string? phone = userRequest.Phone;
        string? password = userRequest.Password;

        try {
            User? user = await dbContext.Users.Where(u => u.Username == username).FirstAsync();
            if (name != null) user.Name = name;
            if (birthDate != null) user.BirthDate = (DateTime)birthDate;
            if (email != null) user.Email = email;
            if (phone != null) user.Phone = phone;
            if (password != null) user.Password = password;
            await dbContext.SaveChangesAsync();

            return mapper.Map<UserDTO>(user);
        } catch
        {
            throw new UserNotFoundException(username);
        }
    }
}