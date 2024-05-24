<<<<<<< Updated upstream
using Microsoft.AspNetCore.Mvc;
=======
using PartyWebAppCommon.Requests;
>>>>>>> Stashed changes
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Services
{
    public interface IUserService
    {
<<<<<<< Updated upstream
        Task<ActionResult<List<User>>> GetAllUsers();

        Task<ActionResult<User>> CreateUserRequest(string username, string name, DateTime birthDate, string email,
            string phone, string password, List<Wallet> wallets);

        Task<ActionResult<User>> GetUser(string username);
        Task<ActionResult<User>> DeleteUser(string username);

        Task<ActionResult<User>> EditUser(string username, string name, DateTime birthDate, string email,
            string phone, string password, List<Wallet> wallets);
=======
        Task<List<User?>> GetAllUsers();

        Task<User?> CreateUser(CreateUserRequest user);

        Task<User?> GetUser(string username);
        Task<User?> DeleteUser(string username);

        Task<User?> EditUser(string username, string? name, DateTime? birthDate, string? email, string? phone, string? password);
>>>>>>> Stashed changes
    }
}