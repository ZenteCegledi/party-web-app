using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Services.JwtService;
using PartyWebAppServer.Services.UserService;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService, IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    [HttpGet]
    public async Task<List<UserDto>> GetAllUsers()
    {
        if (!jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin to access every user.");

        return await userService.GetAllUsers();
    }

    [HttpPost]
    public async Task<UserDto> CreateUser(CreateUserRequest userRequest)
    {
        if (!jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin to create a user.");

        return await userService.CreateUser(userRequest);
    }

    [HttpGet("{username}")]
    public async Task<UserDto> GetUser(string username)
    {
        if (!jwtService.IsUserTheUser(_httpContext.Request, username) && !jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin or the user to access this user.");

        return await userService.GetUser(username);
    }

    [HttpDelete("{username}")]
    public async Task<UserDto> DeleteUser(string username)
    {
        if (!jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin to delete this user.");

        return await userService.DeleteUser(username);
    }

    [HttpPut("{username}")]
    public async Task<UserDto> EditUser(string username, EditUserRequest userRequest)
    {
        if (!jwtService.IsUserTheUser(_httpContext.Request, username) && !jwtService.IsUserAdmin(_httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin or the user to edit this user.");

        return await userService.EditUser(username, userRequest);

    }
}