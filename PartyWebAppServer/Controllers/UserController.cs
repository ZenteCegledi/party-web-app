using AutoMapper;
using PartyWebAppServer.Database.Models;
using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Services;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/user/")]
public class UserController(IUserService userService)
{
    [HttpGet]
    public async Task<List<UserDTO>> GetAllUsers()
    {
        return await userService.GetAllUsers();
    }

    [HttpPost]
    public async Task<UserDTO> CreateUser(CreateUserRequest userRequest)
    {
        return await userService.CreateUser(userRequest);
    }

    [HttpGet("/{username}")]
    public async Task<UserDTO> GetUser(string username)
    {
        return await userService.GetUser(username);
    }

    [HttpDelete("/{username}")]
    public async Task<UserDTO> DeleteUser(string username)
    {
        return await userService.DeleteUser(username);
    }

    [HttpPut("/{username}")]
    public async Task<UserDTO> EditUser(EditUserRequest userRequest)
    {
        return await userService.EditUser(userRequest);

    }
}