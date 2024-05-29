using Microsoft.AspNetCore.Mvc;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;
using PartyWebAppServer.Services.JwtService;
using PartyWebAppServer.Services.UserService;

namespace PartyWebAppServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IServerUserService userService, IHttpContextAccessor httpContextAccessor, IJwtService jwtService)
{
    private readonly HttpContext httpContext = httpContextAccessor.HttpContext;

    [HttpGet]
    public async Task<List<UserDTO>> GetAllUsers()
    {
        if (!jwtService.IsUserAdmin(httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin to access every user.");

        return await userService.GetAllUsers();
    }

    [HttpPost]
    public async Task<UserDTO> CreateUser(CreateUserRequest userRequest)
    {
        if (!jwtService.IsUserAdmin(httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin to create a user.");

        return await userService.CreateUser(userRequest);
    }

    [HttpGet("{username}")]
    public async Task<UserDTO> GetUser(string username)
    {
        if (!jwtService.IsUserTheUser(httpContext.Request, username) && !jwtService.IsUserAdmin(httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin or the user to access this user.");

        return await userService.GetUser(username);
    }

    [HttpDelete("{username}")]
    public async Task<UserDTO> DeleteUser(string username)
    {
        if (!jwtService.IsUserAdmin(httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin to delete this user.");

        return await userService.DeleteUser(username);
    }

    [HttpPut("{username}")]
    public async Task<UserDTO> EditUser(string username, EditUserRequest userRequest)
    {
        if (!jwtService.IsUserTheUser(httpContext.Request, username) && !jwtService.IsUserAdmin(httpContext.Request))
            throw new UnauthorizedAccessException("You need to be an admin or the user to edit this user.");

        return await userService.EditUser(username, userRequest);

    }
}