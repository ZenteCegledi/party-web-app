using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.Requests;

public class CreateLocationRequest
{
    public string Name { get; set; }
    public string Address { get; set; }
    public LocationType Type { get; set; }
}