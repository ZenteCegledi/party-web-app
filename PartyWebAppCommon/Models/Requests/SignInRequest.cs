using System.ComponentModel.DataAnnotations;

namespace PartyWebAppCommon.Models;

public class SignInRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}