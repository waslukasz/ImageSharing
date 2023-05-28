using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Pagination;
using WebAPI.Request;

namespace WebAPI.Services.Interfaces;

public interface ICommentService
{
    Task<Guid> AddComment(AddCommentRequest request, UserEntity user);
    Task<PaginatorResult<CommentDto>> GetAll(GetAllCommentsRequest request);
    Task<CommentDto> FindByGuId(Guid commentGuId);
    Task<CommentDto> Edit(EditCommentRequest request, UserEntity user, Guid id);
    Task Delete(Guid commentGuid, UserEntity user);
}