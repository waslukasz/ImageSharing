using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Application_Core.Exception;
using Infrastructure.EF.Entity;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Configuration;
using WebAPI.Request;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<UserEntity> _userManager;

    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<UserEntity> userManager, JwtSettings jwtSettings)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings;
    }

    public async Task<string> Login(LoginUserRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        return await _userManager.CheckPasswordAsync(user, request.Password) ? CreateToken(user) : throw new BadRequestException("Invalid username or password.", HttpStatusCode.BadRequest);
    }

    public async Task<bool> AssignRole([FromBody] AssignRoleAuthRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username) ?? throw new UserNotFoundException();
        if ((await _userManager.AddToRoleAsync(user, request.Role)).Succeeded)
        {
            return true;
        }
        return false;
    }

    public async Task<bool> RemoveRole(RemoveRoleAuthRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username) ?? throw new UserNotFoundException();
        if ((await _userManager.RemoveFromRoleAsync(user, request.Role)).Succeeded)
        {
            return true;
        }
        return false;
    }

    public string CreateToken(UserEntity user)
    {
        return new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
            .AddClaim(JwtRegisteredClaimNames.Name, user.UserName)
            .AddClaim(ClaimTypes.NameIdentifier, user.Id)
            .AddClaim(JwtRegisteredClaimNames.Email, user.Email)
            // TODO: Ustawić czas na 15 minut przed oddaniem projektu.
            .AddClaim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddDays(365).ToUnixTimeSeconds())
            .AddClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid())
            .AddClaim(ClaimTypes.Role, _userManager.GetRolesAsync(user).Result)
            .Audience(_jwtSettings.Audience)
            .Issuer(_jwtSettings.Issuer)
            .Encode();
    }
}