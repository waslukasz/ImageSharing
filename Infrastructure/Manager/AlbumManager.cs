using Application_Core.Exception;
using Application_Core.Model;
using Infrastructure.EF.Repository.AlbumRepository;
using Infrastructure.Utility.Pagination;

namespace Infrastructure.Manager;

public class AlbumManager
{
    private readonly IAlbumRepository _albumRepository;

    private readonly Paginator<Album> _paginator;

    public AlbumManager(IAlbumRepository albumRepository)
    {
        _paginator = new Paginator<Album>();
        _albumRepository = albumRepository;
    }

    public async Task<PaginatorResult<Album>> GetAllPaginated(int maxItems, int page)
    {
        PaginatorResult<Album> result = await _paginator
            .SetItemNumberPerPage(maxItems)
            .Paginate(_albumRepository.GetAllQuery(), page);

        if (result.Items.Count() == 0)
            throw new AlbumNotFoundException();

        return result;
    }
}