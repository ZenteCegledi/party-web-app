using Microsoft.AspNetCore.Mvc;
using PartyWebAppServer.Database.Models;

namespace PartyWebAppServer.Services
{
    public interface IUserService
    {
        Task<ActionResult<List<User>>> GetAllUsers();

        Task<ActionResult<User>> CreateUserRequest(string username, string name, DateTime birthDate, string email,
            string phone, string password, List<Wallet> wallets);

        Task<ActionResult<User>> GetUser(string username);
        Task<ActionResult<User>> DeleteUser(string username);

        Task<ActionResult<User>> EditUser(string username, string name, DateTime birthDate, string email,
            string phone, string password, List<Wallet> wallets);
    }
}