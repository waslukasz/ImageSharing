using Application_Core.Common.Specification;
using Application_Core.Exception;
using Application_Core.Model;
using AutoMapper;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Pagination;
using Infrastructure.EF.Repository.PostRepository;
using Infrastructure.EF.Repository.UserRepository;
using WebAPI.Request;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly Paginator<Post> _paginator;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ImageService _imageService;


    public PostService(IPostRepository postRepository
        , IMapper mapper
        , IUserRepository userRepository
        , ImageService imageService
    )
    {
        _postRepository = postRepository;
        _paginator = new();
        _mapper = mapper;
        _userRepository = userRepository;
        _imageService = imageService;
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

    public async Task CreateAsync(CreatePostRequest postRequest, UserEntity user)
    {
        var image = _mapper.Map<FileDto>(postRequest);
        var post = _mapper.Map<Post>(postRequest);
        post.UserId = user.Id;
        var imageId = await _imageService.CreateImage(image, user);
        post.ImageId = imageId;
        await _postRepository.CreateAsync(post);
    }

    public async Task DeleteAsync(DeletePostRequest postRequest, UserEntity user)
    {
        BaseSpecification<Post> criteria = new BaseSpecification<Post>();

        criteria.AddCriteria(p => p.Guid == postRequest.PostGuid);
        criteria
            .AddInclude(p => p.Image);

        var posts = await _postRepository.GetByCriteriaAsync(criteria);
        var post = posts.FirstOrDefault();
        if (post == null)
            throw new PostNotFoundException();
        var image = await _imageService.ImageFindByGuid(post.Image.Guid);
        if (image == null)
        {
            throw new ImageNotFoundException();
        }
        await _imageService.DeleteImage(post.Image.Guid, user);
    }
}