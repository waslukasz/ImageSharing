namespace WebAPI.Response;

public class GetRolesAccountResponse
{
    public string Username { get; set; }
    public List<string> Roles { get; set; }
}