using Application_Core.Common.Specification;
using Application_Core.Exception;
using Application_Core.Model;
using AutoMapper;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Repository.PostRepository;
using Infrastructure.EF.Repository.UserRepository;
using Infrastructure.Utility.Pagination;

namespace WebAPI.Managers
{
    public class PostManager : IPostManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly Paginator<Post> _paginator;
        private readonly IMapper _mapper;

        public PostManager(IPostRepository postRepository, IMapper mapper, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _paginator = new();
        }
        public async Task CreateAsync(Post post)
        {
            await _postRepository.CreateAsync(post);

        }
        public async Task<PaginatorResult<PostDto>> GetAll(int maxItems, int page)
        {
            BaseSpecification<Post> criteria = new BaseSpecification<Post>();
            criteria
                .AddInclude(p => p.Image)
                .AddInclude(p => p.User)
                .AddInclude(p => p.Status);

            PaginatorResult<Post> result = await _paginator
                .SetItemNumberPerPage(maxItems)
                .Paginate(_postRepository.GetByCriteriaQuery(criteria), page);

            if (result.Items.Count() == 0)
                throw new PostNotFoundException();

            PaginatorResult<PostDto> resultDto = new(result.TotalItems, result.ItemsOnPage
                , _mapper.Map<List<PostDto>>(result.Items), result.CurrentPage, result.TotalPages);

            return resultDto;
        }
        public async Task<PaginatorResult<PostDto>> GetUserPosts(Guid userId, int maxItems, int page)
        {
            UserEntity user = await _userRepository.GetUserByGuidAsync(userId) ?? throw new UserNotFoundException();

            BaseSpecification<Post> criteria = new BaseSpecification<Post>();
            criteria.AddCriteria(p => p.User == user);

            PaginatorResult<Post> result = await _paginator
                .SetItemNumberPerPage(maxItems)
                .Paginate(_postRepository.GetByCriteriaQuery(criteria), page);

            if (result.Items.Count() == 0)
                throw new PostNotFoundException();

            PaginatorResult<PostDto> resultDto = new(result.TotalItems, result.ItemsOnPage
                , _mapper.Map<List<PostDto>>(result.Items), result.CurrentPage, result.TotalPages);

            return resultDto;
        }

    }
}
