using Application_Core.Common.Specification;
using Application_Core.Exception;
using Application_Core.Model;
using AutoMapper;
using Infrastructure.Database;
using Infrastructure.Database.Seed.Generator;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Pagination;
using Infrastructure.EF.Repository.PostRepository;
using Infrastructure.EF.Repository.UserRepository;
using Infrastructure.Enum;
using Infrastructure.Utility;
using WebAPI.Request;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services;

public struct CurrentUser
{ 
    public UserEntity? User { get; set; }
    public RoleEnum UserRole { get; set; }
}

public class PostService : IPostService
{
    private readonly IImageService _imageService;
    private readonly IPostRepository _postRepository;
    private readonly Paginator<Post> _paginator;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly UniqueFileNameAssigner _nameAssigner;
    private CurrentUser _currentUser = new()
    {
        User = null,
        UserRole = RoleEnum.User
    };


    public PostService(IPostRepository postRepository
        , IMapper mapper
        , IUserRepository userRepository
        , UniqueFileNameAssigner nameAssigner, ImageSharingDbContext dbContext, IImageService imageService)
    {
        _postRepository = postRepository;
        _paginator = new();
        _mapper = mapper;
        _userRepository = userRepository;
        _nameAssigner = nameAssigner;
        _imageService = imageService;
    }

    public PostService SetUser(UserEntity userEntity, RoleEnum roleEnum = RoleEnum.User)
    {
        _currentUser.User = userEntity;
        _currentUser.UserRole = roleEnum;
        return this;
    }

    public async Task<PaginatorResult<Post>> GetAll(int maxItems, int page)
    {
        BaseSpecification<Post> criteria = new BaseSpecification<Post>();

        criteria
            .AddInclude(p => p.Status)
            .AddInclude(p => p.Image)
            .AddInclude(p=> p.Reactions)
            .AddInclude(p => p.User);

        criteria = FilterCriteriaByOwnership(criteria);

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
        
        criteria
            .AddInclude(p => p.Status)
            .AddInclude(p => p.Image)
            .AddInclude(p=>p.Reactions)
            .AddInclude(p => p.User);

        criteria = FilterCriteriaByOwnership(criteria);

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
            .AddInclude(p => p.Reactions)
            .AddInclude(p => p.User);

        criteria = FilterCriteriaByOwnership(criteria);

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
    
    public async Task EditAsync(UpdatePostRequest postRequest, UserEntity user)
    {
        BaseSpecification<Post> criteria = new BaseSpecification<Post>();
        criteria.AddCriteria(p => p.User == user);
        criteria.AddCriteria(p => p.Guid == postRequest.PostGuid);
        criteria.AddInclude(x => x.Image);
        Post post = await _postRepository.GetByCriteriaSingle(criteria) ?? throw new AlbumNotFoundException();

        if (postRequest.Title != null)
            post.Title = postRequest.Title;

        if (postRequest.Tags != null)
            post.Tags = postRequest.Tags;

        post.StatusId = postRequest.IsHidden ? 2 : 1;
        if (postRequest.Image != null)
        {
            FileDto image = _mapper.Map<FileDto>(postRequest.Image);
            ImageDto imageDto = new()
            {
                Stream = image.Stream,
                Guid = post.Image.Guid,
                Name = image.Name,
                Title = post.Title,
                Length = image.Length
            };
            await _imageService.UpdateImage(imageDto);
        }

        await _postRepository.UpdateAsync(post);
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

    public async Task<Post> GetPost(Guid id)
    {
        BaseSpecification<Post> criteria = new BaseSpecification<Post>();

        criteria
            .AddInclude(p => p.User)
            .AddInclude(p => p.Reactions)
            .AddInclude(p => p.Comments)
            .AddInclude(p => p.Image)
            .AddInclude(p => p.Status)
            .AddCriteria(p => p.Guid == id);

        criteria = FilterCriteriaByOwnership(criteria);

        Post? post = await _postRepository.GetByCriteriaSingle(criteria) ?? throw new PostNotFoundException();
        return post;
    }

    private BaseSpecification<Post> FilterCriteriaByOwnership(BaseSpecification<Post> criteria)
    {
        if (_currentUser.User is null)
        {
            criteria.AddCriteria(p => p.Status.Name == StatusEnum.Visible.ToString());
        }
        else
        {
            if (_currentUser.UserRole != RoleEnum.Admin)
            {
                criteria.AddCriteria(p => p.Status.Name == StatusEnum.Visible.ToString() || p.User == _currentUser.User);
            }
        }

        return criteria;
    }
}