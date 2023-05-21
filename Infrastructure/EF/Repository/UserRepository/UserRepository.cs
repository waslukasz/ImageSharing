using Infrastructure.Database;
using Infrastructure.EF.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repository.UserRepository;

public class UserRepository : BaseRepository<UserEntity,int>, IUserRepository 
{
    public UserRepository(ImageSharingDbContext context) : base(context)
    {
        
    }
}