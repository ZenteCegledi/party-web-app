using PartyWebAppClient.Models;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppCommon.Requests;

namespace PartyWebAppClient.Services.UserService;

public interface IUserService
{
    Task<(List<UserDto>, AppErrorModel)> GetAllUsers(int Id);
    Task<(UserDto, AppErrorModel)> CreateUser(CreateUserRequest _req);
    Task<(UserDto, AppErrorModel)> EditUser(EditUserRequest _req);
    Task<(UserDto, AppErrorModel)> DeleteUser(int Id);
    
}