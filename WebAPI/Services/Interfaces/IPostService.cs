using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Pagination;

namespace WebAPI.Services.Interfaces;

public interface IPostService
{
    Task CreateAsync(Post post);
    Task<PaginatorResult<PostDto>> GetAll(int maxItems, int page);
    Task<PaginatorResult<PostDto>> GetUserPosts(Guid id, int maxItems, int page);
}