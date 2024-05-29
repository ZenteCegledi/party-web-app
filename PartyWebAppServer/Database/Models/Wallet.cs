using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PartyWebAppCommon.Enums;

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
    public bool IsPrimary { get; set; } = false;
    public List<Transaction> Transactions { get; set; }

    public override string ToString()
    {
        return $"Wallet: {Currency} {Amount}";
    }
}