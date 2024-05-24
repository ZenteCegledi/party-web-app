using PartyWebAppCommon.Requests;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Services
{
    public interface IUserService
    {
        Task<List<User?>> GetAllUsers();

        Task<User?> CreateUser(CreateUserRequest user);

        Task<User?> GetUser(string username);
        Task<User?> DeleteUser(string username);

        Task<User?> EditUser(string username, string? name, DateTime? birthDate, string? email, string? phone, string? password);
    }
}
