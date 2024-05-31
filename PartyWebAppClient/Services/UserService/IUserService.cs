using PartyWebAppClient.Models;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppClient.Services.UserService;

public interface IUserService {
	Task<(UserDto?, AppErrorModel?)> GetUser(string username);
}