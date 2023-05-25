using Application_Core.Common.Specification;
using Application_Core.Model;

namespace Infrastructure.EF.Repository.PostRepository
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        Task UpdateAsync(Post post);
        IQueryable<Post> GetAllQuery();
        IQueryable<Post> GetByTagsQuery(List<string> tags, ISpecification<Post> specification);
    }
}