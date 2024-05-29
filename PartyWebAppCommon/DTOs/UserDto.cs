namespace PartyWebAppCommon.DTOs;

public class UserDTO
{
    public string Username { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public RoleDTO RoleId { get; set; }
}