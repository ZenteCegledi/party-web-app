using System.ComponentModel.DataAnnotations;

namespace PartyWebAppCommon.Models;

public class SignUpRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Username { get; set; }
    public DateTime BirhtDate { get; set; }
    public int RoleId { get; set; }
    public string Phone { get; set; }
}