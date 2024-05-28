using PartyWebAppCommon.DTOs;

namespace PartyWebAppServer.ErrorHandling.ErrorModels;

public class LocationShouldBeAtmErrorModel
{
    public LocationDTO Location { get; set; }
}