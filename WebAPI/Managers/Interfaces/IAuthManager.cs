using Infrastructure.EF.Entity;
using WebAPI.Request;

namespace WebAPI.Managers.Interfaces;

public interface IAuthManager
{
    public string CreateToken(UserEntity user);
    public Task<string> Login(LoginUserRequest request);
}