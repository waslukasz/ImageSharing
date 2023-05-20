using Application_Core.Model;

namespace Infrastructure.EF.Repository.ImageRepository;

public interface IImageRepository
{
    Task<Image?> GetImageByGuid(Guid id);

    Task AddImage(Image image);

    Task DeleteImage(Image image);
}