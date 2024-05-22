using System.ComponentModel.DataAnnotations;
using PartyWebAppCommon.enums;

namespace PartyWebAppServer.Database.Models;

public class Locations
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public LocationType Type { get; set; }
}