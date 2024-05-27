using System.ComponentModel.DataAnnotations;
using PartyWebAppCommon.Enums;

namespace PartyWebAppServer.Database.Models;

public class Transaction
{
    [Key] public int Id { get; set; }
    public Wallet Wallet { get; set; }
    public int SpentCurrency { get; set; }
    public int Count { get; set; }
    public Location? Location { get; set; }
    public Event? Event { get; set; }
    public TransactionType TransactionType { get; set; }
    public DateTime Date { get; set; }
}