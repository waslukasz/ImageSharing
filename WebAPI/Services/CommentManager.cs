using Application_Core.Exception;
using Application_Core.Model;
using AutoMapper;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Repository.CommentRepository;
using Infrastructure.EF.Repository.PostRepository;
using Infrastructure.EF.Repository.ReactionCommentRepository;
using WebAPI.Request;

namespace WebAPI.Managers
{
    public class CommentManager
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public CommentManager(ICommentRepository commentRepository, IMapper mapper, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddComment(AddCommentRequest request, UserEntity user)
        {
            Post? post = await _postRepository.GetByGuidAsync(request.PostId);

            if (post is null) throw new PostNotFoundException();

            Comment comment = new Comment()
            {
                Post= post,
                PostId = post.Id,
                Text= request.Text,
                User= user,
                UserId= user.Id
            };

            return await _commentRepository.AddCommentAsync(comment);
        }

        public async Task<List<Comment>> GetAll(Guid postGuId)
        {
            int postId = _postRepository.GetByGuidAsync(postGuId).Id;
            List<Comment> result = await _commentRepository.GetAllCommentsAsync(postId);
            return result;
        }
    }
}
