using Application_Core.Model;
using Infrastructure.Manager.Param;
using Infrastructure.Utility.Pagination;

namespace WebAPI.Services.Interfaces;

public interface IAlbumService
{
    Task<PaginatorResult<Album>> GetAllPaginated(int maxItems, int page);
    Task<PaginatorResult<Album>> Search(AlbumSearchDto criteria, int page = 1, int maxItems = 10);
}