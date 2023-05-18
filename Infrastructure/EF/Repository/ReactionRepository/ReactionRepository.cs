using Application_Core.Model;
using Infrastructure.Database;
using Infrastructure.EF.Entity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EF.Repository.ReactionCommentRepository
{
    public class ReactionRepository : IReactionRepository
    {
        private readonly ImageSharingDbContext _context;

        public ReactionRepository(ImageSharingDbContext context)
        {
            _context = context;
        }

        public async Task AddReactionAsync(Reaction reaction)
        {
            
            if(_context.Reactions.Contains(reaction))
            {
                await DeleteAsync(reaction);
            }
            else
            {
                _context.Reactions.Add(reaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Reaction reaction)
        {
            _context.Reactions.Remove(reaction);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetReactionsCountByPostAsync(Post post)
        {                        
            return await Task.FromResult(_context.Reactions.Where(i=>i.Post==post).Count());
        }

    }
}
