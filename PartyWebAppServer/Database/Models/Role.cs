using System.ComponentModel.DataAnnotations;
using PartyWebAppCommon.enums;

namespace PartyWebAppServer.Database.Models;

public class Role
{
    // id
    // name
    [Key]
    public int Id { get; set; }
    
    public RoleType Name { get; set; }
    
    public override string ToString()
    {
        return $"Role: {Name}";
    }
    
}