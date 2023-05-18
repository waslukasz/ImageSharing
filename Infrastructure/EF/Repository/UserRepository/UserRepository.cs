using Infrastructure.Database;
using Infrastructure.EF.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Repository.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly ImageSharingDbContext _context;

    public UserRepository(ImageSharingDbContext context)
    {
        _context = context;
    }

    public async Task<UserEntity?> GetUserByGuidAsync(Guid id)
    {
        return await _context.Users.Where(u => u.Guid == id).FirstOrDefaultAsync();
    }
}