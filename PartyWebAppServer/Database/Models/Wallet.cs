using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyWebAppServer.Database.Models;

public class Wallet
{
    public enum CurrencyType
    {
        HUF,
        EUR,
        USD,
        CREDIT
    }

    [Key]
    public CurrencyType Currency { get; set; }

    [Key]
    [ForeignKey("User")]
    public int UserID { get; set; }
    public User Owner { get; set; }

    public decimal Amount { get; set; }


    public Wallet(CurrencyType currency, decimal amount, User owner)
    {
        Currency = currency;
        Amount = amount;
        Owner = owner;
    }

    public Wallet()
    {
    }

    public override string ToString()
    {
        return $"Wallet: {Currency} {Amount}";
    }
}using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Wallet
{
    public enum CurrencyType
    {
        HUF,
        EUR,
        USD,
        CREDIT
    }

    [Key]
    public CurrencyType Currency { get; set; }

    [Key]
    [ForeignKey("User")]
    public int UserID { get; set; }
    public User Owner { get; set; }

    public decimal Amount { get; set; }


    public Wallet(CurrencyType currency, decimal amount, User owner)
    {
        Currency = currency;
        Amount = amount;
        Owner = owner;
    }

    public Wallet()
    {
    }

    public override string ToString()
    {
        return $"Wallet: {Currency} {Amount}";
    }
}