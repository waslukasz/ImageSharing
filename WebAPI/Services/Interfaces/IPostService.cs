using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Pagination;
using Infrastructure.Enum;
using WebAPI.Request;

namespace WebAPI.Services.Interfaces;

public interface IPostService
{
    Task CreateAsync(CreatePostRequest postRequest,UserEntity user);
    Task<PaginatorResult<Post>> GetAll(int maxItems, int page);
    Task<PaginatorResult<PostDto>> GetUserPosts(Guid id, int maxItems, int page);
    Task DeleteAsync(DeletePostRequest postRequest,UserEntity user);
    Task<PaginatorResult<PostDto>> GetPostByTags(SearchPostRequest request, PaginationRequest paginationRequest);
    Task EditAsync(UpdatePostRequest postRequest, UserEntity user);
    Task<Post> GetPost(Guid id);
    PostService SetUser(UserEntity userEntity, RoleEnum roleEnum = RoleEnum.User);
}