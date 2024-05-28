using PartyWebAppCommon.Enums;

namespace PartyWebAppServer.ErrorHandling.ErrorModels;

public class LocationShouldNotBeAtmErrorModel
{
    public TransactionType Type { get; set; }
}