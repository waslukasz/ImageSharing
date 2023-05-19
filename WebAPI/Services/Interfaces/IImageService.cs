using Application_Core.Model;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;

namespace WebAPI.Services.Interfaces;

public interface IImageService
{
    Task CreateImage(FileDto imageDto, UserEntity user);
    Task UpdateImage(ImageDto imageDto, UserEntity user);
    Task DeleteImage(Guid id, UserEntity user);
    Task<IEnumerable<Image>> GetAll();
}