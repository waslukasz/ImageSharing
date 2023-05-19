using Application_Core.Common.Specification;
using Application_Core.Model;

namespace Infrastructure.EF.Repository.ReactionRepository
{
    public interface IReactionRepository
    {
        Task AddReactionAsync(Reaction reaction);
        Task DeleteAsync(Reaction reaction);
        Task<int> GetReactionsCountByPostAsync(Post post);

        Task<Reaction?> FindByCriteria(ISpecification<Reaction> criteria);

    }
}
