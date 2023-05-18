using Infrastructure.EF.Entity;
using WebAPI.Request;

namespace WebAPI.Managers;

public interface IReactionManager
{
    Task AddReaction(AddReactionRequest request, UserEntity user);
}