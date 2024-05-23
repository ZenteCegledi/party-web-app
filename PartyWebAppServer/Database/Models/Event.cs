using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PartyWebAppCommon.enums;

namespace PartyWebAppServer.Database.Models;

public class Event
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public EventType? Type {get ; set;}
    [ForeignKey("Location")]
    public int LocationId { get; set; }
    public Location? Location { get; set; }
    public int Price { get; set; }
}