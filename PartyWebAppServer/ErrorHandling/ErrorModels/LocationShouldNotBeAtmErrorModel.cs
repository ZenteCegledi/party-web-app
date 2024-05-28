using PartyWebAppCommon.DTOs;

namespace PartyWebAppServer.ErrorHandling.ErrorModels;

public class LocationShouldNotBeAtmErrorModel
{
    public TransactionDto Transaction { get; set; }
}