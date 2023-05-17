using Infrastructure.EF.Entity;
using WebAPI.Dto;

namespace WebAPI.Managers.Interfaces;

public interface IAuthManager
{
    public string CreateToken(UserEntity user);
    public Task<string> Authenticate(LoginUserDto user);
    public Task<bool> RegisterUser(RegisterUserDto dto);
}