using WebAPI.Request;

namespace WebAPI.Managers;

public interface IAccountManager
{
    Task<bool> Register(RegisterAccountRequest request);
    Task<bool> Delete(DeleteAccountRequest request);
    Task<bool> ChangeUsername(ChangeUsernameAccountRequest request);
    Task<bool> ChangeEmail(ChangeEmailAccountRequest request);
    Task<bool> ChangePassword(ChangePasswordAccountRequest request);
}