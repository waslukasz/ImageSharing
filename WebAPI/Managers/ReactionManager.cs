using Application_Core.Exception;
using Application_Core.Model;
using AutoMapper;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Repository.PostRepository;
using Infrastructure.EF.Repository.ReactionCommentRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Request;

namespace Infrastructure.Manager
{
    public class ReactionManager
    {
        private readonly IReactionRepository _reactionRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public ReactionManager(IReactionRepository reactionRepository, IMapper mapper, IPostRepository postRepository)
        {
            _reactionRepository = reactionRepository;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task AddReaction(AddReactionRequest request, UserEntity user)
        {
            Post? post = await _postRepository.GetByGuidAsync(request.PostId);

            if (post is null) throw new PostNotFoundException();

            Guid postGuid= request.PostId;

            Reaction reaction = _mapper.Map<Reaction>(request);
            reaction.PostId = _postRepository.GetByGuidAsync(postGuid).Id;

            reaction.User = user;
            //reaction.Post = await _postRepository.GetByGuidAsync(postGuid);

            await _reactionRepository.AddReactionAsync(reaction);
        }
    }
}
