using System.Net;
using Application_Core.Exception;
using AutoMapper;
using Infrastructure.EF.Entity;
using Microsoft.AspNetCore.Identity;
using WebAPI.Request;
using WebAPI.Response;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services;

public class AccountService : IAccountService
{
    private readonly IMapper _mapper;
    private readonly UserManager<UserEntity> _userManager;

    public AccountService(IMapper mapper, UserManager<UserEntity> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<GetAccountResponse> GetByNameAsync(string username)
    {
        var user = await _userManager.FindByNameAsync(username) ?? throw new UserNotFoundException();
        var result = _mapper.Map<GetAccountResponse>(user);
        return result;
    }

    public async Task<bool> CreateAsync(CreateAccountRequest request)
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

    public async Task<bool> UpdateAsync(string username, UpdateAccountRequest request)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (await _userManager.FindByNameAsync(username) is null)
            throw new UserNotFoundException();

        _mapper.Map(request, user);
        return (await _userManager.UpdateAsync(user)).Succeeded;
    }

    public async Task<bool> DeleteAsync(UserEntity user)
    {
        return (await _userManager.DeleteAsync(user)).Succeeded;
    }
}