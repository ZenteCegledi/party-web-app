using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Requests;

namespace PartyWebAppServer.Services.UserService;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsers();
    Task<UserDto> CreateUser(CreateUserRequest user);
    Task<UserDto> GetUser(string username);
    Task<UserDto> DeleteUser(string username);
    Task<UserDto> EditUser(string username, EditUserRequest userRequest);
}

