
using PartyWebAppCommon.DTOs;

using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Services
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

