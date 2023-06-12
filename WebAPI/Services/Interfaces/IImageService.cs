using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.Struct;

namespace WebAPI.Services.Interfaces;

public interface IImageService
{
    Task<Image> CreateImage(FileDto imageDto, UserEntity user);
    Task UpdateImage(ImageDto imageDtor);
    Task DeleteImage(Guid id, UserEntity user);
    Task<Image> GetImageWithStream(Guid id, CurrentUser user);
    Task<Image> GetImageThumbnailWithStream(Guid id, CurrentUser user);

}