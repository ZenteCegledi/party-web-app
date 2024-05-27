using System.ComponentModel.DataAnnotations;
using PartyWebAppCommon.Enums;

namespace PartyWebAppServer.Database.Models;

public class Role
{

    [Key]
    public int Id { get; set; }

    public RoleType Name { get; set; }

}