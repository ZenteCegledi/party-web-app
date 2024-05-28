using PartyWebAppCommon.DTOs;

namespace PartyWebAppServer.ErrorHandling.ErrorModels;

public class LocationShouldBeAtmErrorModel
{
    public string Name { get; set; }
    public string Address { get; set; }
}