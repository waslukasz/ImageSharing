using Application_Core.Model;
using Infrastructure.EF.Entity;
using WebAPI.Response;

namespace WebAPI.Mapper;

public static class AlbumMapper
{
    public static AlbumResponse FromAlbumToAlbumResponse(Album album)
    {
        return new AlbumResponse()
        {
            Id = album.Guid,
            User = UserMapper.FromUserToUserResponse((UserEntity)album.User),
            Title = album.Title,
            Description = album.Description,
            ImageCount = album.Images.Count()
        };
    }

    public static AlbumWithImagesResponse FromAlbumToAlbumWithImagesResponse(Album album)
    {
        return new AlbumWithImagesResponse()
        {
            Id = album.Guid,
            User = UserMapper.FromUserToUserResponse((UserEntity)album.User),
            Title = album.Title,
            Description = album.Description,
            Images = album.Images.Select(c => ImageMapper.FromImageToImageResponse(c)).ToList()
        };
    }
}