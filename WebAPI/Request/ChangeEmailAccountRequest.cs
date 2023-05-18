using System.ComponentModel.DataAnnotations;

namespace WebAPI.Request;

public class ChangeEmailAccountRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [EmailAddress]
    [Required]
    [Display(Name = "New email address")]
    public string NewEmail { get; set; }
    [EmailAddress]
    [Required]
    [Compare("NewEmail", ErrorMessage = "New email and Confirm new email don't match.")]
    public string ConfirmNewEmail { get; set; }
}