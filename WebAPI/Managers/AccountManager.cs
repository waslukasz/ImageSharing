using System.Net;
using Application_Core.Exception;
using AutoMapper;
using Infrastructure.EF.Entity;
using Microsoft.AspNetCore.Identity;
using WebAPI.Managers.Interfaces;
using WebAPI.Request;

namespace WebAPI.Managers;

public class AccountManager : IAccountManager
{
    private readonly IMapper _mapper;
    private readonly UserManager<UserEntity> _userManager;

    public AccountManager(IMapper mapper, UserManager<UserEntity> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }
    
    public async Task<bool> Register(RegisterAccountRequest request)
    {
        var user = _mapper.Map<UserEntity>(request);

        if (await _userManager.FindByEmailAsync(request.Email) is not null)
            throw new BadRequestException("Email already in use.", HttpStatusCode.BadRequest);
        
        if (await _userManager.FindByNameAsync(request.Username) is not null)
            throw new BadRequestException("Username already in use.", HttpStatusCode.BadRequest);
        
        var result = await _userManager.CreateAsync(user, request.Password);
        await _userManager.AddToRoleAsync(user, "User");
        return result.Succeeded;
    }

    public async Task<bool> Delete(DeleteAccountRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (!await _userManager.CheckPasswordAsync(user, request.Password)) return false;
        await _userManager.DeleteAsync(user);
        return true;
    }
    
    public async Task<bool> ChangeUsername(ChangeUsernameAccountRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (!await _userManager.CheckPasswordAsync(user, request.Password)) return false;
        await _userManager.SetUserNameAsync(user, request.NewUsername);
        return true;
    }
    
    public async Task<bool> ChangeEmail(ChangeEmailAccountRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (!await _userManager.CheckPasswordAsync(user, request.Password)) return false;
        await _userManager.SetEmailAsync(user, request.NewEmail);
        return true;
    }
    
    public async Task<bool> ChangePassword(ChangePasswordAccountRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            return false;
        
        await _userManager.ChangePasswordAsync(user, request.Password, request.NewPassword);
        return true;
    }
}