using System.ComponentModel;

namespace WebAPI.Request;

public class LoginUserRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}