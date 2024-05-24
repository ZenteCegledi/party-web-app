namespace PartyWebAppCommon.Requests;

public class CreateUserRequest(
    string username,
    string name,
    DateTime birthDate,
    string email,
    string phone,
    string password)
{
    public string Username { get; set; } = username;
    public string Name { get; set; } = name;
    public DateTime BirthDate { get; set; } = birthDate;
    public string Email { get; set; } = email;
    public string Phone { get; set; } = phone;
    public string Password { get; set; } = password;
}