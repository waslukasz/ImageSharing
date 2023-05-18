using System.ComponentModel.DataAnnotations;

namespace WebAPI.Request;

public class ChangePasswordAccountRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password has to meet specified criteria: min. 8 characters, 1 lower case, 1 uppercase, 1 number, 1 special character.")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
    [Required]
    [Compare("NewPassword", ErrorMessage = "New Password and Confirm new password did not match.")]
    [Display(Name = "Confirm password")]
    [DataType(DataType.Password)]
    public string ConfirmNewPassword { get; set; }
}