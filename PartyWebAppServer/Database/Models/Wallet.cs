using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PartyWebAppCommon.enums;

namespace PartyWebAppServer.Database.Models;

public class Wallet
{

    [Key]
    public CurrencyType Currency { get; set; }

    [Key]
    [ForeignKey("User")]
    public string Username { get; set; }
    public User Owner { get; set; }

    public decimal Amount { get; set; }

    public override string ToString()
    {
        return $"Wallet: {Currency} {Amount}";
    }
}