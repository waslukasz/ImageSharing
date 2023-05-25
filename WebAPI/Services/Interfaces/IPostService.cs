using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Pagination;
using WebAPI.Request;

namespace WebAPI.Services.Interfaces;

public interface IPostService
{
    Task CreateAsync(CreatePostRequest postRequest,UserEntity user);
    Task<PaginatorResult<Post>> GetAll(int maxItems, int page);
    Task<PaginatorResult<PostDto>> GetUserPosts(Guid id, int maxItems, int page);
    Task DeleteAsync(DeletePostRequest postRequest,UserEntity user);
    Task<PaginatorResult<PostDto>> GetPostByTags(SearchPostRequest request, PaginationRequest paginationRequest);
}