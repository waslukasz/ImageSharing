using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Request;

public class DeleteAccountRequest
{
    [Required]
    [Display(Name = "Username")]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [DefaultValue("string")]
    public string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "Password and Confirm password did not match.")]
    [Display(Name = "Confirm password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}