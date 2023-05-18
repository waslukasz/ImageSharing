using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.Utility.Pagination;

namespace WebAPI.Managers;

public interface IPostManager
{
    Task CreateAsync(Post post);
    Task<PaginatorResult<PostDto>> GetAll(int maxItems, int page);
    Task<PaginatorResult<PostDto>> GetUserPosts(Guid userId, int maxItems, int page);
}