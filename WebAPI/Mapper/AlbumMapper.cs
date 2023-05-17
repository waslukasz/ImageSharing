using Application_Core.Model;
using Infrastructure.Utility.Pagination;
using WebAPI.Response;

namespace WebAPI.Mapper;

public static class AlbumMapper
{
    public static AlbumResponse FromAlbumToAlbumResponse(Album album)
    {
        return new AlbumResponse()
        {
            Id = album.Guid,
            Titile = album.Title,
            Description = album.Description
        };
    }
}