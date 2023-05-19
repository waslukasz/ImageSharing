using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using WebAPI.Request;

namespace WebAPI.Services.Interfaces;

public interface ICommentService
{
    Task<Guid> AddComment(AddCommentRequest request, UserEntity user);
    Task<List<CommentDto>> GetAll(Guid postGuId);
    Task<CommentDto> FindByGuId(Guid commentGuId);
    Task<CommentDto> Edit(EditCommentRequest request);
    Task Delete(Guid CommentGuid);
}