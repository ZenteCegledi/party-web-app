using PartyWebAppClient.Models;
using PartyWebAppCommon.DTOs;

namespace PartyWebAppClient.Services.UserService;

public class UserService(IAppHttpClient http): IUserService {
	public async Task<(UserDto?, AppErrorModel?)> GetUser(string username) =>
		await http.GetAsync<UserDto>($"http://localhost:5259/api/user/{username}");
}