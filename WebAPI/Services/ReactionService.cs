using Application_Core.Common.Specification;
using Application_Core.Exception;
using Application_Core.Model;
using AutoMapper;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Repository.PostRepository;
using Infrastructure.EF.Repository.ReactionRepository;
using WebAPI.Request;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class ReactionService : IReactionService
    {
        private readonly IReactionRepository _reactionRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public ReactionService(IReactionRepository reactionRepository, IMapper mapper, IPostRepository postRepository)
        {
            _reactionRepository = reactionRepository;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task ToggleReaction(AddReactionRequest request, UserEntity user)
        {
            Post post = await _postRepository.GetByGuid(request.Id) ?? throw new PostNotFoundException();

            BaseSpecification<Reaction> criteria = new BaseSpecification<Reaction>();
            
            criteria.AddCriteria(r => r.Post == post);
            criteria.AddCriteria(r => r.User == user);

            Reaction? reaction = await _reactionRepository.GetByCriteriaSingle(criteria);

            if (reaction is null)
            {
                await _reactionRepository.Add(
                    new Reaction() 
                    {
                        Post = post,
                        User = user 
                    }
                );
            }
            else
            {
                await _reactionRepository.Remove(reaction);
            }
        }
    }
}
