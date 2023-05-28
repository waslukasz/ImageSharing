using Application_Core.Common.Specification;
using Application_Core.Exception;
using Application_Core.Model;
using Application_Core.Model.Interface;
using AutoMapper;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Pagination;
using Infrastructure.EF.Repository.CommentRepository;
using Infrastructure.EF.Repository.PostRepository;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System.Net;
using Application_Core.Common.Specification.Comment;
using WebAPI.Request;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;
        private readonly Paginator<Comment> _paginator;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IPostRepository postRepository, UserManager<UserEntity> userManager)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _mapper = mapper;
            _userManager = userManager;
            _paginator = new Paginator<Comment>();
        }

        public async Task<Guid> AddComment(AddCommentRequest request, UserEntity user)
        {
            Post? post = await _postRepository.GetByGuid(request.PostId);

            if (post is null) 
                throw new PostNotFoundException();

            Comment comment = new Comment()
            {
                Post = post,
                PostId = post.Id,
                Text = request.Text,
                User = user,
                UserId = user.Id
            };

            await _commentRepository.Add(comment);
            return comment.Guid;
        }

        public async Task<PaginatorResult<CommentDto>> GetAll(GetAllCommentsRequest request)
        {
            Post post = await _postRepository.GetByGuid(request.PostGuid) ?? throw new PostNotFoundException();

            PaginatorResult<Comment> result = await _paginator
                .SetItemNumberPerPage(request.ItemNumber)
                .Paginate(_commentRepository.GetAllCommentsQueryAsync(post.Id), request.Page);

            PaginatorResult<CommentDto> resultDto = new(result.TotalItems, result.ItemsOnPage
            , _mapper.Map<List<CommentDto>>(result.Items), result.CurrentPage, result.TotalPages);

            return resultDto;
        }

        public async Task<CommentDto> FindByGuId(Guid commentGuId)
        {
            BaseSpecification<Comment> specification = new GetCommentByGuidSpecification(commentGuId);
            
            Comment comment = await _commentRepository.GetByCriteriaSingle(specification) 
                              ?? throw new CommentNotFoundException();
            
            CommentDto commentDto = _mapper.Map<CommentDto>(comment);

            return commentDto;
        }

        public async Task<CommentDto> Edit(EditCommentRequest request, UserEntity user, Guid id)
        {
            BaseSpecification<Comment> specification = new GetCommentByGuidSpecification(id);

            Comment comment = await _commentRepository.GetByCriteriaSingle(specification) 
                              ?? throw new CommentNotFoundException();
            
            if (comment.UserId != user.Id) throw new NotAuthorizedException();

            comment.Text = request.Text;
            await _commentRepository.EditCommentAsync(comment);

            CommentDto commentDto = _mapper.Map<CommentDto>(comment);

            return commentDto;
        }

        public async Task Delete(Guid commentGuid, UserEntity user)
        {
            BaseSpecification<Comment> specification = new GetCommentByGuidSpecification(commentGuid);
            
            Comment comment = await _commentRepository.GetByCriteriaSingle(specification) 
                              ?? throw new CommentNotFoundException();
            
            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                if (comment.UserId != user.Id)
                {
                    throw new NotAuthorizedException();
                }
            }

            await _commentRepository.Remove(comment);
        }
    }
}