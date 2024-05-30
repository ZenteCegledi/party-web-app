using PartyWebAppClient.Models;
using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;
using PartyWebAppCommon.Requests;

namespace PartyWebAppClient.Services.UserService;

public class UserService(IAppHttpClient http) : IUserService
{
    private IUserService _userServiceImplementation;

    public async Task<(List<UserDto>, AppErrorModel)> GetAllUsers(int Id) =>
        await http.GetAsync<List<UserDto>>($"http://localhost:5259/api/user/{Id}");
    
    public async Task<(UserDto, AppErrorModel)> CreateUser(CreateUserRequest _req) =>
        await http.PostAsync<UserDto>($"http://localhost:5259/api/newuser", _req);

    public async Task<(UserDto, AppErrorModel)> EditUser(EditUserRequest _req) =>
        await http.PutAsync<UserDto>($"http://localhost:5259/api/user", _req);
    public async Task<(UserDto, AppErrorModel)> DeleteUser(int Id) =>
        await http.DeleteAsync<UserDto>($"http://localhost:5259/api/user/{Id}");
}