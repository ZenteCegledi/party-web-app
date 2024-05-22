using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyWebAppServer.Database.Models;

public class Event
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public enum Type {Concert, Theater, Exhibition}
    [ForeignKey("Locations")]
    public string Location { get; set; }
    public int Price { get; set; }
}