using AutoMapper;
using PartyWebAppServer.Database.Models;
using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Services;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/")]
public class UserController(IUserService userService, IMapper mapper)
{
    [HttpGet]
    public Task<List<User>> GetAllUsers()
    {
        var user = userService.GetAllUsers();
        return mapper.Map<Task<List<User>>>(user);
    }

    [HttpPost]
    public Task<User> CreateUser(string username, string name, DateTime birthDate, string email, string phone, string password, List<Wallet> wallets)
    {
        CreateUserRequest userRequest = new CreateUserRequest(
            username,
            name,
            birthDate,
            email,
            phone,
            password
        );
        
        var user = userService.CreateUser(userRequest);
        return mapper.Map<Task<User>>(user);
    }

    [HttpGet("/{username}")]
    public async Task<User> GetUser(string username)
    {
        var user = userService.GetUser(username);
        return await mapper.Map<Task<User>>(user);
    }

    [HttpDelete("/{username}")]
    public async Task<User> DeleteUser(string username)
    {
        var user = userService.DeleteUser(username);
        return await mapper.Map<Task<User>>(user);
    }

    [HttpPut("/{username}")]
    public async Task<User> EditUser(string username, string? name, DateTime? birthDate, string? email, string? phone, string? password)
    {
        var user = userService.EditUser(username, name, birthDate, email, phone, password);
        return await mapper.Map<Task<User>>(user);
    }
}