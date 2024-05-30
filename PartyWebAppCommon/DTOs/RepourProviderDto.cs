namespace PartyWebAppCommon.DTOs;

public class RepourProviderDto
{
    public string Name { get; set; }
    public Guid RepourToken { get; set; }
    public decimal RepourPrice { get; set; }
}