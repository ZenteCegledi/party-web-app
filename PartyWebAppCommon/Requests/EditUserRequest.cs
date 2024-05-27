namespace PartyWebAppCommon.Requests;

public class EditUserRequest
{
    public string Username { get; set; }
    public string? Name { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Password { get; set; }
}