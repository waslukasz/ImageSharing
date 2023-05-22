using System.ComponentModel.DataAnnotations;

namespace WebAPI.Request;

public class UpdateAccountRequest
{
    [EmailAddress]
    [Required]
    [Display(Name = "Email address")]
    public string Email { get; set; }
    [Required]
    [MinLength(3)]
    public string Username { get; set; }
    [Required]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password has to meet specified criteria: min. 8 characters, 1 lower case, 1 uppercase, 1 number, 1 special character.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}