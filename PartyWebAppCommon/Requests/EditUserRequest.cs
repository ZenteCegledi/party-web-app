using PartyWebAppCommon.Enums;

namespace PartyWebAppCommon.Requests;

public class EditUserRequest
{
    public string? Name { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Password { get; set; }
    
    public LanguageType Language { get; set; }
}