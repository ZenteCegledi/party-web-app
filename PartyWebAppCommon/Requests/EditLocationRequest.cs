using PartyWebAppCommon.enums;

namespace PartyWebAppCommon.Requests;

public class EditLocationRequest
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public LocationType? Type { get; set; }
}