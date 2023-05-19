using System.ComponentModel.DataAnnotations;

namespace WebAPI.Request;

public class ChangeUsernameAccountRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [MinLength(3)]
    [Display(Name = "Username")]
    public string NewUsername { get; set; }
}