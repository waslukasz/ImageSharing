using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Pagination;

namespace WebAPI.Services.Interfaces;

public interface IAlbumService
{
    Task<PaginatorResult<Album>> GetAllPaginated(int maxItems, int page);
    Task<PaginatorResult<Album>> Search(AlbumSearchDto criteria, int page = 1, int maxItems = 10);
}