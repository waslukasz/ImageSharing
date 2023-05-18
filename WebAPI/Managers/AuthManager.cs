using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Application_Core.Exception;
using AutoMapper;
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
    private readonly RoleManager<RoleEntity> _roleManager;

    private readonly JwtSettings _jwtSettings;
    private readonly IMapper _mapper;

    public AuthManager(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager, JwtSettings jwtSettings, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtSettings = jwtSettings;
        _mapper = mapper;
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
            .AddClaim(JwtRegisteredClaimNames.Email, user.Email)
            .AddClaim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(10).ToUnixTimeSeconds())
            .AddClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid())
            .AddClaim(ClaimTypes.Role, _userManager.GetRolesAsync(user).Result)
            .Audience(_jwtSettings.Audience)
            .Issuer(_jwtSettings.Issuer)
            .Encode();
    }
    
}