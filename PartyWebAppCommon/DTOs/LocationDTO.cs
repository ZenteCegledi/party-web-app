using Microsoft.AspNetCore.Components.Forms.Mapping;
using PartyWebAppCommon.enums;

namespace PartyWebAppCommon.DTOs;

public class LocationDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public LocationType? Type { get; set; }
}
