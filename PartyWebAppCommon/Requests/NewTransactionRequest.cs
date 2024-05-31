using PartyWebAppCommon.DTOs;
using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.Requests;

public class NewTransactionRequest
{
    public int WalletId { get; set; }
    public int? LocationId { get; set; }
    public int? EventId { get; set; }
    
    public int ItemCount { get; set; }
    public int Amount { get; set; }
    public CurrencyType? Currency { get; set; }
    
    public TransactionType TransactionType { get; set; }
    public DateTime Date { get; set; }
}