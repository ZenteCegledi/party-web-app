using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PartyWebAppCommon.Enums;

namespace PartyWebAppServer.Database.Models;

public class Transaction
{
    [Key] public int Id { get; set; }
    
    [ForeignKey("Wallet")] public CurrencyType WalletCurrency { get; set; }
    [ForeignKey("Wallet")] public string WalletUsername { get; set; }
    public Wallet Wallet { get; set; }
    
    [ForeignKey("Location")] public int LocationId { get; set; }
    public Location? Location { get; set; }
    
    [ForeignKey("Event")] public int EventId { get; set; }
    public Event? Event { get; set; }
    
    public int SpentCurrency { get; set; }
    public int Count { get; set; }
    
    public TransactionType TransactionType { get; set; }
    public DateTime Date { get; set; }
}