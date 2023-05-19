using Application_Core.Common.Specification;
using Application_Core.Model;

namespace Infrastructure.EF.Repository.PostRepository
{
    public interface IPostRepository
    {
        public Task CreateAsync(Post post);
        public Task UpdateAsync(Post post);
        public Task DeleteAsync(Post post);
        public Task<Post?> GetByGuidAsync(Guid id);
        public IQueryable<Post> GetByCriteriaQuery(ISpecification<Post> criteria);
        public Task<IEnumerable<Post>> GetByCriteriaAsync(ISpecification<Post> criteria);
        public IQueryable<Post> GetAllQuery();
    }
}