using System.ComponentModel.DataAnnotations;
using PartyWebAppCommon.Enums;

namespace PartyWebAppServer.Database.Models;

public class Location
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public LocationType? Type { get; set; }
    public List<Transaction> Transactions { get; set; }
}