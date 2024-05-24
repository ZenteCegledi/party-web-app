using System.ComponentModel.DataAnnotations;

public class SignUpModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required] 
    [Compare("Password")] 
    public string ConfirmPassword { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public DateTime BirhtDate { get; set; }

    [Required]
    public int RoleId { get; set; }

    [Required]
    [RegularExpression(@"^06\d{9}$", ErrorMessage = "The phone number should start with 06 and then followed by 9 digits")]
    public string Phone { get; set; }
}