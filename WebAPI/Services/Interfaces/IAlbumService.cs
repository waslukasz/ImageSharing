using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.EF.Pagination;
using WebAPI.Request;

namespace WebAPI.Services.Interfaces;

public interface IAlbumService
{
    Task<PaginatorResult<Album>> GetAllPaginated(int maxItems, int page);
    Task<PaginatorResult<Album>> Search(AlbumSearchDto criteria, int page = 1, int maxItems = 10);
    Task<Album> GetAlbum(Guid id);
    Task DeleteAlbum(Album album);
    Task<Album> CreateAlbum(CreateAlbumRequest request, UserEntity user);
    Task<Album> UpdateAlbum(UpdateAlbumRequest request, UserEntity user, Guid albumId);
}