using Application_Core.Model;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Repository.PostRepository
{
    public class PostRepository : IPostRepository
    {
        private readonly ImageSharingDbContext _context;

        public PostRepository(ImageSharingDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Post> GetAllAsync()
        => _context.Posts.Include(x=>x.Status);

        public async Task<Post?> GetByGuidAsync(Guid id)
        =>await _context.Posts.Where(p=>p.Guid==id).FirstOrDefaultAsync();

        public IQueryable<Post> GetByUserIdAsync(int id)
        => _context.Posts.Where(x=>x.UserId==id).Include(x=>x.Status);

        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
           await _context.SaveChangesAsync();
        }
    }
}
