using Infrastructure.EF.Entity;
using WebAPI.Request;

namespace WebAPI.Services.Interfaces;

public interface IReactionService
{
    Task ToggleReaction(AddReactionRequest request, UserEntity user);
}