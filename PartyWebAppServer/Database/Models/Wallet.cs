using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PartyWebAppCommon.enums;

namespace PartyWebAppServer.Database.Models;

public class Wallet
{

    [Key]
    public CurrencyTypeEnum Currency { get; set; }

    [Key]
    [ForeignKey("User")]
    public int UserID { get; set; }
    public User Owner { get; set; }

    public decimal Amount { get; set; }

    public Wallet(CurrencyTypeEnum currency, decimal amount, User owner)
    {
        Currency = currency;
        Amount = amount;
        Owner = owner;
    }

    public override string ToString()
    {
        return $"Wallet: {Currency} {Amount}";
    }
}