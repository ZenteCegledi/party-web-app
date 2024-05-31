using PartyWebAppCommon.Enums;

public class TransactionDto
{
    public int Id { get; set; }
    public int WalletId { get; set; }
    public int LocationId { get; set; }
    public int EventId { get; set; }
    public int SpentCurrency { get; set; }
    public int Count { get; set; }
    public TransactionType TransactionType { get; set; }
    public DateTime Date { get; set; }
}