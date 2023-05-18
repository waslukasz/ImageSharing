using Application_Core.Exception;
using Application_Core.Model;
using AutoMapper;
using Infrastructure.Dto;
using Infrastructure.EF.Repository.PostRepository;
using Infrastructure.Utility.Pagination;

namespace WebAPI.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly Paginator<Post> _paginator;
    private readonly IMapper _mapper;

    public PostService(IPostRepository postRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _paginator = new();
        _mapper = mapper;
    }
    public async Task CreateAsync(Post post)
    {
        await _postRepository.CreateAsync(post);

    }
    public async Task<PaginatorResult<PostDto>> GetAll(int maxItems, int page)
    {
        PaginatorResult<Post> result = await _paginator
            .SetItemNumberPerPage(maxItems)
            .Paginate(_postRepository.GetAllAsync(), page);

        if (result.Items.Count() == 0)
            throw new PostNotFoundException();

        PaginatorResult<PostDto> resultDto = new(result.TotalItems, result.ItemsOnPage
            , _mapper.Map<List<PostDto>>(result.Items), result.CurrentPage, result.TotalPages);

        return resultDto;
    }
    public async Task<PaginatorResult<PostDto>> GetUserPosts(int maxItems, int page, int id)
    {

        PaginatorResult<Post> result = await _paginator
            .SetItemNumberPerPage(maxItems)
            .Paginate(_postRepository.GetByUserIdAsync(id), page);

        if (result.Items.Count() == 0)
            throw new PostNotFoundException();

        PaginatorResult<PostDto> resultDto = new(result.TotalItems, result.ItemsOnPage
            , _mapper.Map<List<PostDto>>(result.Items), result.CurrentPage, result.TotalPages);

        return resultDto;
    }
}