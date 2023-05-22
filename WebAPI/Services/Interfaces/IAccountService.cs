using Infrastructure.EF.Entity;
using WebAPI.Request;
using WebAPI.Response;

namespace WebAPI.Services.Interfaces;

public interface IAccountService
{
    Task<GetAccountResponse> GetByNameAsync(string username);
    Task<bool> CreateAsync(CreateAccountRequest request);
    Task<bool> DeleteAsync(UserEntity request);
    Task<bool> UpdateAsync(string username, UpdateAccountRequest request);
}