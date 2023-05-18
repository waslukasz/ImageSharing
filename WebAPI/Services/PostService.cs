using Application_Core.Common.Specification;
using Application_Core.Exception;
using Application_Core.Model;
using AutoMapper;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Pagination;
using Infrastructure.EF.Repository.PostRepository;
using Infrastructure.EF.Repository.UserRepository;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly Paginator<Post> _paginator;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public PostService(IPostRepository postRepository, IMapper mapper, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _paginator = new();
        _mapper = mapper;
        _userRepository = userRepository;
    }
    public async Task CreateAsync(Post post)
    {
        await _postRepository.CreateAsync(post);

    }
    public async Task<PaginatorResult<PostDto>> GetAll(int maxItems, int page)
    {
        BaseSpecification<Post> criteria = new BaseSpecification<Post>();
        
        criteria
            .AddInclude(p => p.Status)
            .AddInclude(p => p.Image)
            .AddInclude(p => p.User);
        
        PaginatorResult<Post> result = await _paginator
            .SetItemNumberPerPage(maxItems)
            .Paginate(_postRepository.GetByCriteriaQuery(criteria), page);

        if (result.Items.Count() == 0)
            throw new PostNotFoundException();

        PaginatorResult<PostDto> resultDto = new(result.TotalItems, result.ItemsOnPage
            , _mapper.Map<List<PostDto>>(result.Items), result.CurrentPage, result.TotalPages);

        return resultDto;
    }
    public async Task<PaginatorResult<PostDto>> GetUserPosts(Guid id, int maxItems, int page)
    {
        UserEntity user = await _userRepository.GetUserByGuidAsync(id) ?? throw new UserNotFoundException();

        BaseSpecification<Post> criteria = new BaseSpecification<Post>();

        criteria.AddCriteria(p => p.User == user);
        criteria
            .AddInclude(p => p.Status)
            .AddInclude(p => p.Image)
            .AddInclude(p => p.User);

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