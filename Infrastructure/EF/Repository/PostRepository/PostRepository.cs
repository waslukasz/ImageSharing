using Application_Core.Model;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Core.Common.Specification;
using Infrastructure.EF.Evaluator;

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

        public async Task<Post?> GetByGuidAsync(Guid id)
        {
            return await _context.Posts.Where(p=>p.Guid==id).FirstOrDefaultAsync();
        }
        
        public IQueryable<Post> GetByCriteriaQuery(ISpecification<Post> criteria)
        {
            return SpecificationToQueryEvaluator<Post>.ApplySpecification(_context.Posts,criteria);
        }

        public async Task<IEnumerable<Post>> GetByCriteriaAsync(ISpecification<Post> criteria)
        {
            return await GetByCriteriaQuery(criteria).ToListAsync();
        }

        public IQueryable<Post> GetAllQuery()
        {
            return _context.Posts;
        }

        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
           await _context.SaveChangesAsync();
        }
    }
}
