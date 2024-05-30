using PartyWebAppCommon.DTOs;

namespace PartyWebAppServer.ErrorHandling.ErrorModels;

public class LocationShouldBeAtmErrorModel
{
    public int TransactionTypeId { get; set; }
    public int LocationTypeId { get; set; }
}