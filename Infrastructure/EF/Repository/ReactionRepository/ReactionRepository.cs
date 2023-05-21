using Application_Core.Common.Specification;
using Application_Core.Model;
using Infrastructure.Database;
using Infrastructure.EF.Evaluator;

namespace Infrastructure.EF.Repository.ReactionRepository
{
    public class ReactionRepository : BaseRepository<Reaction,int>, IReactionRepository
    {
        public ReactionRepository(ImageSharingDbContext context) : base(context)
        {
            
        }
        
        public async Task<int> GetReactionsCountByPostAsync(Post post)
        {                        
            return await Task.FromResult(Context.Reactions.Where(i=>i.Post==post).Count());
        }

    }
}
