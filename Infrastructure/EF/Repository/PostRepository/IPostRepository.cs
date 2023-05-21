using Application_Core.Common.Specification;
using Application_Core.Model;

namespace Infrastructure.EF.Repository.PostRepository
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        public Task UpdateAsync(Post post);
        public IQueryable<Post> GetAllQuery();
    }
}