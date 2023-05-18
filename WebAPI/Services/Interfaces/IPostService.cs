using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.Utility.Pagination;

namespace WebAPI.Services;

public interface IPostService
{
    Task CreateAsync(Post post);
    Task<PaginatorResult<PostDto>> GetAll(int maxItems, int page);
    Task<PaginatorResult<PostDto>> GetUserPosts(Guid id, int maxItems, int page);
}