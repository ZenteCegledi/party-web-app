using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.DTOs;

public class TransactionDto
{
    public WalletDto Wallet { get; set; }
    
    public LocationDto Location { get; set; }
    public EventDTO Event { get; set; }
    
    public int ItemCount { get; set; }
    public int Amount { get; set; }
    public CurrencyType Currency { get; set; }
    
    public TransactionType TransactionType { get; set; }
    public DateTime Date { get; set; }
}