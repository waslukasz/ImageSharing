using Application_Core.Common.Specification;
using Application_Core.Exception;
using Application_Core.Model;
using AutoMapper;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Repository.CommentRepository;
using Infrastructure.EF.Repository.PostRepository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebAPI.Request;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;


        public CommentService(ICommentRepository commentRepository, IMapper mapper, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddComment(AddCommentRequest request, UserEntity user)
        {
            Post? post = await _postRepository.GetByGuid(request.PostId);

            if (post is null) throw new PostNotFoundException();

            Comment comment = new Comment()
            {
                Post= post,
                PostId = post.Id,
                Text= request.Text,
                User= user,
                UserId= user.Id
            };

            await _commentRepository.Add(comment);
            return comment.Guid;
        }

        public async Task<List<CommentDto>> GetAll(Guid postGuId)
        {
            Post post = await _postRepository.GetByGuid(postGuId) ?? throw new PostNotFoundException();

            BaseSpecification<Comment> specification = new BaseSpecification<Comment>();

            specification.AddCriteria(c => c.PostId == post.Id);
            specification
                .AddInclude(c => c.Post)
                .AddInclude(c => c.User);

            List<Comment> comments = (await _commentRepository.GetByCriteria(specification)).ToList();
            List<CommentDto> result = _mapper.Map<List<Comment>, List<CommentDto>>(comments);
            return result;
        }

        public async Task<CommentDto> FindByGuId(Guid commentGuId)
        {
            Comment comment = await _commentRepository.GetCommentByGuIdAsync(commentGuId) ?? throw new CommentNotFoundException();
            CommentDto commentDto = _mapper.Map<CommentDto>(comment);
            
            return commentDto;
        }

        public async Task<CommentDto> Edit(EditCommentRequest request)
        {
            Comment comment = await _commentRepository.GetCommentByGuIdAsync(request.CommentGuId) ?? throw new CommentNotFoundException();
            comment.Text = request.Text;
            await _commentRepository.EditCommentAsync(comment);

            CommentDto commentDto = _mapper.Map<CommentDto>(comment);

            return commentDto;
        }

        public async Task Delete(Guid commentGuid)
        {
            Comment comment = await _commentRepository.GetCommentByGuIdAsync(commentGuid) ?? throw new CommentNotFoundException();
            await _commentRepository.Remove(comment);
        }
    }
}
