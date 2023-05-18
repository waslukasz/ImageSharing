using Infrastructure.EF.Entity;
using WebAPI.Request;

namespace WebAPI.Managers;

public interface IReactionService
{
    Task AddReaction(AddReactionRequest request, UserEntity user);
}