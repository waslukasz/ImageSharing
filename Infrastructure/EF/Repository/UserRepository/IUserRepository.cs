using Infrastructure.EF.Entity;

namespace Infrastructure.EF.Repository.UserRepository;

public interface IUserRepository
{
    public Task<UserEntity?> GetUserByGuidAsync(Guid id);
}