using Infrastructure.EF.Entity;
using WebAPI.Request;

namespace WebAPI.Services.Interfaces;

public interface IAuthService
{
    public string CreateToken(UserEntity user);
    public Task<string> Login(LoginUserRequest request);
    Task<bool> AssignRole(AssignRoleAuthRequest request);
    Task<bool> RemoveRole(RemoveRoleAuthRequest request);
}