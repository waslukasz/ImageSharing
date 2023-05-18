using Application_Core.Model;
using Infrastructure.EF.Entity;
using WebAPI.Request;

namespace WebAPI.Services.Interfaces;

public interface ICommentManager
{
    Task<Guid> AddComment(AddCommentRequest request, UserEntity user);
    Task<List<Comment>> GetAll(Guid postGuId);
}