using System.ComponentModel.DataAnnotations;

namespace PartyWebAppCommon.Models;

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
    public string Phone { get; set; }
}