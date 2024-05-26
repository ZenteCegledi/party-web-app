namespace PartyWebAppCommon.DTOs;

public class UserDto
{
    public string Username { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public RoleDto RoleId { get; set; }
}