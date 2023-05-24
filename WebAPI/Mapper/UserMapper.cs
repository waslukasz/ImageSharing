using Infrastructure.EF.Entity;
using WebAPI.Response;

namespace WebAPI.Mapper;

public static class UserMapper
{
    public static UserResponse FromUserToUserResponse(UserEntity user)
    {
        return new UserResponse()
        {
            Id = user.Guid,
            Name = user.UserName ?? string.Empty
        };
    }
}