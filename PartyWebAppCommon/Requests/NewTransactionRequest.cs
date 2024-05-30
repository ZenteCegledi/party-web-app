using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.Requests;

public class NewTransactionRequest
{
    public UserDto User { get; set; }
    public WalletDto Wallet { get; set; }
    public int SpentCurrency { get; set; }
    public int Count { get; set; }
    
    public LocationDto? Location { get; set; }
    public EventDto? Event { get; set; }
    
    public TransactionType TransactionType { get; set; }
    
    public DateTime Date { get; set; }
}