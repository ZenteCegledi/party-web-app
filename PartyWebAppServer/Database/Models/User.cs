using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PartyWebAppCommon.Enums;

namespace PartyWebAppServer.Database.Models;

public class User
{
    [Key]
    public string Username { get; set; }

    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public string Password { get; set; }
    public DateTime PasswordUpdated { get; set; }

    public List<Wallet> Wallets { get; set; }

    [ForeignKey("Role")]
    public int RoleId { get; set; }
    
    public LanguageType Language { get; set; } = LanguageType.Hu;
}