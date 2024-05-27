using PartyWebAppCommon.enums;

namespace PartyWebAppCommon.DTOs;

public class TransactionDto
{
    public int SpentCurrency { get; set; }
    public int Count { get; set; }
    public TransactionType TransactionType { get; set; }
    public DateTime Date { get; set; }
}