using Application_Core.Common.Specification;
using Application_Core.Model;

namespace Infrastructure.EF.Repository.ReactionRepository
{
    public interface IReactionRepository : IRepositoryBase<Reaction>
    {
        Task<int> GetReactionsCountByPostAsync(Post post);
        
    }
}
