using Application_Core.Common.Specification;
using Application_Core.Exception;
using Application_Core.Model;
using AutoMapper;
using Infrastructure.Database;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Pagination;
using Infrastructure.EF.Repository.ImageRepository;
using Infrastructure.EF.Repository.PostRepository;
using Infrastructure.EF.Repository.UserRepository;
using Infrastructure.Utility;
using WebAPI.Request;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly Paginator<Post> _paginator;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly UniqueFileNameAssigner _nameAssigner;


    public PostService(IPostRepository postRepository
        , IMapper mapper
        , IUserRepository userRepository
        , IImageRepository imageRepository, UniqueFileNameAssigner nameAssigner, ImageSharingDbContext dbContext)
    {
        _postRepository = postRepository;
        _paginator = new();
        _mapper = mapper;
        _userRepository = userRepository;
        _nameAssigner = nameAssigner;
    }

    public async Task<PaginatorResult<Post>> GetAll(int maxItems, int page)
    {
        BaseSpecification<Post> criteria = new BaseSpecification<Post>();

        criteria
            .AddInclude(p => p.Status)
            .AddInclude(p => p.Image)
            .AddInclude(p=>p.Reactions)
            .AddInclude(p => p.User);
            
        PaginatorResult<Post> result = await _paginator
            .SetItemNumberPerPage(maxItems)
            .Paginate(_postRepository.GetByCriteriaQuery(criteria), page);

        if (result.Items.Count() == 0)
            throw new PostNotFoundException();

        return result;
    }

    public async Task<PaginatorResult<PostDto>> GetUserPosts(Guid id, int maxItems, int page)
    {
        UserEntity user = await _userRepository.GetByGuid(id) ?? throw new UserNotFoundException();

        BaseSpecification<Post> criteria = new BaseSpecification<Post>();

        criteria.AddCriteria(p => p.User == user);
        criteria
            .AddInclude(p => p.Status)
            .AddInclude(p => p.Image)
            .AddInclude(p=>p.Reactions)
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

    public async Task<PaginatorResult<PostDto>> GetPostByTags(SearchPostRequest request, PaginationRequest paginationRequest)
    {
        BaseSpecification<Post> criteria = new BaseSpecification<Post>();
        
        criteria
            .AddInclude(p => p.Status)
            .AddInclude(p => p.Image)
            .AddInclude(p=>p.Reactions)
            .AddInclude(p => p.User);
        
        if (request.ImageId!=Guid.Empty)
        {
            criteria.AddCriteria(c => c.Image.Guid == request.ImageId);
        }
        if (request.Title != null)
        {
            criteria.AddCriteria(c => c.Title.Contains(request.Title));
        }
        if (request.OrderBy==OrderBy.Asc)
        {
            criteria.SetOrderBy(x=>x.Title);
        }
        else
        {
            criteria.SetOrderByDescending(x=>x.Title);
        }
        if (request.Tags == null)
        {
            request.Tags = new List<string>();
        }

        IQueryable<Post> data = _postRepository.GetByTagsQuery(request.Tags,criteria);

        PaginatorResult<Post> result = await _paginator
            .SetItemNumberPerPage(paginationRequest.ItemNumber)
            .Paginate(data, paginationRequest.Page);
        
        if (result.Items.Count() == 0)
            throw new PostNotFoundException();

        PaginatorResult<PostDto> resultDto = new(result.TotalItems, result.ItemsOnPage
            , _mapper.Map<List<PostDto>>(result.Items), result.CurrentPage, result.TotalPages);

        return resultDto;
    }


    public async Task CreateAsync(CreatePostRequest postRequest, UserEntity user)
    {
        FileDto image = _mapper.Map<FileDto>(postRequest);
        Post post = _mapper.Map<Post>(postRequest);

        post.User = user;
        post.Image = image.ToImage(_nameAssigner);
        post.Image.User = user;

        await _postRepository.Add(post);
    }

    public async Task DeleteAsync(DeletePostRequest postRequest, UserEntity user)
    {
        BaseSpecification<Post> criteria = new BaseSpecification<Post>();

        criteria.AddCriteria(p => p.Guid == postRequest.PostGuid);
        criteria.AddCriteria(p => p.User == user);

        Post post = await _postRepository.GetByCriteriaSingle(criteria) ?? throw new PostNotFoundException();

        await _postRepository.Remove(post);
    }
}