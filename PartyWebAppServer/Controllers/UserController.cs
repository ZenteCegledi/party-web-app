using AutoMapper;
using PartyWebAppServer.Database.Models;
using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Services;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/")]
public class UserController(IUserService userService)
{
    [HttpGet]
    public async Task<List<UserDTO?>> GetAllUsers()
    {
        return await userService.GetAllUsers();
    }

    [HttpPost]
    public async Task<UserDTO?> CreateUser(string username, string name, DateTime birthDate, string email, string phone, string password)
    {
        CreateUserRequest userRequest = new CreateUserRequest(
            username,
            name,
            birthDate,
            email,
            phone,
            password
        );
        
        return await userService.CreateUser(userRequest);
    }

    [HttpGet("/{username}")]
    public async Task<UserDTO?> GetUser(string username)
    {
        return await userService.GetUser(username);
    }

    [HttpDelete("/{username}")]
    public async Task<UserDTO?> DeleteUser(string username)
    {
        return await userService.DeleteUser(username);
    }

    [HttpPut("/{username}")]
    public async Task<UserDTO?> EditUser(string username, string? name, DateTime? birthDate, string? email, string? phone, string? password)
    {
        return await userService.EditUser(username, name, birthDate, email, phone, password);
    }
}