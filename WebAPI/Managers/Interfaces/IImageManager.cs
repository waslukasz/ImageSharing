using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;

namespace WebAPI.Managers.Interfaces;

public interface IImageManager
{
    Task CreateImage(FileDto imageDto, UserEntity user);
    Task UpdateImage(ImageDto imageDto, UserEntity user);
    Task DeleteImage(Guid id, UserEntity user);
    Task<IEnumerable<Image>> GetAll();
}
