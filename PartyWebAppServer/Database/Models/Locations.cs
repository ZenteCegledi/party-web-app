using System.ComponentModel.DataAnnotations;

namespace PartyWebAppServer.Database.Models;

public class Locations
{
    public enum LocationType
    {
        club,
        pub,
        atm,
        theater,
        museum
    }
    
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public LocationType Type { get; set; }
}