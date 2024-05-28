using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.DTOs;

public class TransactionDTO
{
    public int SpentCurrency { get; set; }
    public int Count { get; set; }
    public TransactionType TransactionType { get; set; }
    public DateTime Date { get; set; }
}