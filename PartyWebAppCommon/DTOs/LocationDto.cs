using Microsoft.AspNetCore.Components.Forms.Mapping;
using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.DTOs;

public class LocationDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public LocationType? Type { get; set; }
}
