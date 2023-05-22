using System.ComponentModel.DataAnnotations;

namespace WebAPI.Request;

public class AssignRoleAuthRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Role { get; set; }
}