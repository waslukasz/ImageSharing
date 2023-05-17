using Application_Core.Model;

namespace Infrastructure.EF.Repository.PostRepository
{
    public interface IPostRepository
    {
        public Task CreateAsync(Post post);
        public Task UpdateAsync(Post post);
        public Task DeleteAsync(Post post);
        public Task<Post?> GetByGuidAsync(Guid id);
        public IQueryable<Post> GetAllAsync();
        public IQueryable<Post> GetByUserIdAsync(int id);
    }
}