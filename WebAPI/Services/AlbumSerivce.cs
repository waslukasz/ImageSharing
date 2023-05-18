using Application_Core.Common.Specification;
using Application_Core.Exception;
using Application_Core.Model;
using Infrastructure.EF.Repository.AlbumRepository;
using Infrastructure.Manager.Param;
using Infrastructure.Utility.Pagination;

namespace WebAPI.Managers;

public class AlbumSerivce : IAlbumSerivce
{
    private readonly IAlbumRepository _albumRepository;

    private readonly Paginator<Album> _paginator;

    public AlbumSerivce(IAlbumRepository albumRepository)
    {
        _paginator = new Paginator<Album>();
        _albumRepository = albumRepository;
    }

    public async Task<PaginatorResult<Album>> GetAllPaginated(int maxItems, int page)
    {
        BaseSpecification<Album> specification = new BaseSpecification<Album>();

        specification.AddInclude(a => a.Images);
        
        PaginatorResult<Album> result = await _paginator
            .SetItemNumberPerPage(maxItems)
            .Paginate(_albumRepository.GetAlbumsByCriteriaQuery(
                specification
            ), page);

        if (result.Items.Count() == 0)
            throw new AlbumNotFoundException();

        return result;
    }

    public async Task<PaginatorResult<Album>> Search(AlbumSearchDto criteria, int page = 1, int maxItems = 10)
    {
        BaseSpecification<Album> specification = new BaseSpecification<Album>();

        specification.AddInclude(a => a.Images);
        
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
        
        IQueryable<Album> query = _albumRepository.GetAlbumsByCriteriaQuery(
            specification
        );

        PaginatorResult<Album> result = await _paginator
            .SetItemNumberPerPage(maxItems)
            .Paginate(query, page);

        if (result.Items.Count() == 0)
            throw new AlbumNotFoundException();

        return result;
    }
}