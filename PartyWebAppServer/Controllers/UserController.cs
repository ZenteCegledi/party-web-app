using PartyWebAppServer.Database.Models;
using Microsoft.AspNetCore.Mvc;
using PartyWebAppServer.Services;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/")]
public class UserController
{   
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public Task<ActionResult<List<User>>> GetAllUsers()
    {
        return _userService.GetAllUsers();
    }

    [HttpPost]
    public Task<ActionResult<User>> CreateUserRequest(string username, string name, DateTime birthDate, string email, string phone, string password, List<Wallet> wallets)
    {
        return _userService.CreateUserRequest(username, name, birthDate, email, phone, password, wallets);
    }

    [HttpGet("/username/{username}")]
    public async Task<ActionResult<User>> GetUser(string username)
    {
        return await _userService.GetUser(username);
    }

    [HttpDelete("/username/{username}")]
    public async Task<ActionResult<User>> DeleteUser(string username)
    {
        return await _userService.DeleteUser(username);
    }

    [HttpPut("/username/{username}")]
    public async Task<ActionResult<User>> EditUser(string username, string name, DateTime birthDate, string email, string phone, string password, List<Wallet> wallets)
    {
        return await _userService.EditUser(username, name, birthDate, email, phone, password, wallets);
    }
}