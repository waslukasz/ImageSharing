
using Application_Core.Common.Specification;
using Application_Core.Exception;
using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Pagination;
using Infrastructure.EF.Repository.AlbumRepository;
using Infrastructure.EF.Repository.ImageRepository;
using WebAPI.Request;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services;

public class AlbumService : IAlbumService
{
    private readonly IAlbumRepository _albumRepository;
    private readonly Paginator<Album> _paginator;
    private readonly IImageRepository _imageRepository;

    public AlbumService(IAlbumRepository albumRepository, IImageRepository imageRepository)
    {
        _paginator = new Paginator<Album>();
        _albumRepository = albumRepository;
        _imageRepository = imageRepository;
    }

    public async Task<PaginatorResult<Album>> GetAllPaginated(int maxItems, int page)
    {
        BaseSpecification<Album> specification = new BaseSpecification<Album>();

        specification
            .AddInclude(a => a.Images)
            .AddInclude(a => a.User);

        PaginatorResult<Album> result = await _paginator
            .SetItemNumberPerPage(maxItems)
            .Paginate(_albumRepository.GetByCriteriaQuery(
                specification
            ), page);

        if (result.Items.Count() == 0)
            throw new AlbumNotFoundException();

        return result;
    }

    public async Task<Album> GetAlbum(Guid id)
    {
        BaseSpecification<Album> specification = new BaseSpecification<Album>();

        specification
            .AddCriteria(a => a.Guid == id)
            .AddInclude(a => a.Images)
            .AddInclude(a => a.User);

        Album album = await _albumRepository.GetByCriteriaSingle(specification) ?? throw new AlbumNotFoundException();

        return album;
    }

    public async Task<Album> CreateAlbum(CreateAlbumRequest request, UserEntity user)
    {
        Album album = new Album();
        album.Title = request.Title;
        album.Description = request.Description;
        album.User = user;
        album.Images = await ValidateRequestedImages(request.Images);

        await _albumRepository.Add(album);

        return album;
    }

    public async Task<Album> UpdateAlbum(UpdateAlbumRequest request, UserEntity user, Guid albumId)
    {
        BaseSpecification<Album> specification = new BaseSpecification<Album>();
        specification
            .AddCriteria(a => a.Guid == albumId)
            .AddCriteria(a => a.User == user)
            .AddInclude(a => a.Images)
            .AddInclude(a => a.User);
        
        Album album = await _albumRepository.GetByCriteriaSingle(specification) ?? throw new AlbumNotFoundException();
        
        if (album.User != user)
            throw new AlbumNotFoundException();
        
        if (request.Images is not null && request.Images.Count() > 0)
        {
            album.Images = await ValidateRequestedImages(request.Images);
        }
        if (request.Description is not null)
        {
            album.Description = request.Description;
        }

        if (request.Title is not null)
        {
            album.Title = request.Title;
        }

        await _albumRepository.UpdateAlbum(album);
        return album;

    }

    public async Task<PaginatorResult<Album>> Search(AlbumSearchDto criteria, int page = 1, int maxItems = 10)
    {
        BaseSpecification<Album> specification = new BaseSpecification<Album>();

        specification
            .AddInclude(a => a.Images)
            .AddInclude(a => a.User);

        if (!string.IsNullOrWhiteSpace(criteria.AlbumTitle))
            specification.AddCriteria(c => c.Title.Contains(criteria.AlbumTitle));

        if (criteria.MaxImages is not null && int.IsPositive((int)criteria.MaxImages))
            specification.AddCriteria(c => c.Images.Count() <= criteria.MaxImages);

        if (criteria.MinImages is not null && int.IsPositive((int)criteria.MinImages))
            specification.AddCriteria(c => c.Images.Count() >= criteria.MinImages);

        if (criteria.OrderBy == OrderBy.Asc) {
            specification.SetOrderBy(c => c.Title);
        } else { 
            specification.SetOrderByDescending(c => c.Title);
        }
        
        IQueryable<Album> query = _albumRepository.GetByCriteriaQuery(
            specification
        );

        PaginatorResult<Album> result = await _paginator
            .SetItemNumberPerPage(maxItems)
            .Paginate(query, page);

        if (result.Items.Count() == 0)
            throw new AlbumNotFoundException();

        return result;
    }
    
    public async Task DeleteAlbum(Album album)
    {
        await _albumRepository.Remove(album);
    }

    private async Task<ICollection<Image>> ValidateRequestedImages(List<Guid> images)
    {
        BaseSpecification<Image> imgSpec = new BaseSpecification<Image>();
        imgSpec.AddCriteria(i => images.Contains(i.Guid));
        ICollection<Image> foundImages = (await _imageRepository.GetByCriteria(imgSpec)).ToList();
            
        Console.WriteLine("---------------------------------------");
        Console.WriteLine($"Found images count {foundImages.Count}");
        Console.WriteLine($"Requested images count {images.Count}");
        Console.WriteLine("---------------------------------------");
            
        if (foundImages.Count() != images.Count())
            throw new ImageNotFoundException("Could not find all images specified in request !");

        return foundImages;
    }
}