using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Application_Core.Exception;
using Infrastructure.EF.Entity;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Identity;
using WebAPI.Configuration;
using WebAPI.Managers.Interfaces;
using WebAPI.Request;

namespace WebAPI.Managers;

public class AuthManager : IAuthManager
{
    private readonly UserManager<UserEntity> _userManager;

    private readonly JwtSettings _jwtSettings;

    public AuthManager(UserManager<UserEntity> userManager, JwtSettings jwtSettings)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings;
    }

    public async Task<string> Login(LoginUserRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        return await _userManager.CheckPasswordAsync(user, request.Password) ? CreateToken(user) : throw new BadRequestException("Invalid username or password.", HttpStatusCode.BadRequest);
    }

    public string CreateToken(UserEntity user)
    {
        return new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
            .AddClaim(JwtRegisteredClaimNames.Name, user.UserName)
            .AddClaim(ClaimTypes.NameIdentifier, user.Id)
            .AddClaim(JwtRegisteredClaimNames.Email, user.Email)
            .AddClaim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(10).ToUnixTimeSeconds())
            .AddClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid())
            .AddClaim(ClaimTypes.Role, _userManager.GetRolesAsync(user).Result)
            .Audience(_jwtSettings.Audience)
            .Issuer(_jwtSettings.Issuer)
            .Encode();
    }
    
}