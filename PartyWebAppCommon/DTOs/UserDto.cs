namespace PartyWebAppCommon.DTOs;

public class UserDto
{
    public string Username { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public DateTime PasswordUpdated { get; set; }
    public List<WalletDto> Wallets { get; set; }
    public int RoleId { get; set; }
}