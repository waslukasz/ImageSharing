using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Request;

public class CreateAccountRequest
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
    [DefaultValue("string")]
    public string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "Password and Confirm password did not match.")]
    [Display(Name = "Confirm password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}