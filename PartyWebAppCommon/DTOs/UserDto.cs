namespace PartyWebAppCommon.DTOs;

public class UserDto
{
    public string Username { get; set; }

    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public List<WalletDto> Wallets { get; set; }
}