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
using Microsoft.AspNetCore.Mvc;
using WebAPI.Configuration;
using WebAPI.Dto;
using WebAPI.Managers.Interfaces;

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

    public async Task<string> Authenticate(LoginUserDto user)
    {
        var logged = await _userManager.FindByNameAsync(user.Username);
        if (await _userManager.CheckPasswordAsync(logged, user.Password)) return CreateToken(logged);
        
        throw new BadRequestException("Invalid username or password.", HttpStatusCode.BadRequest);
    }

    public async Task<bool> RegisterUser(RegisterUserDto dto)
    {
        var user = _mapper.Map<UserEntity>(dto);
        if (await _userManager.FindByEmailAsync(dto.Email) is not null)
            throw new BadRequestException("Email already in use.", HttpStatusCode.BadRequest);
        
        if (await _userManager.FindByNameAsync(dto.UserName) is not null)
            throw new BadRequestException("Username already in use.", HttpStatusCode.BadRequest);
        
        var result = await _userManager.CreateAsync(user, dto.Password);
        await _userManager.AddToRoleAsync(user, "User");
        return result.Succeeded;
    }
    
    public string CreateToken(UserEntity user)
    {
        return new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
            .AddClaim(JwtRegisteredClaimNames.Name, user.UserName)
            .AddClaim(JwtRegisteredClaimNames.Gender, "male")
            .AddClaim(JwtRegisteredClaimNames.Email, user.Email)
            .AddClaim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds())
            .AddClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid())
            .AddClaim(ClaimTypes.Role, _userManager.GetRolesAsync(user).Result)
            .Audience(_jwtSettings.Audience)
            .Issuer(_jwtSettings.Issuer)
            .Encode();
    }
}