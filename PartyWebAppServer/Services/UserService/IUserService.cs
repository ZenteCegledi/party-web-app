using PartyWebAppCommon.Requests;

namespace PartyWebAppServer.Services.UserService
{
    public interface IUserService
    {

        Task<List<UserDTO>> GetAllUsers();

        Task<UserDTO> CreateUser(CreateUserRequest user);

        Task<UserDTO> GetUser(string username);
        Task<UserDTO> DeleteUser(string username);

        Task<UserDTO> EditUser(string username, EditUserRequest userRequest);
    }
}

